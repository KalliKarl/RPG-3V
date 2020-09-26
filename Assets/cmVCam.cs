using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cmVCam : MonoBehaviour {
    public float yawSpeed = 100f, pitchSpeed = 1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(1)) {

            Camera.main.transform.rotation *= Quaternion.AngleAxis(Input.GetAxis("Horizontal")* yawSpeed,Vector3.up);

        }
    }
}
