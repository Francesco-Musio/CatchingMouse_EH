using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MouseManager))]
[RequireComponent(typeof(QuadManager))]
public class LevelManager : MonoBehaviour
{

    private MouseManager mouseMng;

    private QuadManager quadMng;

    #region Start
    private void Start()
    {
        mouseMng = GetComponent<MouseManager>();
        if (mouseMng != null)
            mouseMng.Init();

        quadMng = GetComponent<QuadManager>();
        if (quadMng != null)
            quadMng.Init(mouseMng);
    }
    #endregion

}
