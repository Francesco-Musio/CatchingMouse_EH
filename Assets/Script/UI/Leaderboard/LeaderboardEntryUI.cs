using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LeaderboardEntryUI : MonoBehaviour
{
    [Header("Leaderboard Entry UI Settings")]
    [SerializeField]
    private Text position;
    [SerializeField]
    private Text name;
    [SerializeField]
    private Text score;

    #region API
    public void Setup(int _position, string _name, int _score)
    {
        position.text = _position.ToString();
        name.text = _name;
        score.text = _score.ToString();
    }
    #endregion
}
