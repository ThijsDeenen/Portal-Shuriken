using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolBehaviour : StateMachineBehaviour
{
    public float speed = 5f;

    private Enemy enemy;
    private List<Transform> patrolPositions;
    private NavMeshAgent navMeshAgent;

    private int currentPatrolIndex = 0;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemy = animator.GetComponent<Enemy>();
        patrolPositions = enemy.patrolPositions;
        navMeshAgent = enemy.navMeshAgent;

        navMeshAgent.enabled = true;
        navMeshAgent.isStopped = false;

        navMeshAgent.speed = speed;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        DoPatrol();
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

    private void DoPatrol()
    {
        if (navMeshAgent.enabled && IsEnemyAtDestination())
        {
            navMeshAgent.destination = patrolPositions[currentPatrolIndex].position;
            currentPatrolIndex++;
            if (currentPatrolIndex > (patrolPositions.Count - 1))
            {
                currentPatrolIndex = 0;
            }
        }
    }

    private bool IsEnemyAtDestination()
    {
        if (navMeshAgent.pathPending) return false;
        if (!(navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)) return false;
        return !navMeshAgent.hasPath || navMeshAgent.velocity.sqrMagnitude == 0f;
    }
}
