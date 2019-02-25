using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    #region Delegates
    public delegate void GameStateEvent(GameState _target);
    public static GameStateEvent OnStateChange;
    #endregion

    [Header("References")]
    [SerializeField]
    private LevelManager levelMng;

    private GameState currentState;

    #region API
    private void Start()
    {
        currentState = GameState.Init;

        if (levelMng != null)
            levelMng.Init();

        OnStateChange += HandleOnStateChange;

        OnStateChange(GameState.MainMenu);
    }
    #endregion

    #region Handlers
    private void HandleOnStateChange(GameState _target)
    {
        currentState = _target;
        switch (currentState)   
        {
            case GameState.MainMenu:
                levelMng.OnMenuEnter();
                break;
            case GameState.Game:
                levelMng.OnGameStart();
                break;
            case GameState.Finish:
                levelMng.OnGameEnd();
                break;
            default:
                break;
        }
    }
    #endregion
}

public enum GameState
{
    Init,
    MainMenu,
    Game,
    Finish
}
