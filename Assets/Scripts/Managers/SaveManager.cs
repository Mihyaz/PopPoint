using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveManager
{
    public static void SaveScore(int player)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream stream = new FileStream(Application.persistentDataPath + "/score.sav", FileMode.Create);
        SaveData fdata = new SaveData(player);
        bf.Serialize(stream, fdata);
        stream.Close();
    }
    public static int LoadScore()
    {
        if (File.Exists(Application.persistentDataPath + "/score.sav"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream stream = new FileStream(Application.persistentDataPath + "/score.sav", FileMode.Open);
            SaveData fdata = bf.Deserialize(stream) as SaveData;
            stream.Close();
            return fdata.myScore;
        }
        else
        {
            return 0;
        }

    }

}

[Serializable]
public class SaveData
{
    public int myScore;
    public SaveData(int player)
    {
        myScore = player;
    }
}