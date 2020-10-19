using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;
using System.Net.Cache;

public class PlayerControl : MonoBehaviour {
    public Interactable focus;
    public LayerMask movementMask;
    public GameObject hitPrefab;
    Camera cam;
    PlayerMotor motor;
    GameObject mobUI;
    Image healthSlider;

    void Start() {
        cam = Camera.main;
        motor = GetComponent<PlayerMotor>();
        mobUI = StaticMethods.FindInActiveObjectByName("MobUI");
        healthSlider = StaticMethods.FindInActiveObjectByName("MobHpTop").GetComponent<Image>();
    }

    void Update() {
        

        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (Input.GetMouseButtonDown(0)) {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100, movementMask)) {
                motor.MoveToPoint(hit.point);
                hitPrefab.transform.position = hit.point;
                hitPrefab.transform.position += new Vector3(0f,0.1f,0f);
                // Debug.Log("we hit" + hit.collider.name + hit.point);
                // move our player to what we hit

                // stop focusing any objects
                RemoveFocus();
                
            }
        }
        if (Input.GetMouseButtonDown(1)) {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100)) {
                
                Interactable interactable = hit.collider.GetComponentInParent<Interactable>();
                if (interactable != null) {
                    SetFocus(interactable);
                }
                //Check if we hit an itreccatable

                // stop focusing any objects
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
}
