using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] int speed;
    [SerializeField] int midMouseSlowFactor;
    Camera cam;
    float startDragPos;

    private void Awake()
    {
        cam = GetComponent<Camera>();
    }

    void Update()
    {
        if (Input.mouseScrollDelta.y > 0)
            Move(Vector3.up);
        if (Input.mouseScrollDelta.y < 0)
            Move(Vector3.down);

        if (Input.GetMouseButtonDown(2))
            startDragPos = Input.mousePosition.y;
        if (Input.GetMouseButton(2))
        {
            var mousePos = Input.mousePosition;
            if(mousePos.y > startDragPos + 1f)
                Move(Vector3.up / midMouseSlowFactor);
            if (mousePos.y < startDragPos - 1f)
                Move(Vector3.down / midMouseSlowFactor);
        }
    }

    void Move(Vector3 dir)
    {
        transform.position += dir * speed * Time.deltaTime;
    }
}
