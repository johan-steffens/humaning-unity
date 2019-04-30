using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Scores
{
    private ScoreData scores;
    private string gameDataFileName = "scores.json";

    public Scores()
    {
        LoadGameData();
    }

    private void LoadGameData()
    {
        // Path.Combine combines strings into a file path
        // Application.StreamingAssets points to Assets/StreamingAssets in the Editor, and the StreamingAssets folder in a build
        string filePath = Path.Combine(Application.persistentDataPath, gameDataFileName);

        if (! File.Exists(filePath)) 
        {
            scores = new ScoreData();
            SaveGameData();
            return;
        }

        string dataAsJson = File.ReadAllText(filePath);
        scores = JsonUtility.FromJson<ScoreData>(dataAsJson);

        if (scores == null)
        {
            scores = new ScoreData();
        }
    }

    private void SaveGameData()
    {
        string filePath = Path.Combine(Application.persistentDataPath, gameDataFileName);

        BinaryFormatter bf = new BinaryFormatter();
        using (var file = File.Open(filePath, FileMode.OpenOrCreate))
        {
            byte[] scoreArray = Encoding.UTF8.GetBytes(JsonUtility.ToJson(scores));
            file.Write(scoreArray, 0, scoreArray.Length);
            file.Close();
        }
    }

    public List<Score> GetScores()
    {
        return scores.scores;
    }

    public void AddScore(Score score)
    {
        if(scores.scores.Count == 5)
        {
            scores.scores.RemoveAt(4);
        }

        // Ensure highest score is on top
        scores.scores.Add(score);
        scores.scores = scores.scores.OrderByDescending(x => x.value).ToList();

        // Save scores
        SaveGameData();
    }

}
