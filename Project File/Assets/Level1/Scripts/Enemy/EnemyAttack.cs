using UnityEngine;

public class EnemyAttack : MonoBehaviour
{

    public float timeBetweenAttacks = 5f;
    public int attackDamage = 10;

    Animator anim;
    GameObject player;
    Player playerHealth;
    bool playerInRange;
    float timer;
    EnemyHealth enemyHealth;
    

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<Player>();
        anim = GetComponent<Animator>();
        enemyHealth = GetComponent<EnemyHealth>();
    }

    void OnTriggerEnter(Collider _col)
    {
        if (_col.gameObject == player)
        {
            playerInRange = true;
        }
    }

    void OnTriggerExit(Collider _col)
    {
        if (_col.gameObject == player)
        {
            playerInRange = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Timer();
    }

    void Timer()
    {
        timer += Time.deltaTime;

        if (timer >= timeBetweenAttacks && playerInRange && enemyHealth.currentHealth > 0)
        {
            //Debug.Log("Playerisinrange");
            Attack();
        }

        //if (true)
        //{
        //    anim.SetTrigger("PlayerDead");
        //}
    }

    void Attack()
    {
        timer = 0f;
        if (playerHealth.currentHealth > 0)
        {
            playerHealth.TakeDamage(attackDamage);
        }
    }
}
