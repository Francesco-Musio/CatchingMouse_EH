using UnityEngine;
using System.Collections;

public class RedQuad : BaseQuad
{

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.name);
        Debug.Log("lol");
        direction = Random.onUnitSphere;
    }

    #region API
    public override void Init()
    {
        coll = GetComponent<Collider2D>();
        direction = Random.onUnitSphere;
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
            yield return null;
        }
    }
    #endregion

    #region Test
    public void OOBTest()
    {
        OutOfBounds(this);
    }
    #endregion
}
