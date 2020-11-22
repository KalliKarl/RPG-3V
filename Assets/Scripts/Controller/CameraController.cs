using UnityEngine;
using Cinemachine;
[RequireComponent(typeof(CinemachineFreeLook))]

public class CameraController : MonoBehaviour{

    private bool _freeLookActive;
    private void Start() {
        CinemachineCore.GetInputAxis = GetInputAxis;
    }
    private void Update()
    {
        
        _freeLookActive = Input.GetMouseButton(1); // 0 = left mouse btn or 1 = right

    }
    private float GetInputAxis(string axisName) {
        //return !_freeLookActive ? 0 : Input.GetAxis(axisName == "Mouse Y" ? "Mouse Y" : "Mouse X");
        if(!_freeLookActive)
        {
            return 0;
        }
        else
        {
            if(axisName == "Mouse Y")
            {
               return Input.GetAxis("Mouse Y");
            }
            else
            {
                return Input.GetAxis("Mouse X");
            }
        }
        
    }



}
