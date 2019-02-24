using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseManager : MonoBehaviour
{
    #region Delegates
    public delegate void EndDragEvent(Rect _selectionArea);
    public EndDragEvent EndDrag;
    #endregion

    [SerializeField]
    private Texture texture;

    [SerializeField]
    private bool isDragging;
    private Vector2 startingPoint;
    private Vector2 endPoint;

    Rect _selection = Rect.zero;
    private void RectUpdate(Vector2 _start, Vector2 _end)
    {
        _selection = Rect.zero;

        /*
        if (_start.x < _end.x && _start.y > _end.y)
        {
            _selection = new Rect(_start.x, _start.y, _end.x - _start.x, _start.y - _end.y);
        }
        else if (_end.x < _start.x && _end.y > _start.y)
        {
            _selection = new Rect(_end.x, _end.y, _start.x - _end.x, _end.y - _start.y);
        }
        else if (_start.x < _end.x && _start.y < _end.y)
        {
            _selection = new Rect(_start.x, _end.y, _end.x - _start.x, _end.y - _start.y);
        }
        else if (_end.x < _start.x && _end.y < _start.y)
        {
            _selection = new Rect(_end.x, _start.y, _start.x - _end.x, _start.y - _end.y);
        }*/

        _selection = new Rect(_start.x, Screen.height - _start.y, _end.x - _start.x, _start.y - _end.y);
        
        //EndDrag?.Invoke(_selection);
    }

    private void OnGUI()
    {
        if (isDragging)
        {
            GUI.DrawTexture(_selection, texture);
        }
    }

    #region API
    public void Init()
    {
        isDragging = false;
        startingPoint = Vector3.zero;

        StartCoroutine(MouseCheck());
    }
    #endregion

    #region Coroutines
    private IEnumerator MouseCheck()
    {
        // insert start conditions
        while (true)
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
            }

            if (isDragging)
            {
                //endPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                endPoint = Input.mousePosition;
                RectUpdate(startingPoint, endPoint);
            }

            yield return null;
        }
    }
    #endregion

}
