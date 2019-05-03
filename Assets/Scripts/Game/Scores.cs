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
    private string filePath = Path.Combine(Application.persistentDataPath, "scores.json");

    public Scores()
    {
        LoadGameData();
    }

    private void LoadGameData()
    {
        // Create the file if it doesn't exist
        if (! File.Exists(filePath)) 
        {
            scores = new ScoreData();
            SaveGameData();
            return;
        }

        // Read the contents of the file
        var file = File.Open(filePath, FileMode.OpenOrCreate);

        byte[] scoreArray = new byte[file.Length];
        file.Read(scoreArray, 0, (int) file.Length);
        file.Close();
        string scoreData = Encoding.UTF8.GetString(scoreArray);

        scores = JsonUtility.FromJson<ScoreData>(scoreData);
        if (scores == null)
        {
            scores = new ScoreData();
        }
    }

    private void SaveGameData()
    {
        // Open file
        var file = File.Open(filePath, FileMode.OpenOrCreate);

        // Clear file contents
        file.SetLength(0);
        file.Flush();

        // Now write new contents
        byte[] scoreArray = Encoding.UTF8.GetBytes(JsonUtility.ToJson(scores));
        file.Write(scoreArray, 0, scoreArray.Length);
        file.Close();
    }

    public List<Score> GetScores()
    {
        return scores.scores;
    }

    public bool AddScore(Score score)
    {
        if(scores.scores.Count == 8)
        {
            // Don't save score if the score is worth less than the lowest score
            if(score.value <= scores.scores[7].value)
            {
                return false;
            } else
            {
                scores.scores[7] = score;
            }
        } else
        {
            scores.scores.Add(score);
        }

        // Ensure highest score is on top
        scores.scores = scores.scores.OrderByDescending(x => x.value).ToList();

        // Save scores
        SaveGameData();

        // Return new high score
        return true;
    }

}
