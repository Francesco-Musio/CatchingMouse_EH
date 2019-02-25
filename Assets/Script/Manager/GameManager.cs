using UnityEngine;
using System.Collections;

/*
 * Author: Francesco Musio
 * 
 * This class is a state machine that define the behaviour of the game in any given moment.
 */

public class GameManager : MonoBehaviour
{
    #region Delegates
    public delegate void GameStateEvent(GameState _target);

    /// <summary>
    /// Event called to change the state of the game.
    /// On every change the game will respond accordingly
    /// </summary>
    public static GameStateEvent OnStateChange;
    #endregion

    [Header("References")]
    [SerializeField]
    // Reference to the Level Manager
    private LevelManager levelMng;

    /// <summary>
    /// Current State of the Game
    /// </summary>
    private GameState currentState;

    #region API
    private void Start()
    {
        // Set the initial state of the machine
        currentState = GameState.Init;

        // initialize the level manager
        if (levelMng != null)
            levelMng.Init();

        // event subscribe
        OnStateChange += HandleOnStateChange;

        // initialization end. Change to main menu state
        OnStateChange(GameState.MainMenu);
    }
    #endregion

    #region Handlers
    /// <summary>
    /// Change the state of the game
    /// </summary>
    /// <param name="_target">state to reach</param>
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
            case GameState.Leaderboard:
                levelMng.OnLeaderboard();
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
    Finish,
    Leaderboard
}
