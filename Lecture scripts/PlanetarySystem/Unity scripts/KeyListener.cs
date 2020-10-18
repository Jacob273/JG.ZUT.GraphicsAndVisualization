using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyListener : MonoBehaviour
{
    public Camera camera1;
    public Camera camera2;
    public Camera camera3;
    public Camera camera4;
    List<Camera> cameras = new List<Camera>();
    void Start()
    {
        cameras = new List<Camera>();

        if(camera1 != null)
        {
            cameras.Add(camera1);
        }

        if (camera2 != null)
        {
            cameras.Add(camera2);
        }

        if (camera3 != null)
        {
            cameras.Add(camera3);
        }


        if (camera4 != null)
        {
            cameras.Add(camera4);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            DisableAllCamerasExcept(camera1);
        }

        if (Input.GetKeyUp(KeyCode.Alpha2))
        {
            DisableAllCamerasExcept(camera2);
        }

        if (Input.GetKeyUp(KeyCode.Alpha3))
        {
            DisableAllCamerasExcept(camera3);
        }

        if (Input.GetKeyUp(KeyCode.Alpha4))
        {
            DisableAllCamerasExcept(camera4);
        }
    }

    private void DisableAllCamerasExcept(Camera exceptionCamera)
    {
        foreach (var camera in cameras)
        {
            if (camera != exceptionCamera)
            {
                camera.enabled = false;
            }
            exceptionCamera.enabled = true;
        }
    }
}
