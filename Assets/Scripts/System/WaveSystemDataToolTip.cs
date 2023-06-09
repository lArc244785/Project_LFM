using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR
public enum WaveSystemDataToolTipType
{
	Create,
	Load,
}

public class WaveSystemDataToolTip:EditorWindow
{
	private static WaveSystemDataToolTipType m_mode;
	private string m_text;
	private static WaveSystemDataEditor m_editor;

	private static WaveSystemDataToolTip m_window;
	public static void Init(WaveSystemDataToolTipType mode, WaveSystemDataEditor editor)
	{
		m_mode = mode;
		m_editor = editor;

		m_window = GetWindow<WaveSystemDataToolTip>();
		m_window.minSize = new Vector2(200, 100);
		m_window.maxSize = m_window.minSize;
		m_window.Show();
	}

	private void OnGUI()
	{
		EditorGUILayout.BeginVertical();
		m_text = EditorGUILayout.TextField(m_text);
		if(GUILayout.Button("OK"))
		{
			switch (m_mode)
			{
				case WaveSystemDataToolTipType.Create:
					m_editor.Create(m_text);
					break;
				case WaveSystemDataToolTipType.Load:
					m_editor.Load(m_text);
					break;
			}
			m_window.Close();
		}
		EditorGUILayout.EndVertical();
	}
}
#endif