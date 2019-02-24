using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class QuadManager : MonoBehaviour
{
    #region Delegates
    public delegate void PlaceQuadEvent(BaseQuad _base);
    public PlaceQuadEvent PlaceQuad;
    #endregion

    [Header("Pool Options")]
    [SerializeField]
    private int quantityPerPrefab;
    [SerializeField]
    private List<GameObject> poolPrefabs = new List<GameObject>();

    private List<BaseQuad> pool = new List<BaseQuad>();

    [Header("Scene Options")]
    [SerializeField]
    private int quadInScene;
    [SerializeField]
    private List<BaseQuad> activeQuads = new List<BaseQuad>();

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

    private bool placeEl(BaseQuad _el)
    {
        float screenX = Random.Range(5f, Camera.main.pixelWidth - 5f);
        float screenY = Random.Range(5f, Camera.main.pixelHeight - 5f);
        Vector3 point = Camera.main.ScreenToWorldPoint(new Vector3(screenX, screenY, 10));

        foreach (BaseQuad _temp in activeQuads)
        {
            Bounds _b = _temp.GetCollider().bounds;
            _b.Expand(1f);
            if (_b.Contains(point))
            {
                return false;
            }
        }

        _el.transform.position = point;
        _el.Move();
        return true;
    } 

    #region API
    public void Init(MouseManager _mouseMng)
    {
        GeneratePool();

        PopulateScene();

        _mouseMng.EndDrag += HandleEndDrag;
    }
    #endregion

    #region Pool
    private void GeneratePool()
    {
        var BottomLeft = Camera.main.ScreenToWorldPoint(Vector3.zero);
        var TopRight = Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth, Camera.main.pixelHeight));

        Rect _cameraBound = new Rect(BottomLeft.x, BottomLeft.y, TopRight.x - BottomLeft.x, TopRight.y - BottomLeft.y);

        foreach (GameObject _current in poolPrefabs)
        {
            for (int i = 0; i < quantityPerPrefab; i++)
            {
                GameObject _new = Instantiate(_current);
                _new.SetActive(false);
                _new.transform.position = new Vector3(1000, 1000, 1000);
                BaseQuad _newQuad = _new.GetComponent<BaseQuad>();
                _newQuad.Init(_cameraBound);
                pool.Add(_newQuad);

                _newQuad.OutOfBounds += HandleOutOfBounds;
            }
        }
    }

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
    private void HandleOutOfBounds(BaseQuad _base)
    {
        ReturnToPool(_base);
        StartCoroutine(CPlaceElement());
    }

    [SerializeField]
    List<BaseQuad> selected = new List<BaseQuad>();
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
        
        bool isValid = true;
        QuadType _target = QuadType.Red;
        List<BaseQuad> selected = new List<BaseQuad>();
        int score = 0;
        
        foreach (BaseQuad _current in activeQuads)
        {
            if (isValid && _selectionArea.Contains(_current.transform.position, true))
            {
                if (selected.Count == 0)
                {
                    _target = _current.GetQuadType();
                    selected.Add(_current);
                }
                else if (_current.GetQuadType() == _target)
                {
                    selected.Add(_current);
                }
                else
                {
                    isValid = false;
                }
            }
        }

        if (selected.Count == 1)
            isValid = false;

        if (isValid)
        {
            foreach (BaseQuad _current in selected)
            {
                score += _current.GetScore();
                ReturnToPool(_current);
                StartCoroutine(CPlaceElement());
            }

            Debug.Log(score);
        }
    }
    #endregion

    #region Coroutines
    private IEnumerator CPlaceElement()
    {
        BaseQuad _el = GetObjectFormPool(null);

        while (!placeEl(_el))
        {
            yield return null;
        }
        activeQuads.Add(_el);
    }
    #endregion

}
