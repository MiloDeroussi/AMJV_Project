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
        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.position += (Vector3.forward + Vector3.right) * 1/10;
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.position += (Vector3.right + Vector3.back) * 1/10;
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.position += (Vector3.back + Vector3.left) * 1 / 10;
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position += (Vector3.left + Vector3.forward) * 1 / 10;
        }

        if (Input.mouseScrollDelta.y != 0)
        {
            cam.fieldOfView -= Input.mouseScrollDelta.y;
        }
    }
}
