using UnityEngine;
using System.Collections;

public abstract class BaseQuad : MonoBehaviour
{
    #region Delegates
    public delegate void OutOfBoundsEvent(BaseQuad _base);
    public OutOfBoundsEvent OutOfBounds;
    #endregion

    [Header("Quad Options")]
    [SerializeField]
    protected QuadType type;
    [SerializeField]
    protected float movementSpd;
    [SerializeField]
    protected int score = 100;

    protected Vector2 direction;
    protected Collider2D coll;
    protected Rect cameraRect;

    #region Abstract
    public abstract void Init(Rect _cameraRect);
    public abstract void Move();
    #endregion

    #region Getters
    public QuadType GetQuadType()
    {
        return type;
    }

    public Collider2D GetCollider()
    {
        return coll;
    }

    public int GetScore()
    {
        return score;
    }
    #endregion
}

public enum QuadType
{
    Red,
    Yellow,
    Blue
}
