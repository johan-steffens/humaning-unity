using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
        string filePath = Path.Combine(Application.streamingAssetsPath, gameDataFileName);

        if (File.Exists(filePath))
        {
            string dataAsJson = File.ReadAllText(filePath);
            scores = JsonUtility.FromJson<ScoreData>(dataAsJson);
        }
        else
        {
            File.Create(filePath);
            Debug.LogError("Cannot load game data! File did not exist.");
        }

        if (scores == null)
        {
            scores = new ScoreData();
        }
    }

    private void SaveGameData()
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, gameDataFileName);

        if (File.Exists(filePath))
        {
            string dataAsJson = JsonUtility.ToJson(scores);

            Debug.Log("WRITING JSON");
            Debug.Log(dataAsJson);

            File.WriteAllText(filePath, dataAsJson);
        }
        else
        {
            File.Create(filePath);
            Debug.LogError("Cannot load game data! File did not exist.");
            scores = new ScoreData();
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
