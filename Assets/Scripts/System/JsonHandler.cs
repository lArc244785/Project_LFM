using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;
using System.Text;

public class JsonHandler 
{

    public void SaveData(object saveObject, string path)
	{
        string jsonData = JsonConvert.SerializeObject(saveObject);
        FileStream fs = new FileStream(path, FileMode.OpenOrCreate);
        byte[] data = Encoding.UTF8.GetBytes(jsonData);
        fs.Write(data, 0, data.Length);
        fs.Close();
	}

    public WaveSystemData LoadWaveData(string path)
	{
        FileStream fs = new(path, FileMode.Open);
        byte[] data = new byte[fs.Length];
        fs.Read(data, 0, data.Length);
        fs.Close();
        string jsonData = Encoding.UTF8.GetString(data);
        return JsonConvert.DeserializeObject<WaveSystemData>(jsonData);
    }
 
}
