using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossHairTarget : MonoBehaviour
{
    Camera MainCamera;
    Ray Ray;
    RaycastHit hitInfo;

    void Start()
    {
        MainCamera = Camera.main;
    }

    void Update()
    {
        Ray.origin=MainCamera.transform.position;
        Ray.direction=MainCamera.transform.forward;
        Physics.Raycast(Ray, out hitInfo);
        transform.position = hitInfo.point;
        
    }
}
