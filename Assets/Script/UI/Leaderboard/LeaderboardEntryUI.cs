using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LeaderboardEntryUI : MonoBehaviour
{
    [Header("Leaderboard Entry UI Settings")]
    [SerializeField]
    [Tooltip("Reference to the position text")]
    private Text position;
    [SerializeField]
    [Tooltip("Reference to the name text")]
    private Text name;
    [SerializeField]
    [Tooltip("Reference to the score text")]
    private Text score;

    #region API
    /// <summary>
    /// Load the information on this object
    /// </summary>
    /// <param name="_position"></param>
    /// <param name="_name"></param>
    /// <param name="_score"></param>
    public void Setup(int _position, string _name, int _score)
    {
        position.text = _position.ToString();
        name.text = _name;
        score.text = _score.ToString();
    }
    #endregion
}
