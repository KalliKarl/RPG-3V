using UnityEngine;
using Cinemachine;
using UnityEngine.InputSystem;
[RequireComponent(typeof(CinemachineFreeLook))]

public class CameraController : MonoBehaviour
{
    PlayerControls controls;
    Vector2 move;
  
    private bool _freeLookActive;
    private void Start() {
        CinemachineCore.GetInputAxis = GetInputAxis;
    }
    private void Update()
    {if(Input.GetAxis(""))
        _freeLookActive = Input.GetMouseButton(1); // 0 = left mouse btn or 1 = right
        Input.GetAxis(Jo);
    }
    private void LateUpdate()
    {

    }
    private float GetInputAxis(string axisName) {
        return !_freeLookActive ? 0 : Input.GetAxis(axisName == "Mouse Y" ? "Mouse Y" : "Mouse X");
        
    }
    private void Awake() {
        //controls = new PlayerControls();
        //controls.Gameplay.RStick.performed += ctx => move =ctx.ReadValue<Vector2>();
       // controls.Gameplay.RStick.canceled += ctx => move = Vector2.zero;
    }

    void RStick() {
        Debug.Log("Rstick");
    }
    private void OnEnable() {
        controls.Gameplay.Enable();
        Debug.Log("Enable");
    }
    private void OnDisable() {
        controls.Gameplay.Disable();
    }


}
