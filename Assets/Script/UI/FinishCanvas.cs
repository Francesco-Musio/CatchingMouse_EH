using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FinishCanvas : MonoBehaviour
{
    [Header("Canvas Options")]
    [SerializeField]
    private Text finishText;
    [SerializeField]
    private GameObject highscoreGameObject;
    [SerializeField]
    private GameObject saveConfirm;

    private ScoreManager scoreMng;

    private LeaderboardManager leaderboardMng;

    #region API
    public void Init(LevelManager _lvlMng)
    {
        highscoreGameObject.SetActive(false);
        saveConfirm.SetActive(false);

        scoreMng = _lvlMng.GetScoreMng();
        leaderboardMng = _lvlMng.GetLeaderboardMng();

        _lvlMng.OnGameEnd += HandleOnGameEnd;

        gameObject.SetActive(false);
    }
    #endregion

    #region OnClick
    public void MainMenu()
    {
        GameManager.OnStateChange(GameState.MainMenu);
    }

    public void SaveScore()
    {
        InputField _input = highscoreGameObject.GetComponentInChildren<InputField>();

        if (_input.text != "")
        {
            leaderboardMng.NewEntry(_input.text, scoreMng.GetScore());

            highscoreGameObject.SetActive(false);
            saveConfirm.SetActive(true);

            _input.text = "";
        }
    }
    #endregion

    #region Handlers
    private void HandleOnGameEnd()
    {
        saveConfirm.SetActive(false);

        string _text = "Hai collezionato " + scoreMng.GetQuads() + " quadrati! \nIl tuo punteggio è " + scoreMng.GetScore();

        finishText.text = _text;

        highscoreGameObject.SetActive(leaderboardMng.CheckForHighscore(scoreMng.GetScore()));
    }
    #endregion
}
