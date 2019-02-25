﻿using UnityEngine;
using System.Collections.Generic;
using System.Collections;

/*
 * Author: Francesco Musio
 * 
 * This class handles the creation of all the quads.
 * It contains a pool from which the quads are taken
 */

public class QuadManager : MonoBehaviour
{
    [Header("Pool Options")]
    [SerializeField]
    // number of object that has to be instantiated for each prefab
    private int quantityPerPrefab;
    [SerializeField]
    // list that contains all the quad prefabs
    private List<GameObject> poolPrefabs = new List<GameObject>();

    /// <summary>
    /// Object Pool
    /// </summary>
    private List<BaseQuad> pool = new List<BaseQuad>();

    [Header("Scene Options")]
    [SerializeField]
    // number of objects that has to be in scene
    private int quadInScene;
    [SerializeField]
    // list with all the quads in scene
    private List<BaseQuad> activeQuads = new List<BaseQuad>();
    [SerializeField]
    // reference to the quad container
    private Transform quadContainer;

    /// <summary>
    /// Refence to the Score Manager
    /// </summary>
    private ScoreManager scoreMng;

    /// <summary>
    /// Populate the scene with quads
    /// </summary>
    private void PopulateScene()
    {
        
        foreach (GameObject _current in poolPrefabs)
        {
            for (int i = 0; i < quadInScene / 3; i++)
            {
                BaseQuad _el = GetObjectFormPool(_current.GetComponent<BaseQuad>().GetQuadType());

                if (!placeEl(_el))
                {
                    i--;
                }
                else
                {
                    activeQuads.Add(_el);
                }
            }
        }

        /*
        // DEBUG only red
        GameObject _current = poolPrefabs[0];
        for (int i = 0; i < quadInScene / 3; i++)
        {
            BaseQuad _el = GetObjectFormPool(_current.GetComponent<BaseQuad>().GetQuadType());

            if (!placeEl(_el))
            {
                i--;
            }
            else
            {
                activeQuads.Add(_el);
            }
        }*/
    }

    /// <summary>
    /// Place an element in scene
    /// </summary>
    /// <param name="_el">element to place</param>
    /// <returns>true if the quad has been successfully placed</returns>
    private bool placeEl(BaseQuad _el)
    {
        // get a random position
        float screenX = Random.Range(5f, Camera.main.pixelWidth - 5f);
        float screenY = Random.Range(5f, Camera.main.pixelHeight - 5f);
        Vector3 point = Camera.main.ScreenToWorldPoint(new Vector3(screenX, screenY, 10));

        // check if there are intersection with existing quads
        foreach (BaseQuad _temp in activeQuads)
        {
            Bounds _b = _temp.GetCollider().bounds;
            _b.Expand(1f);
            if (_b.Contains(point))
            {
                return false;
            }
        }

        // set the position
        _el.transform.position = point;
        // start moving
        _el.Move();
        return true;
    } 

    #region API
    /// <summary>
    /// Initialize this manager
    /// </summary>
    /// <param name="_lvlMng"></param>
    public void Init(LevelManager _lvlMng)
    {
        scoreMng = _lvlMng.GetScoreMng();

        GeneratePool();

        PopulateScene();

        _lvlMng.GetMouseMng().EndDrag += HandleEndDrag;
    }
    #endregion

