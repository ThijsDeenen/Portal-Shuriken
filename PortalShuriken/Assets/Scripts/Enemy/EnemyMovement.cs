using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float margin = 0.1f;
    [SerializeField] private float chaseDelay = 0.1f;

    public FieldOfView vieldOfView;
    public List<Transform> patrolPositions;

    private NavMeshAgent navMeshAgent;
    private Rigidbody rigidbody;

    private int currentPatrolIndex = 0;
    private bool isOnPatrol = false;
    private bool isChasingPlayer = false;

    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        rigidbody = GetComponent<Rigidbody>();
        StartPatrol();
    }

    private void Update()
    {
        if (!isChasingPlayer && vieldOfView.visibleTargets.Count > 0)
        {
            StartPlayerChase();
        }

        if (!isChasingPlayer && !isOnPatrol)
        {
            StartPatrol();
        }
    }

    public void StartPatrol()
    {
        if (isOnPatrol) return;
        navMeshAgent.enabled = true;
        navMeshAgent.isStopped = false;
        isOnPatrol = true;
        StartCoroutine(DoPatrol());
    }

    public void StopPatrol()
    {
        if (!isOnPatrol) return;
        isOnPatrol = false;
        StopCoroutine(DoPatrol());
        navMeshAgent.isStopped = true;
        navMeshAgent.enabled = false;
    }

    public void StartPlayerChase()
    {
        if (isChasingPlayer) return;
        if (isOnPatrol)
        {
            StopPatrol();
        }
        navMeshAgent.enabled = true;
        navMeshAgent.isStopped = false;
        isChasingPlayer = true;
        StartCoroutine(ChasePlayer());
    }

    public void StopPlayerChase()
    {
        if (!isChasingPlayer) return;
        isChasingPlayer = false;
        StopCoroutine(ChasePlayer());
        navMeshAgent.isStopped = true;
        navMeshAgent.enabled = false;
    }

    private IEnumerator DoPatrol()
    {
        while (isOnPatrol)
        {
            navMeshAgent.destination = patrolPositions[currentPatrolIndex].position;
            currentPatrolIndex++;
            yield return new WaitUntil(IsEnemyNearDestination);
            if (currentPatrolIndex > (patrolPositions.Count - 1))
            {
                currentPatrolIndex = 0;
            }
        }
    }

    private IEnumerator ChasePlayer()
    {
        while (vieldOfView.visibleTargets.Count > 0)
        {
            navMeshAgent.destination = vieldOfView.visibleTargets[0].position;
            yield return new WaitForSeconds(chaseDelay);
        }

        StopPlayerChase();
    }

    private bool IsEnemyNearDestination()
    {
        if (!isOnPatrol) return false;
        if (navMeshAgent.pathPending) return false;
        if (!(navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)) return false;
        return !navMeshAgent.hasPath || navMeshAgent.velocity.sqrMagnitude == 0f;
    }
}
