using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ThirdPersonMovement : MonoBehaviour{
    public CharacterController controller;
    public Joystick joystick;
    public float speed = 3f, turnSmoothTime = 0.1f, turnSmoothVelocity,axis;
    public GameObject CM_Cam;
    Vector3 gravity = new Vector3(0f,-9.81f,0f);
    Transform cam;

    private void Start() {
        cam = Camera.main.transform;
    }
    void Update(){
        

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        
        if (joystick.Horizontal >= .2f)
            horizontal = joystick.Horizontal;
        else if (joystick.Horizontal <= -.2f)
            horizontal = joystick.Horizontal;
        if (joystick.Vertical >= .2f)
            vertical = joystick.Vertical;
        else if (joystick.Vertical <= -.2f)
            vertical = joystick.Vertical;

        axis = Mathf.Clamp01(new Vector2(horizontal,vertical).magnitude);



        Vector3 direction = new Vector3(horizontal,0f,vertical).normalized;
        if (direction.magnitude >= 0.1f) {
            CM_Cam.GetComponent<CinemachineFreeLook>().m_XAxis.Value = 0;
            float targetAngle = Mathf.Atan2(direction.x,direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y,targetAngle,ref turnSmoothVelocity,turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f,angle,0f);
            Vector3 moveDir = Quaternion.Euler(0f,targetAngle,0f) * Vector3.forward;
            
            controller.Move(moveDir.normalized * (speed*axis)  * Time.deltaTime + gravity);
        }
    }
    



}