    #region Pool
    /// <summary>
    /// Generate all objects that has to be placed in the pool
    /// </summary>
    private void GeneratePool()
    {
        // get the camera rect
        var BottomLeft = Camera.main.ScreenToWorldPoint(Vector3.zero);
        var TopRight = Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth, Camera.main.pixelHeight));

        Rect _cameraBound = new Rect(BottomLeft.x, BottomLeft.y, TopRight.x - BottomLeft.x, TopRight.y - BottomLeft.y);

        // instantiate objects
        foreach (GameObject _current in poolPrefabs)
        {
            for (int i = 0; i < quantityPerPrefab; i++)
            {
                GameObject _new = Instantiate(_current, quadContainer, true);
                _new.SetActive(false);
                _new.transform.position = new Vector3(1000, 1000, 1000);
                BaseQuad _newQuad = _new.GetComponent<BaseQuad>();
                _newQuad.Init(_cameraBound);
                pool.Add(_newQuad);

                _newQuad.OutOfBounds += HandleOutOfBounds;
            }
        }
    }

    /// <summary>
    /// Get object of a specific type from the pool
    /// if the parameter is null, a ramdom object will be returned
    /// </summary>
    /// <param name="_type"></param>
    /// <returns></returns>
    private BaseQuad GetObjectFormPool(QuadType? _type)
    {
        foreach (BaseQuad _current in pool)
        {
            if (!_current.gameObject.activeInHierarchy)
            {
                if (_type != null)
                {
                    if (_current.GetQuadType() == _type)
                    {
                        _current.gameObject.SetActive(true);
                        return _current;
                    }
                }
                else
                {
                    _current.gameObject.SetActive(true);
                    return _current;
                }
            }
        }

        return null;
    }

    /// <summary>
    /// Return an objects to the pool
    /// </summary>
    /// <param name="_quad"></param>
    private void ReturnToPool(BaseQuad _quad)
    {
        if (activeQuads.Contains(_quad))
        {
            activeQuads.Remove(_quad);
            _quad.transform.position = new Vector3(1000, 1000, 1000);
            _quad.gameObject.SetActive(false);
        }
    }
    #endregion

    #region Handlers
    /// <summary>
    /// If an element fall out of bounds a new element will be spawned
    /// </summary>
    /// <param name="_base"></param>
    private void HandleOutOfBounds(BaseQuad _base)
    {
        ReturnToPool(_base);
        StartCoroutine(CPlaceElement(_base.GetQuadType()));
    }
    
    /// <summary>
    /// Handle the end of mouse drag
    /// </summary>
    /// <param name="_selectionArea"></param>
    private void HandleEndDrag(Rect _selectionArea)
    {
        /*
        // Stampa quadrati blu nell'area di selezione
        // DEBUG
        Instantiate(poolPrefabs[1], new Vector3(_selectionArea.xMin, _selectionArea.yMin, 0), Quaternion.identity);
        Instantiate(poolPrefabs[1], new Vector3(_selectionArea.xMax, _selectionArea.yMin, 0), Quaternion.identity);
        Instantiate(poolPrefabs[1], new Vector3(_selectionArea.xMin, _selectionArea.yMax, 0), Quaternion.identity);
        Instantiate(poolPrefabs[1], new Vector3(_selectionArea.xMax, _selectionArea.yMax, 0), Quaternion.identity);
        */
        
        // signal if the selection is valid
        bool isValid = true;
        // target color for the selection
        QuadType _target = QuadType.Red;
        // list that contains the quads in the selection
        List<BaseQuad> selected = new List<BaseQuad>();
        // score obtaained with the selection if valid
        int score = 0;
        
        // identify wich quads are in the selection
        foreach (BaseQuad _current in activeQuads)
        {
            if (isValid && _selectionArea.Contains(_current.transform.position, true))
            {
                // set the target quad type
                if (selected.Count == 0)
                {
                    _target = _current.GetQuadType();
                    selected.Add(_current);
                }
                else if (_current.GetQuadType() == _target)
                {
                    selected.Add(_current);
                }
                // if there's a quad with a different type the selection is not valid
                else
                {
                    isValid = false;
                }
            }
        }

        // if there's only 1 quad in the selection, then the selection is not valid
        if (selected.Count == 1)
            isValid = false;

        if (isValid)
        {
            // count the score and reposition the selected quads
            foreach (BaseQuad _current in selected)
            {
                score += _current.GetScore();
                ReturnToPool(_current);
                StartCoroutine(CPlaceElement(_current.GetQuadType()));
            }

            // obtain the score
            scoreMng.Score(score, selected.Count);
        }
    }
    #endregion

    #region Coroutines
    /// <summary>
    /// Place a quad in scene in a point where it does not overlap with the ones already in scene
    /// </summary>
    /// <param name="_type"></param>
    /// <returns></returns>
    private IEnumerator CPlaceElement(QuadType? _type)
    {
        BaseQuad _el = GetObjectFormPool(_type);

        while (!placeEl(_el))
        {
            yield return null;
        }
        activeQuads.Add(_el);
    }
    #endregion

}
