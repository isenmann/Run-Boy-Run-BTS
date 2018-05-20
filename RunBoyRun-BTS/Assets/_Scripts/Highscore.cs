using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class Highscore : MonoBehaviour
{
    private string highScoreFile;
    public HighScoreData highscore = new HighScoreData();

    private void Awake()
    {
        highScoreFile = Application.persistentDataPath + "/highscore.dat";
        Load();
    }

    public void SetHighscore(float time, long score)
    {
        var entry = new HighScoreEntry();
        entry.Time = time;
        entry.Score = score;

        highscore.Entries.Add(entry);
        highscore.Entries = highscore.Entries.OrderByDescending(i => i.Time).ToList();
        highscore.Entries = highscore.Entries.Take(10).ToList();

        Save();
    }

    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        using (FileStream stream = File.Open(highScoreFile, FileMode.OpenOrCreate))
        {
            bf.Serialize(stream, highscore);
        }
    }

    public void Load()
    {
        if (File.Exists(highScoreFile))
        {
            BinaryFormatter bf = new BinaryFormatter();
            using (FileStream stream = File.Open(highScoreFile, FileMode.Open))
            {
                highscore = (HighScoreData) bf.Deserialize(stream);
            }
        }
    }

    private void OnDestroy()
    {
        Save();
    }
}

[Serializable]
public class HighScoreData
{
    public List<HighScoreEntry> Entries = new List<HighScoreEntry>();
}

[Serializable]
public class HighScoreEntry
{
    public float Time;
    public long Score;
}
