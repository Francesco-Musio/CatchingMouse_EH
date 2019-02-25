using UnityEngine;
using System.Collections;

public class MenuCanvas : MonoBehaviour
{
    #region API
    private void Init()
    {
        this.gameObject.SetActive(false);
    }
    #endregion

    #region OnClick
    public void StartGame()
    {
        GameManager.OnStateChange(GameState.Game);
    }
    #endregion
}
