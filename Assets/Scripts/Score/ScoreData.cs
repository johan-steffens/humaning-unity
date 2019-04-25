using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[Serializable]
public class ScoreData
{

    public List<Score> scores;

    public ScoreData()
    {
        scores = new List<Score>();
    }
}
