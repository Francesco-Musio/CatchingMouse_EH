using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

/*
 * Author: Francesco Musio
 * 
 * This class handle the leaderboard.
 */
 
public class LeaderboardManager : MonoBehaviour
{
    #region Delegates
    public delegate void NewEntryEvent(string _name, int _score);

    /// <summary>
    /// Event called every time a new score has to be saved
    /// </summary>
    public NewEntryEvent NewEntry;
    #endregion

    [Header("Leaderboard options")]
    [SerializeField]
    // max number of scores saved in the leaderboard
    private int maxEntries;
    [SerializeField]
    // indicate if there is a saved leaderboard
    private bool leaderboardPresence;

    /// <summary>
    /// current leaderboard taken from playerprefs
    /// </summary>
    private List<LeaderboardEntry> leaderboard = new List<LeaderboardEntry>();

    #region API
    /// <summary>
    /// Initialize the Leaderboard Manager
    /// </summary>
    public void Init()
    {
        // events subscribe
        NewEntry += HandleNewEntry;

        CheckLeaderboard();
    }

    /// <summary>
    /// Check if there is a leaderboard saved and, if there's one, 
    /// store it in the leaderboard variable
    /// </summary>
    public void CheckLeaderboard()
    {
        leaderboardPresence = false;

        // take the string from the playerprefs
        string _leaderboard = PlayerPrefs.GetString("leaderboard", "empty");

        if (_leaderboard == "empty")
        {
            leaderboardPresence = false;
            return;
        }

        // save data in LeaderboardData class
        LeaderboardData _temp = JsonUtility.FromJson<LeaderboardData>(_leaderboard);

        leaderboard = _temp.leaderboard;

        // sort the list by player position
        if (leaderboard.Count > 1)
            leaderboard.Sort((LeaderboardEntry a, LeaderboardEntry b) => a.position.CompareTo(b.position));

        leaderboardPresence = true;
    }

    /// <summary>
    /// Check if the passed score is a highscore
    /// </summary>
    /// <param name="_score">score to be checked</param>
    /// <returns>bool to signal if the score is highscore</returns>
    public bool CheckForHighscore(int _score)
    {
        // if there's less than the maxEntries the score is always an highscore
        if (leaderboard.Count + 1 <= maxEntries)
        {
            return true;
        }

        // if the score is higher of any current saved score, than it's an highscore
        foreach (LeaderboardEntry _current in leaderboard)
        {
            // if the score is the same as the last enry then it will not be saved
            if (_current.score <= _score && _current.position != 10)
            {
                return true;
            }
        }

        return false;
    }
    #endregion

    #region Handlers
    /// <summary>
    /// Handle the creation of a new Leaderboard entry
    /// </summary>
    /// <param name="_name"></param>
    /// <param name="_score"></param>
    private void HandleNewEntry(string _name, int _score)
    {
        LeaderboardEntry _newEntry = new LeaderboardEntry();
        _newEntry.position = 1;
        _newEntry.name = _name;
        _newEntry.score = _score;

        // if the leaderboard is empty
        if (leaderboard.Count == 0)
        {
            _newEntry.position = 1;
        }
        else
        {
            // the position increment by 1 every time ther's an higher score saved
            foreach (LeaderboardEntry _current in leaderboard)
            {
                if (_current.score >= _score)
                {
                    _newEntry.position++;
                }
            }

            // if the position is higher than the maxEntries it can't be saved
            if (_newEntry.position == maxEntries + 1)
            {
                return;
            }

            // Rearrange the list with the new positions and remove the last element if present
            List<LeaderboardEntry> _temp = new List<LeaderboardEntry>();
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
        
        // add the new element to the leaderboard list
        leaderboard.Add(_newEntry);

        // save the list in playerprefs
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
