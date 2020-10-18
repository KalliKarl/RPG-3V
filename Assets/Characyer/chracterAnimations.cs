using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class chracterAnimations : MonoBehaviour
{
    Animator animator;
    NavMeshAgent agent;
    public CharacterController controller;
    public float horizontalSpeed, verticalSpeed, overallSpeed,overallSpeedPercent,speedPercent,axis;
    // Start is called before the first frame update
    void Start()
    {
        agent  = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 horizontalVelocity = controller.velocity;

        horizontalVelocity = new Vector3(controller.velocity.x,0,controller.velocity.z);
        horizontalSpeed = horizontalVelocity.magnitude;
        verticalSpeed = controller.velocity.y;
        overallSpeed = controller.velocity.magnitude;
        overallSpeedPercent = overallSpeed / 6f;

        if (Input.GetAxis("Vertical")  != 0  )
        {
            axis = Input.GetAxis("Vertical") ;
        }
        else if (Input.GetAxis("Horizontal") != 0)
        {
            axis = Input.GetAxis("Horizontal") * 3;
        }
       
        
        speedPercent = agent.velocity.magnitude / agent.speed;

        if (speedPercent != 0)
        {
            Debug.Log(speedPercent + "speedpercent");
            animator.SetFloat("speedPercent",speedPercent,0.1f,Time.deltaTime);
        }
        else if (axis <= -0.25 || axis >= 0.25)
        {
            Debug.Log(axis + "axxxx");
            animator.SetFloat("speedPercent", overallSpeedPercent,0.1f,Time.deltaTime);
        }
       else if (axis >= -0.25 || axis <= 0.25)
        {
            Debug.Log(axis + "else");
            animator.SetFloat("speedPercent", 0, 0.1f, Time.deltaTime);
        }
    
    }
}
