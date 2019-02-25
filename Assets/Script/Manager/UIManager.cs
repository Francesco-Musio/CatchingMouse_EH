using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Canvas References")]
    [SerializeField]
    private GameCanvas gameCanvas;
    [SerializeField]
    private MenuCanvas menuCanvas;
    [SerializeField]
    private FinishCanvas finishCanvas;

    #region API
    public void Init(LevelManager _lvlMng)
    {
        if (gameCanvas != null)
            gameCanvas.Init(_lvlMng);

        if (finishCanvas != null)
            finishCanvas.Init(_lvlMng);

        _lvlMng.OnMenuEnter += HandleOnMenuEnter;
        _lvlMng.OnGameStart += HandleOnGameStart;
        _lvlMng.OnGameEnd += HandleOngameEnd;
    }
    #endregion

    #region Handlers
    private void HandleOnMenuEnter()
    {
        menuCanvas.gameObject.SetActive(true);
        gameCanvas.gameObject.SetActive(false);
        finishCanvas.gameObject.SetActive(false);
    }

    private void HandleOnGameStart()
    {
        menuCanvas.gameObject.SetActive(false);
        finishCanvas.gameObject.SetActive(false);
        gameCanvas.gameObject.SetActive(true);
    }

    private void HandleOngameEnd()
    {
        gameCanvas.gameObject.SetActive(false);
        menuCanvas.gameObject.SetActive(false);
        finishCanvas.gameObject.SetActive(true);
    }
    #endregion
}
