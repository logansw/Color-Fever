using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighscoreData : IJSONData<HighscoreData>
{
    public List<Entry> Highscores;
    public string PreviousName;

    public HighscoreData CreateNewFile() {
        HighscoreData data = new HighscoreData();
        data.Highscores = new List<Entry>();
        data.PreviousName = "Name";
        return data;
    }

    [System.Serializable]
    public struct Entry {
        public string Name;
        public int Score;

        public Entry(string name, int score) {
            Name = name;
            Score = score;
        }

        public int CompareTo(Entry other) {
            return Score.CompareTo(other.Score);
        }

        public override string ToString() {
            return $"{Name} - {Score}";
        }
    }
}