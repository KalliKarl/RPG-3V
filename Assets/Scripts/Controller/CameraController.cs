using UnityEngine;
using Cinemachine;

[RequireComponent(typeof(CinemachineFreeLook))]

public class CameraController : MonoBehaviour
{
    //public Transform target;

    //public Vector3 offset;
    //public float pitch = 2f;
    //public float zoomSpeed = 4f;
    //public float minZoom = 5f;
    //public float maxZoom = 15f;

    //private float currentZoom = 10f;
    //public float yawSpeed = 10f, pitchSpeed = 10f;
    //private float currentYaw = 0f, currentPitch=0f;
    //float y, x;

    private bool _freeLookActive;
    private void Start() {
        CinemachineCore.GetInputAxis = GetInputAxis;
    }
    private void Update()
    {
        _freeLookActive = Input.GetMouseButton(1); // 0 = left mouse btn or 1 = right

        //currentZoom -= Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
        //currentZoom = Mathf.Clamp(currentZoom, minZoom, maxZoom);
        //if (Input.GetMouseButton(1)){
        //    y = Input.GetAxis("Horizontal");
        //    x = Input.GetAxis("Vertical");
        //    Debug.Log(x+" -  -  - "+y);
        //    currentYaw += Input.GetAxis("Horizontal") * yawSpeed * Time.deltaTime;
        //    currentPitch += Input.GetAxis("Vertical") * yawSpeed * Time.deltaTime;
        //}
        
    }
    private void LateUpdate()
    {
        //transform.position = target.position - offset * currentZoom;
        //transform.LookAt(target.position + Vector3.up * pitch);
        //transform.RotateAround(target.position, Vector3.up, currentYaw);
    }
    private float GetInputAxis(string axisName) {
        return !_freeLookActive ? 0 : Input.GetAxis(axisName == "Mouse Y" ? "Mouse Y" : "Mouse X");
    }


}
