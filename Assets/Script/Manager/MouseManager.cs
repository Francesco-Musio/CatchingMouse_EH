using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseManager : MonoBehaviour
{
    #region Delegates
    public delegate void EndDragEvent(Rect _selectionArea);
    public EndDragEvent EndDrag;
    #endregion

    [Header("Hover Options")]
    [SerializeField]
    private Texture texture;

    private bool isGameActive;
    
    private bool isDragging;
    private Vector2 startingPoint;
    private Vector2 endPoint;

    Rect _selection = Rect.zero;
    Rect _worldSelection = Rect.zero;

    private void RectUpdate(Vector2 _start, Vector2 _end)
    {
        _selection = Rect.zero;

        _selection = new Rect(_start.x, Screen.height - _start.y, _end.x - _start.x, _start.y - _end.y);
        
        _start = Camera.main.ScreenToWorldPoint(_start);
        _end = Camera.main.ScreenToWorldPoint(_end);

        if (_start.x < _end.x && _start.y > _end.y)
        {
            _worldSelection = new Rect(_start.x, _start.y, _end.x - _start.x, -(_start.y - _end.y));
        }
        else if (_end.x < _start.x && _end.y > _start.y)
        {
            _worldSelection = new Rect(_end.x, _end.y, _start.x - _end.x, -(_end.y - _start.y));
        }
        else if (_start.x < _end.x && _start.y < _end.y)
        {
            _worldSelection = new Rect(_start.x, _end.y, _end.x - _start.x, -(_end.y - _start.y));
        }
        else if (_end.x < _start.x && _end.y < _start.y)
        {
            _worldSelection = new Rect(_end.x, _start.y, _start.x - _end.x, -(_start.y - _end.y));
        }

    }

    private void OnGUI()
    {
        if (isDragging)
        {
            GUI.DrawTexture(_selection, texture);
        }
    }

    #region API
    public void Init(LevelManager _lvlMng)
    {
        isDragging = false;
        startingPoint = Vector3.zero;

        isGameActive = false;
        StartCoroutine(MouseCheck());

        _lvlMng.OnGameStart += HandleOnGameStart;
        _lvlMng.OnGameEnd += HandleOnGameEnd;
    }
    #endregion

    #region Coroutines
    private IEnumerator MouseCheck()
    {
        // insert start conditions
        while (true)
        {
            if (isGameActive)
            {
                if (!isDragging && Input.GetMouseButtonDown(0))
                {
                    isDragging = true;
                    //startingPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    startingPoint = Input.mousePosition;
                }
                else if (Input.GetMouseButtonUp(0))
                {
                    isDragging = false;
                    RectUpdate(startingPoint, endPoint);

                    EndDrag?.Invoke(_worldSelection);
                }

                if (isDragging)
                {
                    //endPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    endPoint = Input.mousePosition;
                    RectUpdate(startingPoint, endPoint);
                }
            }
            
            yield return null;
        }
    }
    #endregion

    #region Handlers
    private void HandleOnGameStart()
    {
        _selection = Rect.zero;
        _worldSelection = Rect.zero;
        isGameActive = true;
    }

    private void HandleOnGameEnd()
    {
        _selection = Rect.zero;
        _worldSelection = Rect.zero;
        isGameActive = false;
    }
    #endregion

}
