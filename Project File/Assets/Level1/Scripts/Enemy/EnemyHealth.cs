using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHealth : MonoBehaviour
{
    public int startingHealth = 100;
    public int currentHealth;
    public float sinkSpeed = 2.5f;
    public int scoreValue = 10;
    public AudioClip deathClip;

    Animator anim;
    AudioSource enemyAudio;
    CapsuleCollider capsuleCollider;
    bool isDead;
    bool isSinking;

    public EnemyManager spawnCounter;

    private void Awake()
    {
        spawnCounter = GetComponent<EnemyManager>();

        anim = GetComponent<Animator>();
        enemyAudio = GetComponent<AudioSource>();
        capsuleCollider = GetComponent<CapsuleCollider>();

        currentHealth = startingHealth;
    }

    private void Update()
    {
        isItSinking();
    }

    public void TakeDamage(int _amount, Vector3 _hitPoint)
    {
        if (isDead)
        {
            return;
        }

        enemyAudio.Play();
        currentHealth -= _amount;

        if (currentHealth <= 0)
        {
            Death();
        }
    }

    public void Death()
    {
        isDead = true;

        capsuleCollider.isTrigger = true;
        anim.SetTrigger("Dead");

        enemyAudio.clip = deathClip;
        enemyAudio.Play();

        spawnCounter.spawnCount--;
    }

    void isItSinking()
    {
        if (isSinking)
        {
            transform.Translate(-Vector3.up * sinkSpeed * Time.deltaTime);
        }
    }

    public void StartSinkig()
    {
        GetComponent<NavMeshAgent>();
        GetComponent<Rigidbody>().isKinematic = true;
        isSinking = true;
        ScoreManager.score += scoreValue;
        Destroy(gameObject, 2f);
    }
}
