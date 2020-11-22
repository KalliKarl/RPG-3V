using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class chracterAnimations : MonoBehaviour
{
    Joystick joystick;
    Animator animator;
    NavMeshAgent agent;
    public CharacterController controller;
    public float  speedPercent,axis,speed;
    Vector3 oldpos, newpos,velocity;
    // Start is called before the first frame update
    void Start()
    {
        agent  = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
        joystick = GetComponent<ThirdPersonMovement>().joystick;
        oldpos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {


        newpos = transform.position;
        var media = (newpos - oldpos);
        velocity = media / Time.deltaTime;
        oldpos = newpos;
        newpos = transform.position;
        speed = velocity.magnitude/6f;

        if (speed != 0)
            animator.SetFloat("speedPercent", speed, .1f, Time.deltaTime);
        else
            animator.SetFloat("speedPercent", 0, .1f, Time.deltaTime);

        // if (Input.GetAxis("Vertical")  != 0  )
        // {
        //     axis = Input.GetAxis("Vertical") ;
        // }
        // else if (Input.GetAxis("Horizontal") != 0)
        // {
        //     axis = Input.GetAxis("Horizontal") * 3;
        // }
        // else if (joystick.Vertical >= .2f || joystick.Vertical <= -.2f || joystick.Horizontal >= .2f || joystick.Horizontal <= -.2f) {
        //       if (joystick.Vertical > joystick.Horizontal)
        //         axis = joystick.Vertical;
        //     else
        //         axis = joystick.Horizontal;
        // }

        // speedPercent = agent.velocity.magnitude / agent.speed;

        // if (speedPercent != 0)
        // {
        //     //Debug.Log(speedPercent + "speedpercent");
        //     animator.SetFloat("speedPercent",speedPercent,0.1f,Time.deltaTime);
        // }
        // else if (axis <= -0.2 || axis >= 0.2)
        // {
        //     //Debug.Log(axis + "axxxx");
        //     animator.SetFloat("speedPercent", speed,0.1f,Time.deltaTime);
        // }
        //else if (axis >= -0.2 || axis <= 0.2)
        // {
        //     //Debug.Log(axis + "else");
        //     animator.SetFloat("speedPercent", 0, 0.1f, Time.deltaTime);
        // }

    }
}
