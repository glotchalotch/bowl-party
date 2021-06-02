using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

//if you think this is too much setup for only one property you're probably right but it's always good to plan ahead
public static class SaveDataManager
{
    public static SaveData save;

    public static void LoadSave()
    {
        string filePath = Application.persistentDataPath + "/save.dat";
        if (File.Exists(filePath))
        {
            FileStream stream = File.OpenRead(filePath);
            SaveData awesome = (SaveData)new BinaryFormatter().Deserialize(stream);
            save = awesome ?? new SaveData();
            stream.Close();
        }
        else save = new SaveData();
    }

    public static void SaveGame()
    {
        string filePath = Application.persistentDataPath + "/save.dat";
        FileStream stream;
        if (!File.Exists(filePath)) stream = File.Create(filePath);
        else stream = File.OpenWrite(filePath);
        new BinaryFormatter().Serialize(stream, save);
        stream.Close();
    }

    [System.Serializable]
    public class SaveData
    {
        public int highScore = 0;
    }
}
