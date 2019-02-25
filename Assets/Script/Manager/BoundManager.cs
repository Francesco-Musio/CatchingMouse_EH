using UnityEngine;
using System.Collections;

/*
 * Author: Francesco Musio
 * 
 * This class sets up the bounds of the scene, depending on the camera
 */

public class BoundManager : MonoBehaviour
{
    [Header("Bound Manager Options")]
    [SerializeField]
    [Tooltip("Reference to the upper collider")]
    private BoxCollider2D upperCollider;
    [SerializeField]
    [Tooltip("Reference to the right collider")]
    private BoxCollider2D rightCollider;
    [SerializeField]
    [Tooltip("Reference to the left collider")]
    private BoxCollider2D leftCollider;
    [SerializeField]
    [Tooltip("Reference to the bottom collider")]
    private BoxCollider2D bottomCollider;

    /// <summary>
    /// Rect that indicates the camera bounds
    /// </summary>
    private Rect cameraBound;
    /// <summary>
    /// Position of the main camera
    /// </summary>
    private Vector3 cameraPosition;

    /// <summary>
    /// Position the colliders on the edge of the camera
    /// </summary>
    private void SetupBounds()
    {
        upperCollider.transform.position = upperCollider.transform.position + new Vector3(cameraPosition.x, cameraBound.yMax + upperCollider.bounds.extents.y, 0);
        bottomCollider.transform.position = bottomCollider.transform.position + new Vector3(cameraPosition.x, cameraBound.yMin - bottomCollider.bounds.extents.y, 0);
        rightCollider.transform.position = rightCollider.transform.position + new Vector3(cameraBound.xMax + rightCollider.bounds.extents.x, cameraPosition.y, 0);
        leftCollider.transform.position = leftCollider.transform.position + new Vector3(cameraBound.xMin - leftCollider.bounds.extents.x, cameraPosition.y, 0);
    }

    #region API
    /// <summary>
    /// Initialize this manager
    /// </summary>
    public void Init()
    {
        // get the camera rect
        var BottomLeft = Camera.main.ScreenToWorldPoint(Vector3.zero);
        var TopRight = Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth, Camera.main.pixelHeight));

        cameraBound = new Rect(BottomLeft.x, BottomLeft.y, TopRight.x - BottomLeft.x, TopRight.y - BottomLeft.y);

        cameraPosition = cameraBound.center;

        SetupBounds();
    }
    #endregion

    #region Getters
    public Rect GetCameraBound()
    {
        return cameraBound;
    }
    #endregion
}
