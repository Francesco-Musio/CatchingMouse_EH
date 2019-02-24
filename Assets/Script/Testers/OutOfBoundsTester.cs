using UnityEngine;
using System.Collections;

public class OutOfBoundsTester : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            SendMessage("OOBTest");
        }
    }
}
