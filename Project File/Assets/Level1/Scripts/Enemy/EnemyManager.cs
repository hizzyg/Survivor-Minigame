using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public Player playerHealth;
    public GameObject enemy;
    public int spawnCount;
    public int spawnLimit;
    public float spawnTime = 3f;
    public float timer;
    public Transform[] spawnPoints;
    public bool spawn;

    private void Start()
    {
        //InvokeRepeating("Spawn", spawnTime, spawnTime);
    }

    void Update()
    {
        SpawnCheck();
    }

    void SpawnCheck()
    {
        if (spawnCount < spawnLimit)
        {
            timer += Time.deltaTime;
        }
        // Cooldown
        if (timer > spawnTime)
        {
            if (spawnCount < spawnLimit)
            {
                Spawn();
                timer = 0.0f;
            }
        }
    }

    void Spawn()
    {
        if (playerHealth.currentHealth <= 0f)
        {
            return;
        }
        int spawnPointIndex = Random.Range(0, spawnPoints.Length);
        GameObject go = Instantiate(enemy, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
        go.GetComponent<EnemyHealth>().spawnCounter = this;
        spawnCount++;
    }
}
