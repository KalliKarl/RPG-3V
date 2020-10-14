using UnityEngine;
using Cinemachine;
[RequireComponent(typeof(CinemachineFreeLook))]

public class CameraController : MonoBehaviour
{
    Vector2 move;
    float rsh, rsv;
    bool inpt;
    private bool _freeLookActive;
    private void Start() {
        CinemachineCore.GetInputAxis = GetInputAxis;
    }
    private void Update()
    {
        
        _freeLookActive = Input.GetMouseButton(1); // 0 = left mouse btn or 1 = right

        if (Input.GetAxis("RSH") != 0)
        {
            if (Input.GetAxis("RSH") >= 0.2 || Input.GetAxis("RSH") <= -0.2)
                
                rsh = Input.GetAxis("RSH");
            
            _freeLookActive = true;
            inpt = true;
        }
        if (Input.GetAxis("RSV") != 0)
        {
            if (Input.GetAxis("RSV") >= 0.2 || Input.GetAxis("RSV") <= -0.2)
                rsv = Input.GetAxis("RSV");

            _freeLookActive = true;
            inpt = true;
        }
        if (inpt)
        {
            Debug.Log("X : " + rsh + "\t Y:" + rsv);
            inpt = false;
        }

    }
    private float GetInputAxis(string axisName) {
        return !_freeLookActive ? 0 : Input.GetAxis(axisName == "Mouse Y" ? "Mouse Y" : "Mouse X");
        
    }
    private void Awake() {
    }



}
