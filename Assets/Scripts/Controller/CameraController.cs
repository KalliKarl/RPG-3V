﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;

    public Vector3 offset;
    public float pitch = 2f;
    public float zoomSpeed = 4f;
    public float minZoom = 5f;
    public float maxZoom = 15f;

    private float currentZoom = 10f;
    public float yawSpeed = 100f, pitchSpeed = 1f;
    private float currentYaw = 0f, currentPitch=0f;

    private void Update()
    {
        currentZoom -= Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
        currentZoom = Mathf.Clamp(currentZoom, minZoom, maxZoom);
        if (Input.GetMouseButton(1)){
            currentYaw += Input.GetAxis("Horizontal") * yawSpeed * Time.deltaTime;
            currentPitch += Input.GetAxis("Vertical") * yawSpeed * Time.deltaTime;
        }
        
    }
    private void LateUpdate()
    {
        transform.position = target.position - offset * currentZoom;
        transform.LookAt(target.position + Vector3.up * pitch);
        
        transform.RotateAround(target.position, Vector3.up, currentYaw);
    }
  

}
