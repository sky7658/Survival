using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using LMS.Data;
using System.IO.Enumeration;

public class DataManager
{
    private static readonly string dataFilePath = Path.Combine(Application.persistentDataPath + "/Data.json");
    public static void SaveData(PlayerData data)
    {
        if (data == null)
        {
            Debug.Log("Player Data is null");
            return;
        }
        //string _dataUTF8 = ConvertJsonToUTF8String(JsonUtility.ToJson(data));
        string _dataUTF8 = JsonUtility.ToJson(data);
        File.WriteAllText(dataFilePath, _dataUTF8);
    }

    public static PlayerData LoadData()
    {
        PlayerData _data = null;

        if (File.Exists(dataFilePath))
        {
            //string _fromJsonData = ConvertUTF8StringToJson(File.ReadAllText(dataFilePath));
            string _fromJsonData = File.ReadAllText(dataFilePath);
            _data = JsonUtility.FromJson<PlayerData>(_fromJsonData);
        }
        else _data = new PlayerData();

        return _data;
    }

    private static string ConvertJsonToUTF8String(string _jsonData)
    {
        byte[] _databytes = System.Text.Encoding.UTF8.GetBytes(_jsonData);
        string _encodedJson = System.Convert.ToBase64String(_databytes);

        return _encodedJson;
    }

    private static string ConvertUTF8StringToJson(string _utf8Data)
    {
        byte[] _databytes = System.Convert.FromBase64String(_utf8Data);
        string _decodedJson = System.Text.Encoding.UTF8.GetString(_databytes);

        return _decodedJson;
    }
}
