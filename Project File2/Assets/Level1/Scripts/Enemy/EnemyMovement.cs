using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{

    private NavMeshAgent nma;
    private Player[] player;

    void Start()
    {
        player = FindObjectsOfType<Player>();
        nma = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        nma.SetDestination(player[0].transform.position);
        //Debug.Log(player[0].transform.position,player[0].gameObject);
    }

   //Transform player;
   //NavMeshAgent nav;
   //
   //
   //void Awake()
   //{
   //    player = GameObject.FindGameObjectWithTag("Player").transform;
   //    nav = GetComponent<NavMeshAgent>();
   //}
   //
   //void Start()
   //{
   //    if (!player)
   //    {
   //        Debug.Log("Make sure the player is tagged!");
   //    }
   //}
   //
   //// Update is called once per frame
   //void Update()
   //{
   //    nav.destination = player.transform.position;
   //}
}
