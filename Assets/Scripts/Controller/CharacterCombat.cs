using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CharacterStats))]
public class CharacterCombat : MonoBehaviour{

    public float attackSpeed = 1f;
    private float attackCooldown = 0f,healingCooldown = 0f;
    public float attackDelay = .6f;
    public event System.Action OnAttack;
    CharacterStats myStats;
    Image healthSlider;
    public GameObject player;
    //private Animator anim;
    private void Start() {
        myStats = GetComponent<CharacterStats>();
        //anim = GameObject.Find("AxeAnimator").GetComponent<Animator>();
        healthSlider = StaticMethods.FindInActiveObjectByName("MobHpTop").GetComponent<Image>();
        player = GameObject.Find("Player");
    }

    private void Update() {
        attackCooldown -= Time.deltaTime;
        healingCooldown -= Time.deltaTime;
        if(healingCooldown <= 0) {

           int _cCurhp = this.GetComponent<CharacterStats>().currentHealth;

           int _cMaxhp= this.GetComponent<CharacterStats>().maxHealth;
            if (_cCurhp != _cMaxhp) {
                _cCurhp +=(int) ((_cMaxhp / 100f) * 5);
                if (_cCurhp >= _cMaxhp) {
                    _cCurhp = _cMaxhp;
                    this.GetComponent<CharacterStats>().Healthmodifer(_cCurhp);

                }
                    
            }

        }
    }
    public void Attack(CharacterStats targetStats,int a) {
        //Debug.Log(attackCooldown);
        if(attackCooldown <= 0) {
            StartCoroutine(DoDamage(targetStats,attackDelay,a));
           // targetStats.TakeDamage(myStats.damage.GetValue());
            attackCooldown = .6f / attackSpeed;
            if (OnAttack != null)
                OnAttack();
            //if (anim != null && gameObject.name == "Player") {
            //    anim.Play("Base Layer.aXe",0);
            //}            
           
        }
    }

    IEnumerator DoDamage (CharacterStats stats , float delay,int a) {
        yield return new WaitForSeconds(delay);
        int _cDamage = myStats.damage.GetValue();
        stats.TakeDamage(_cDamage,a);
        int rand = (int)Random.Range(0f, 5f);
        //Debug.Log(rand +"DoDamage" + myStats.damage.GetValue());
        
        if (healthSlider != null && player.GetComponent<PlayerControl>().focus != null) {
                    Interactable enemy =    player.GetComponent<PlayerControl>().focus;
                    float healthPercent =(float) enemy.GetComponent<EnemyStats>().currentHealth / (float)enemy.GetComponent<EnemyStats>().maxHealth;
                    GameObject.Find("MobCHp").GetComponent<Text>().text = enemy.GetComponent<EnemyStats>().currentHealth.ToString();
                    GameObject.Find("MobMHp").GetComponent<Text>().text = " /" + enemy.GetComponent<EnemyStats>().maxHealth.ToString();
            healthSlider.fillAmount = healthPercent;
            }
        FindObjectOfType<AudioManager>().Play("hit"+rand);
    }
}
