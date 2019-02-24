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

    protected Vector2 direction;
    protected Collider2D coll;

    #region Abstract
    public abstract void Init();
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
    #endregion
}

public enum QuadType
{
    Red,
    Yellow,
    Blue
}
