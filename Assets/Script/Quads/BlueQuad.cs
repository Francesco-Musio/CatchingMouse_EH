using UnityEngine;
using System.Collections;

public class BlueQuad : BaseQuad
{
    /// <summary>
    /// Signal collision is in act
    /// </summary>
    bool stillInContact = false;

    /// <summary>
    /// Signal collision has begun
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        stillInContact = true;
    }

    /// <summary>
    /// Signal collision has ended
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionExit2D(Collision2D collision)
    {
        stillInContact = false;
    }

    #region API
    /// <summary>
    /// Initialize this quad
    /// </summary>
    /// <param name="_cameraRect"></param>
    public override void Init(Rect _cameraRect)
    {
        coll = GetComponent<Collider2D>();
        direction = Random.onUnitSphere;

        cameraRect = _cameraRect;
    }

    /// <summary>
    /// Start Movement Coroutine
    /// </summary>
    public override void Move()
    {
        StartCoroutine(CMove());
    }
    #endregion

    #region Coroutines
    private IEnumerator CMove()
    {
        while (true)
        {
            // move in the selected direction
            transform.Translate(direction * movementSpd * Time.deltaTime);

            // if the quad goes out of bound, call OutOfBound Event
            if (!(cameraRect.Contains(transform.position)))
                OutOfBounds(this);

            // while in contact with something get a new random direction
            if (stillInContact)
            {
                direction = Random.onUnitSphere;
            }

            yield return null;
        }
    }
    #endregion
}
