using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class LeaderboardManager : MonoBehaviour
{
    #region Delegates
    public delegate void NewEntryEvent(string _name, int _score);
    public NewEntryEvent NewEntry;
    #endregion

    [Header("Leaderboard options")]
    [SerializeField]
    private int maxEntries;
    [SerializeField]
    private bool leaderboardPresence;

    private List<LeaderboardEntry> leaderboard = new List<LeaderboardEntry>();

    #region API
    public void Init()
    {
        NewEntry += HandleNewEntry;

        CheckLeaderboard();
    }

    public void CheckLeaderboard()
    {
        leaderboardPresence = false;

        string _leaderboard = PlayerPrefs.GetString("leaderboard", "empty");

        if (_leaderboard == "empty")
        {
            leaderboardPresence = false;
            return;
        }

        LeaderboardData _temp = JsonUtility.FromJson<LeaderboardData>(_leaderboard);

        leaderboard = _temp.leaderboard;

        if (leaderboard.Count > 1)
            leaderboard.Sort((LeaderboardEntry a, LeaderboardEntry b) => a.position.CompareTo(b.position));

        leaderboardPresence = true;
    }

    public bool CheckForHighscore(int _score)
    {
        if (leaderboard.Count + 1 <= maxEntries)
        {
            return true;
        }

        foreach (LeaderboardEntry _current in leaderboard)
        {
            if (_current.score <= _score && _current.position != 10)
            {
                return true;
            }
        }

        return false;
    }
    #endregion

    #region Handlers
    private void HandleNewEntry(string _name, int _score)
    {
        LeaderboardEntry _newEntry = new LeaderboardEntry();
        _newEntry.position = 1;
        _newEntry.name = _name;
        _newEntry.score = _score;

        if (leaderboard.Count == 0)
        {
            _newEntry.position = 1;
        }
        else
        {
            foreach (LeaderboardEntry _current in leaderboard)
            {
                if (_current.score >= _score)
                {
                    _newEntry.position++;
                }
            }

            if (_newEntry.position == maxEntries + 1)
            {
                return;
            }

            List<LeaderboardEntry> _temp = new List<LeaderboardEntry>();
            LeaderboardEntry _toRemove;
            foreach (LeaderboardEntry _current in leaderboard)
            {
                if(_current.position < _newEntry.position)
                {
                    _temp.Add(_current);
                }
                else
                {
                    _current.position++;
                    if (_current.position != maxEntries + 1)
                    {
                        _temp.Add(_current);
                    }
                }
            }

            leaderboard = _temp;
        }
        

        leaderboard.Add(_newEntry);

        string _toSave = JsonUtility.ToJson(new LeaderboardData(leaderboard));
        PlayerPrefs.SetString("leaderboard", _toSave);
        PlayerPrefs.Save();
    }
    #endregion

    #region Getters
    public bool GetLeaderboardPresence()
    {
        return leaderboardPresence;
    }

    public List<LeaderboardEntry> GetLeaderboard()
    {
        return leaderboard;
    }
    #endregion
}

[Serializable]
public class LeaderboardEntry
{
    public int position;
    public string name;
    public int score;
}

[Serializable]
public class LeaderboardData
{
    public List<LeaderboardEntry> leaderboard = new List<LeaderboardEntry>();

    public LeaderboardData(List<LeaderboardEntry> _leaderboard)
    {
        leaderboard = _leaderboard;
    }
}
