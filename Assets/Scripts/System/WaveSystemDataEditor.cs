using System.IO;
using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR
public class WaveSystemDataEditor : EditorWindow
{
	private WaveSystemData m_data;
	private WaveData m_selectionWaveData;
	private string m_fileName;

	private Vector2 m_waveSelectionScroll;
	private Vector2 m_enemyEditScroll;

	private float m_totalRatio;
	[MenuItem("Tools/WaveSystemDataEditor")]
	private static void Init()
	{
		var window = GetWindow<WaveSystemDataEditor>();
		window.minSize = new Vector2(800.0f, 800.0f);
		window.maxSize = window.minSize;
		window.Show();
	}


	private void OnGUI()
	{
		DrawTopBar();
		DrawWaveSelection();
		DrawEditWaveData();
	}

	private void DrawTopBar()
	{
		EditorGUILayout.BeginHorizontal();
		if (GUILayout.Button("Create"))
		{
			WaveSystemDataToolTip.Init(WaveSystemDataToolTipType.Create, this);
		}
		if (GUILayout.Button("Save"))
		{
			Save();
		}
		if (GUILayout.Button("Load"))
		{
			WaveSystemDataToolTip.Init(WaveSystemDataToolTipType.Load, this);
		}
		EditorGUILayout.EndHorizontal();
	}
	private void DrawWaveSelection()
	{
		GUILayout.BeginArea(new Rect(10, 50, 256, 700));
		DrawWaveDataSelection();
		GUILayout.EndArea();

		//BottonButtons
		GUILayout.BeginArea(new Rect(10, 750, 256, 30));
		GUILayout.BeginHorizontal();
		if (GUILayout.Button("+"))
		{
			AddWaveData();
		}
		if (GUILayout.Button("-"))
		{
			RemoveWaveData();
		}
		GUILayout.EndHorizontal();
		GUILayout.EndArea();
	}
	private void DrawEnemyEdit()
	{


		m_totalRatio = 0;
		m_enemyEditScroll = GUILayout.BeginScrollView(m_enemyEditScroll);
		foreach (var enemyData in m_selectionWaveData.EnemyList)
		{ 
			GUILayout.BeginHorizontal();
			enemyData.Enemy = (ObjectPoolKey)EditorGUILayout.EnumPopup(enemyData.Enemy);
			enemyData.Ratio = EditorGUILayout.FloatField(enemyData.Ratio);
			m_totalRatio += enemyData.Ratio;
			GUILayout.EndHorizontal();

			GUI.color = Color.white;
		}
		GUILayout.EndScrollView();
	}

	private void DrawEditWaveData()
	{
		if (m_selectionWaveData == null)
			return;

		GUILayout.BeginArea(new Rect(276, 50, 513, 700));
		GUILayout.BeginVertical();

		GUILayout.BeginHorizontal();
		GUILayout.Label("Tick", GUILayout.Width(100.0f));
		m_selectionWaveData.Tick = EditorGUILayout.FloatField(m_selectionWaveData.Tick);
		GUILayout.EndHorizontal();

		GUILayout.BeginHorizontal();
		GUILayout.Label("Amount", GUILayout.Width(100.0f));
		m_selectionWaveData.Amount = EditorGUILayout.IntField(m_selectionWaveData.Amount);
		GUILayout.EndHorizontal();

		GUILayout.Space(10.0f);
		DrawEnemyEdit();
		DrawTotalRatio();
		GUILayout.EndVertical();
		GUILayout.EndArea();

		GUILayout.BeginArea(new Rect(276, 750, 513, 30));
		GUILayout.BeginHorizontal();
		if (GUILayout.Button("+"))
		{
			AddEnemyData();
		}
		if (GUILayout.Button("-"))
		{
			RemoveEnemyData();
		}
		GUILayout.EndHorizontal();
		GUILayout.EndArea();
	}

	private void DrawTotalRatio()
	{
		if (m_totalRatio > 1.0f)
			GUI.color = Color.red;

		GUILayout.BeginHorizontal();
		GUILayout.Label("Total Ratio");
		GUILayout.Label(m_totalRatio.ToString());
		GUILayout.EndHorizontal();

		GUI.color = Color.white;
	}

	private void DrawWaveDataSelection()
	{
		if (m_data == null)
			return;
		if (m_data.waveDataList.Count == 0)
		{
			GUILayout.Label("None Data");
			return;
		}


		m_waveSelectionScroll = GUILayout.BeginScrollView(m_waveSelectionScroll);
		for (int i = 0; i < m_data.waveDataList.Count; i++)
		{
			if (m_selectionWaveData == m_data.waveDataList[i])
				GUI.color = Color.green;

			if (GUILayout.Button($"Wave {i + 1}"))
			{
				SetSelectionWaveData(m_data.waveDataList[i]);
			}
			GUI.color = Color.white;
		}
		GUILayout.EndScrollView();
	}

	private void SetSelectionWaveData(WaveData selection)
	{
		m_selectionWaveData = selection;
	}

	private void AddWaveData()
	{
		if (m_data == null)
			return;

		var newData = new WaveData();
		m_data.waveDataList.Add(newData);
	}

	private void RemoveWaveData()
	{
		if (m_data == null || m_data.waveDataList.Count == 0)
			return;

		if (!m_data.waveDataList.Contains(m_selectionWaveData))
		{
			m_data.waveDataList.RemoveAt(m_data.waveDataList.Count - 1);
		}
		else
		{
			m_data.waveDataList.Remove(m_selectionWaveData);
		}
	}

	private void AddEnemyData()
	{
		if (m_selectionWaveData == null)
			return;
		m_selectionWaveData.EnemyList.Add(new WaveEnemy());
	}

	private void RemoveEnemyData()
	{
		if (m_selectionWaveData == null || m_selectionWaveData.EnemyList.Count == 0)
			return;

		m_selectionWaveData.EnemyList.RemoveAt(m_selectionWaveData.EnemyList.Count - 1);
	}

	public void Create(string name)
	{
		if (IsExistsFile(name))
		{
			Debug.LogWarning($"File Exists: {name}.json");
			return;
		}

		m_fileName = name;
		m_data = new();

		Debug.Log("Create NewData");
		Save();
	}

	public void Save()
	{
		if (m_data == null)
		{
			Debug.LogWarning($"Save File Null Data");
			return;
		}

		JsonHandler json = new();
		json.SaveData(m_data, Utility.GetWaveSystemDataPath(m_fileName));
		Debug.Log($"Save Data {Utility.GetWaveSystemDataPath(m_fileName)}");
	}

	public void Load(string name)
	{
		if (!IsExistsFile(name))
		{
			Debug.LogWarning($"File Not Exists:{name}.json");
			return;
		}
		m_fileName = name;
		JsonHandler json = new();
		m_data = json.LoadWaveData(Utility.GetWaveSystemDataPath(name));
		Debug.Log($"Load Data {m_fileName}");
	}

	private bool IsExistsFile(string name)
	{
		return File.Exists(Utility.GetWaveSystemDataPath(name));
	}


}
#endif