using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FinishCanvas : MonoBehaviour
{
    [Header("Canvas Options")]
    [SerializeField]
    [Tooltip("Reference to the results text")]
    private Text finishText;
    [SerializeField]
    [Tooltip("Reference to the gameobject that handles the score save")]
    private GameObject highscoreGameObject;
    [SerializeField]
    [Tooltip("Reference to the gameobject that confirm the score save")]
    private GameObject saveConfirm;

    /// <summary>
    /// Reference to the score Manager
    /// </summary>
    private ScoreManager scoreMng;
    /// <summary>
    /// Reference to the leaderboard Manager
    /// </summary>
    private LeaderboardManager leaderboardMng;

    #region API
    /// <summary>
    /// Initialize this Canvas
    /// </summary>
    /// <param name="_lvlMng"></param>
    public void Init(LevelManager _lvlMng)
    {
        // both objects are set as false
        highscoreGameObject.SetActive(false);
        saveConfirm.SetActive(false);

        scoreMng = _lvlMng.GetScoreMng();
        leaderboardMng = _lvlMng.GetLeaderboardMng();

        _lvlMng.OnGameEnd += HandleOnGameEnd;

        gameObject.SetActive(false);
    }
    #endregion

    #region OnClick
    /// <summary>
    /// Go To Menu on click
    /// </summary>
    public void MainMenu()
    {
        GameManager.OnStateChange(GameState.MainMenu);
    }

    /// <summary>
    /// if the name inserted is valid, sale the score
    /// </summary>
    public void SaveScore()
    {
        InputField _input = highscoreGameObject.GetComponentInChildren<InputField>();

        if (_input.text != "")
        {
            leaderboardMng.NewEntry(_input.text, scoreMng.GetScore());

            // after saving, highscoreGO is disabled and the confirmation will appear
            highscoreGameObject.SetActive(false);
            saveConfirm.SetActive(true);

            _input.text = "";
        }
    }
    #endregion

    #region Handlers
    /// <summary>
    /// Update the results text with the game informations and display the highscoreGO if the score is high enough
    /// </summary>
    private void HandleOnGameEnd()
    {
        saveConfirm.SetActive(false);

        string _text = "Hai collezionato " + scoreMng.GetQuads() + " quadrati! \nIl tuo punteggio è " + scoreMng.GetScore();

        finishText.text = _text;

        highscoreGameObject.SetActive(leaderboardMng.CheckForHighscore(scoreMng.GetScore()));
    }
    #endregion
}
