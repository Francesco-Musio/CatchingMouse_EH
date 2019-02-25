using UnityEngine;
using System.Collections;

public class YellowQuad : BaseQuad
{
    bool stillInContact = false;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        stillInContact = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        stillInContact = false;
    }

    #region API
    public override void Init(Rect _cameraRect)
    {
        coll = GetComponent<Collider2D>();
        direction = Random.onUnitSphere;

        cameraRect = _cameraRect;
    }

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
            transform.Translate(direction * movementSpd * Time.deltaTime);
            if (!(cameraRect.Contains(transform.position)))
                OutOfBounds(this);

            if (stillInContact)
            {
                direction = Random.onUnitSphere;
            }

            yield return null;
        }
    }
    #endregion
}
