using UnityEngine;

public class Utility
{
	public static string GetWaveSystemDataPath(string fileName)
	{
		return Application.dataPath + "/Datas/WaveData/" + fileName + ".json";
	}
}