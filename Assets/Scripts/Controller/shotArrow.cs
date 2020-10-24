using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shotArrow : MonoBehaviour
{
    // Start is called before the first frame update
    public Rigidbody rb;
    public float thrust;
    public Vector3 CmFwd;
    public GameObject Boom;
    bool explosion = false;
    void Start() {
        //GameObject.Find("Player").transform.forward
        CmFwd = Camera.main.transform.forward;
        CmFwd += new Vector3(0f,0.1f,0f);
        rb.AddForce(CmFwd * thrust);
        Destroy(gameObject, 30);
    }

    // Update is called once per frame
    void Update()
    {
        if(rb.velocity.magnitude >0 )
            transform.rotation = Quaternion.LookRotation(rb.velocity);
    }

    private void OnTriggerEnter(Collider other) {
        if(other.name != "Player") {
            rb.constraints = RigidbodyConstraints.FreezeAll;
            if (!explosion) {
                Instantiate(Boom, this.transform.position,this.transform.rotation);
                explosion = true;
            }
                
            rb.velocity = Vector3.zero;
            this.GetComponent<BoxCollider>().enabled = false;
            Debug.Log("Arrow Hit : " + other.name);
        }

        
        
    }
}
