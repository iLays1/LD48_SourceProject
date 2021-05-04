using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchFeedback : MonoBehaviour
{
    public GameObject touchPrefab;
    Camera cam;

    private void Awake()
    {
        cam = Camera.main;
    }

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            for (int i = 0; i < Input.touchCount; i++)
            {
                Touch touch = Input.GetTouch(i);

                if (touch.phase == TouchPhase.Began)
                {
                    Vector3 touchPos = Camera.main.ScreenToWorldPoint(touch.position);
                    CreateTouch(touchPos);
                }
            }
        }

        //if (Input.GetMouseButtonDown(0))
        //{
        //    Vector3 mousePos = cam.ScreenToWorldPoint(Input.mousePosition); 
        //    CreateTouch(mousePos);
        //}
    }

    public void CreateTouch(Vector3 pos)
    {
        var go = Instantiate(touchPrefab);
        pos.z = 0;
        go.transform.position = pos;
    }
}
