﻿using UnityEngine;
using System.Collections;

public abstract class BaseQuad : MonoBehaviour
{
    #region Delegates
    public delegate void OutOfBoundsEvent(BaseQuad _base);

    /// <summary>
    /// Event called when this object goes out of bounds
    /// </summary>
    public OutOfBoundsEvent OutOfBounds;
    #endregion

    [Header("Quad Options")]
    [SerializeField]
    [Tooltip("Type of this quad")]
    protected QuadType type;
    [SerializeField]
    [Tooltip("movement Speed")]
    protected float movementSpd;
    [SerializeField]
    [Tooltip("Individual score for selecting this quad")]
    protected int score;

    /// <summary>
    /// movement direction of this quad
    /// </summary>
    protected Vector2 direction;
    /// <summary>
    /// Reference to this quad's collider
    /// </summary>
    protected Collider2D coll;
    /// <summary>
    /// Rect of the active camera
    /// </summary>
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
