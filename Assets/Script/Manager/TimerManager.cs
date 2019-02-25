using UnityEngine;
using System.Collections;

public class TimerManager : MonoBehaviour
{
    #region Delegates
    public delegate void TimeEvent(float _timer);
    public TimeEvent TimeUpdate;
    #endregion

    [Header("LevelOption")]
    [SerializeField]
    private int gameTime = 30;

    private float timer = 0;
    private bool isGameActive;

    #region API
    public void Init(LevelManager _lvlMng)
    {
        isGameActive = false;

        StartCoroutine(CGameTimer());

        _lvlMng.OnGameStart += HandleOnGameStart;
        _lvlMng.OnGameEnd += HandleOnGameEnd;
    }
    #endregion
    
    #region Coroutines
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
    private void HandleOnGameStart()
    {
        timer = gameTime;
        isGameActive = true;
    }

    private void HandleOnGameEnd()
    {
        isGameActive = false;
    }
    #endregion
}
