using UnityEngine;
using System.Collections;

public class BlueQuad : BaseQuad
{
    #region API
    public override void Init()
    {
        coll = GetComponent<Collider2D>();
    }

    public override void Move()
    {
    }
    #endregion
}
