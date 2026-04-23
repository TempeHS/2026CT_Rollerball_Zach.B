using UnityEngine;
using UnityEngine.AI;


public class EnemyMovement2 : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    public Transform player;
    private PlayerController playerController;

    void Start () {
        navMeshAgent = GetComponent<NavMeshAgent>();
        playerController = player.GetComponent<PlayerController>();
        navMeshAgent.isStopped = false;
    }

    void Update() {
        if (player == null) return;

        if(playerController.islevel1destroyed) {
            navMeshAgent.isStopped = false;
            navMeshAgent.SetDestination(player.position);
        }
    }



}