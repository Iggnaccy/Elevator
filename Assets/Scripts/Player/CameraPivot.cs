using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Used to control the rotation of camera following the player
/// </summary>
public class CameraPivot : MonoBehaviour
{
    [SerializeField] private Transform cameraParent;

    [SerializeField] private float movementPerFrame;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        var deltaX = Input.GetAxis("Mouse X");
        if (deltaX != 0)
        {
            transform.Rotate(0, deltaX * movementPerFrame, 0);
        }
        var deltaY = Input.GetAxis("Mouse Y");
        if (deltaY != 0)
        {
            cameraParent.Rotate(-deltaY * movementPerFrame, 0, 0);
        }
    }
}
