using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{

    public Camera mainCamera;

    public Vector2 capTopLeft;
    public Vector2 capBottomRight;

    // Update is called once per frame
    void Update()
    {
        mainCamera.transform.position = new Vector3(transform.position.x, transform.position.y, mainCamera.transform.position.z);
    }
}
