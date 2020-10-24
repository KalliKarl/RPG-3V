using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;
using System.Net.Cache;
using UnityEngine.Diagnostics;
using System.Collections.Generic;
using Cinemachine;

public class PlayerControl : MonoBehaviour {
    public Interactable focus;
    public LayerMask movementMask,itemMask;
    public GameObject hitPrefab, arrow,posBow,CM_Cam;
    public float fov,minT,maxT,tMultp;
    Camera cam;
    PlayerMotor motor;
    GameObject mobUI;
    Image healthSlider;
    int indx;
    public float thrust;
    public List<Transform> transforms = new List<Transform>();
    void Start() {
        cam = Camera.main;
        motor = GetComponent<PlayerMotor>();
        mobUI = StaticMethods.FindInActiveObjectByName("MobUI");
        healthSlider = StaticMethods.FindInActiveObjectByName("MobHpTop").GetComponent<Image>();
    }

    void Update() {
        if (Input.GetKey(KeyCode.E)) { // Hold E BOW
            maxT = 5000;
            minT = 500;
            if (thrust < maxT)
                thrust += tMultp;
            fov = 45 - ((thrust - minT) / (maxT - minT)) * 10;
            CM_Cam.GetComponent<CinemachineFreeLook>().m_Lens.FieldOfView = Mathf.Lerp(CM_Cam.GetComponent<CinemachineFreeLook>().m_Lens.FieldOfView, fov,Time.deltaTime *10);
            this.transform.rotation = Quaternion.Lerp(this.transform.rotation, cam.transform.rotation, Time.deltaTime * 5);
        }
        if (Input.GetKeyUp(KeyCode.E)) { // When Relase BOW Button

            CM_Cam.GetComponent<CinemachineFreeLook>().m_Lens.FieldOfView = 45;
            fov = (int)CM_Cam.GetComponent<CinemachineFreeLook>().m_Lens.FieldOfView;
            Vector3 playerPos = this.transform.position;
            Quaternion playerRot = this.transform.rotation;
            Vector3 shootingPos = posBow.transform.position;
            Quaternion cmRot = cam.transform.rotation;



            if (thrust > 5000)
                thrust = 5000;
            arrow.GetComponent<shotArrow>().thrust = thrust;
            Debug.Log(thrust);
            Instantiate(arrow, shootingPos, cmRot);
            //Instantiate(arrow, shootingPos + new Vector3(0, .5f, 0),  cmRot);
            //Instantiate(arrow, shootingPos + new Vector3(0, -.5f, 0), cmRot);
            //Instantiate(arrow, shootingPos + new Vector3(.5f, 0, 0),  cmRot);
            //Instantiate(arrow, shootingPos + new Vector3(-.5f, 0, 0), cmRot);

            thrust = 500;
        }


        if (Input.GetKeyDown(KeyCode.G) && transforms.Count >0) { // Grab item nearest item to player from list
            
            transforms.RemoveAll(Transform => Transform == null);
            float distanceF =0f ,distanceL = 0f;
            for(int i = 0; i < transforms.Count;i ++) {
                distanceF = Vector3.Distance(this.transform.position,transforms[i].position);
                if (distanceL == 0)
                    distanceL = distanceF ;
                if (distanceF < distanceL) {
                    distanceL = distanceF;
                    indx = i;
                }
            }
            if(transforms.Count > 0 && transforms[indx] != null) {
                motor.MoveToPoint(transforms[indx].transform.position);
                hitPrefab.transform.position = transforms[indx].transform.position;
                hitPrefab.transform.position += new Vector3(0f, 0.05f, 0f);
                Interactable interactable = transforms[indx].gameObject.GetComponentInParent<Interactable>();
                if (interactable != null) {
                    SetFocus(interactable);
                }
            }

            
            transforms.RemoveAll(Transform => Transform == null);
            indx = 0;
        }

        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (Input.GetMouseButtonDown(0)) {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;


            if (Physics.Raycast(ray, out hit, 100)) {
                if (hit.transform.gameObject.layer == LayerMask.NameToLayer("walkable")) {
                    motor.MoveToPoint(hit.point);
                    hitPrefab.transform.position = hit.point;
                    hitPrefab.transform.position += new Vector3(0f, 0.1f, 0f);
                    Debug.DrawLine(this.transform.position, hit.point, Color.red,1f);
                    Debug.Log("we hit movementmask" + hit.collider.name + hit.point);
                    // move our player to what we hit

                    // stop focusing any objects
                    RemoveFocus();
                }
                else if(hit.transform.gameObject.layer == LayerMask.NameToLayer("itemBox")) {
                    motor.MoveToPoint(hit.point);
                    Debug.DrawLine(this.transform.position, hit.point, Color.red, 1f);

                    Interactable interactable = hit.collider.GetComponentInParent<Interactable>();
                    if (interactable != null) {
                        SetFocus(interactable);
                    }
                    Debug.Log("we hit item mask" + hit.collider.name + hit.point);
                }
                else {
                    if (hit.transform.name != "Player")
                       motor.MoveToPoint(hit.point);  
                   
                    Debug.DrawLine(this.transform.position, hit.point, Color.red, 1f);

                    Interactable interactable = hit.collider.GetComponentInParent<Interactable>();
                    if (interactable != null) {
                        SetFocus(interactable);
                    }
                    Debug.Log("we hit others " + hit.collider.name + hit.point);
                }
                transforms.RemoveAll(Transform => Transform == null);

            }
        }
        
        
    }
    void SetFocus(Interactable newFocus) {
        if (newFocus != focus) {
            if (focus != null)
                focus.OnDefocused();
            focus = newFocus;
            motor.FollowTarget(newFocus);
            string foc = newFocus.name.ToString();
            foc = foc.Substring(0, 7);
            if(foc != "itemBox" && foc !="NPCNPCN") {
                StaticMethods.FindInActiveObjectByName("MobUI").SetActive(true);
                StaticMethods.FindInActiveObjectByName("MobName").SetActive(true);
                StaticMethods.FindInActiveObjectByName("MobLvl").SetActive(true);
                StaticMethods.FindInActiveObjectByName("MobHealthUi").SetActive(true);
                StaticMethods.FindInActiveObjectByName("MobMHp").SetActive(true);
                StaticMethods.FindInActiveObjectByName("MobCHp").SetActive(true);
                GameObject.Find("MobName").GetComponent<Text>().text = newFocus.GetComponent<EnemyStats>().mobName;
                GameObject.Find("MobLvl").GetComponent<Text>().text = newFocus.GetComponent<EnemyStats>().Level.ToString();
                GameObject.Find("MobMHp").GetComponent<Text>().text = " /" + newFocus.GetComponent<EnemyStats>().maxHealth.ToString();
                GameObject.Find("MobCHp").GetComponent<Text>().text = newFocus.GetComponent<EnemyStats>().currentHealth.ToString();
            }
            if(foc == "NPCNPCN") {
                StaticMethods.FindInActiveObjectByName("MobUI").SetActive(true); ;
                GameObject.Find("MobName").GetComponent<Text>().text = newFocus.GetComponent<NPC>().npcName;
                StaticMethods.FindInActiveObjectByName("MobLvl").SetActive(false);
                StaticMethods.FindInActiveObjectByName("MobHealthUi").SetActive(false);
                StaticMethods.FindInActiveObjectByName("MobMHp").SetActive(false);
                StaticMethods.FindInActiveObjectByName("MobCHp").SetActive(false);

            }
            if (healthSlider != null && gameObject.name != "Player") {
                float healthPercent = transform.GetComponent<EnemyStats>().currentHealth / (float)transform.GetComponent<EnemyStats>().maxHealth;
                healthSlider.fillAmount = healthPercent;
            }
        }
    
        newFocus.OnFocused(transform);

    }
    void RemoveFocus() {
        if (focus != null)
            focus.OnDefocused();
        mobUI.SetActive(false);
        focus = null;
        motor.StopFollowingTarget();
    }
    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.layer == 12)
            transforms.Add(other.transform);

    }
    private void OnTriggerExit(Collider other) {
        if (other.gameObject.layer == LayerMask.NameToLayer("itemBox"))
            transforms.Remove(other.transform);
    }

}
