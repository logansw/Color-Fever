using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighscoreData : IJSONData<HighscoreData>
{
    public List<int> Highscores;

    public HighscoreData CreateNewFile() {
        HighscoreData data = new HighscoreData();
        data.Highscores = new List<int>();
        return data;
    }
}
