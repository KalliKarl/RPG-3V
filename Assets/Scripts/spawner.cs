using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class spawner : MonoBehaviour
{
    public GameObject[] enemies,spots;  
    private Vector3 spawnValues;
    public float spawnWait,range;
    public int startWait, mobIndex,limit,amount; 
    public bool stop,spot;
    void Start(){
        StartCoroutine(waitSpawner());
        spawnValues = this.transform.position;
     
    }
    public void startCorotine() {
        StartCoroutine(waitSpawner());
    }
    IEnumerator waitSpawner() {
        yield return new WaitForSeconds(startWait);

        while (!stop) {

            int randSpot = Random.Range(0,8);

            if (spot) {
                spawnValues = spots[randSpot].transform.position;
            }
            else {

                spawnValues = new Vector3(Random.Range(transform.position.x + range, transform.position.x - range), transform.position.y, Random.Range(transform.position.z + range, transform.position.z - range));

            }


            GameObject enemyAI = Instantiate(enemies[mobIndex], spawnValues, gameObject.transform.rotation) as GameObject;
            if (enemyAI != null)
                amount++;
            if (amount >= limit)
                stop = true;
            yield return new WaitForSeconds(spawnWait);
        } 
        
    }
    void OnDrawGizmosSelected() {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, range);
    }
    private void OnTriggerEnter(Collider other) {
        Debug.Log(other.name);
        Destroy(this.gameObject.GetComponent<Rigidbody>()); 


    }
}
