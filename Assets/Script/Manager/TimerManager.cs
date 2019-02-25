using UnityEngine;
using System.Collections;

public class TimerManager : MonoBehaviour
{
    #region Delegates
    public delegate void TimeEvent(float _timer);

    /// <summary>
    /// Event called every time the timer gets updated
    /// </summary>
    public TimeEvent TimeUpdate;
    #endregion

    [Header("LevelOption")]
    [SerializeField]
    // duration of the game
    private int gameTime = 30;

    /// <summary>
    /// timer goes from gameTime to 0
    /// </summary>
    private float timer = 0;

    /// <summary>
    /// when true, the timer start decreasing
    /// </summary>
    private bool isGameActive;

    #region API
    /// <summary>
    /// Setup of this manager
    /// </summary>
    /// <param name="_lvlMng"></param>
    public void Init(LevelManager _lvlMng)
    {
        isGameActive = false;

        StartCoroutine(CGameTimer());

        _lvlMng.OnGameStart += HandleOnGameStart;
        _lvlMng.OnGameEnd += HandleOnGameEnd;
    }
    #endregion
    
    #region Coroutines
    /// <summary>
    /// When the game starts decrease the timer every fixed update
    /// </summary>
    /// <returns></returns>
    private IEnumerator CGameTimer()
    {
        while (true)
        {
            yield return new WaitForFixedUpdate();
            if (isGameActive)
            {
                timer -= Time.fixedDeltaTime;
                if (timer <= 0)
                {
                    GameManager.OnStateChange(GameState.Finish);
                }

                TimeUpdate(timer);
            }
        }
    }
    #endregion

    #region Handlers
    /// <summary>
    /// Set the timer equal to gametime and start counting
    /// </summary>
    private void HandleOnGameStart()
    {
        timer = gameTime;
        isGameActive = true;
    }

    /// <summary>
    /// end of the game
    /// </summary>
    private void HandleOnGameEnd()
    {
        isGameActive = false;
    }
    #endregion
}
