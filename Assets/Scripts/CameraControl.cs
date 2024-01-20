using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Z))
        {
            transform.position += (Vector3.forward + Vector3.right) * 1/10;
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.position += (Vector3.right + Vector3.back) * 1/10;
        }

        if (Input.GetKey(KeyCode.S))
        {
            transform.position += (Vector3.back + Vector3.left) * 1 / 10;
        }

        if (Input.GetKey(KeyCode.Q))
        {
            transform.position += (Vector3.left + Vector3.forward) * 1 / 10;
        }

        if (Input.mouseScrollDelta.y != 0)
        {
            cam.fieldOfView -= Input.mouseScrollDelta.y;
        }
    }
}
