using UnityEngine;
using System.Collections;

public class YellowQuad : BaseQuad
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        direction = Random.onUnitSphere;
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
            yield return null;
        }
    }
    #endregion
}
