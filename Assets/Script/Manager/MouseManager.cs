using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Author: Francesco Musio
 * 
 * This class handles the selection area created by the mouse
 */

public class MouseManager : MonoBehaviour
{
    #region Delegates
    public delegate void EndDragEvent(Rect _selectionArea);

    /// <summary>
    /// Event called when the mouse drag ends
    /// </summary>
    public EndDragEvent EndDrag;
    #endregion

    [Header("Hover Options")]
    [SerializeField]
    // texture used by rect onscreen
    private Texture texture;

    /// <summary>
    /// signals if this class should listen to mouse inputs
    /// </summary>
    private bool isGameActive;
    
    /// <summary>
    /// true if the mouse button is been kept pressed
    /// </summary>
    private bool isDragging;

    /// <summary>
    /// Starting point of the drag
    /// </summary>
    private Vector2 startingPoint;

    /// <summary>
    /// end point of the drag
    /// </summary>
    private Vector2 endPoint;

    /// <summary>
    /// Rect with selection area in screen space
    /// </summary>
    Rect _selection = Rect.zero;

    /// <summary>
    /// Rect with selection area in world space
    /// </summary>
    Rect _worldSelection = Rect.zero;

    /// <summary>
    /// Reload the two rects with the current positions
    /// </summary>
    /// <param name="_start"></param>
    /// <param name="_end"></param>
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

    /// <summary>
    /// show the selection area on screen
    /// </summary>
    private void OnGUI()
    {
        if (isDragging)
        {
            GUI.DrawTexture(_selection, texture);
        }
    }

    #region API
    /// <summary>
    /// initialize this manager
    /// </summary>
    /// <param name="_lvlMng"></param>
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
        while (true)
        {
            if (isGameActive)
            {
                // check if the user start dragging
                if (!isDragging && Input.GetMouseButtonDown(0))
                {
                    isDragging = true;
                    startingPoint = Input.mousePosition;
                }
                // check if the user has stopped dragging
                else if (Input.GetMouseButtonUp(0))
                {
                    isDragging = false;
                    RectUpdate(startingPoint, endPoint);

                    EndDrag?.Invoke(_worldSelection);
                }

                // if is dragging update the selection area
                if (isDragging)
                {
                    endPoint = Input.mousePosition;
                    RectUpdate(startingPoint, endPoint);
                }
            }
            
            yield return null;
        }
    }
    #endregion

    #region Handlers
    /// <summary>
    /// Set the game as active
    /// </summary>
    private void HandleOnGameStart()
    {
        _selection = Rect.zero;
        _worldSelection = Rect.zero;
        isGameActive = true;
    }

    /// <summary>
    /// Set the game as inactive
    /// </summary>
    private void HandleOnGameEnd()
    {
        _selection = Rect.zero;
        _worldSelection = Rect.zero;
        isGameActive = false;
    }
    #endregion

}
