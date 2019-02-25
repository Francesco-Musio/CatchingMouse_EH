using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FinishCanvas : MonoBehaviour
{
    [Header("Canvas Options")]
    [SerializeField]
    private Text finishText;

    private ScoreManager scoreMng;

    #region API
    public void Init(LevelManager _lvlMng)
    {
        scoreMng = _lvlMng.GetScoreMng();

        _lvlMng.OnGameEnd += HandleOnGameEnd;

        gameObject.SetActive(false);
    }
    #endregion

    #region OnClick
    public void MainMenu()
    {
        GameManager.OnStateChange(GameState.MainMenu);
    }
    #endregion

    #region Handlers
    private void HandleOnGameEnd()
    {
        string _text = "Hai collezionato " + scoreMng.GetQuads() + " quadrati! \nIl tuo punteggio è " + scoreMng.GetScore();

        finishText.text = _text;
    }
    #endregion
}
