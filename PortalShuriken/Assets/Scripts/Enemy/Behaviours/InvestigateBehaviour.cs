using UnityEngine;
using UnityEngine.AI;

public class InvestigateBehaviour : StateMachineBehaviour
{
    public float speed = 5f;
    public float viewRadius = 60f;

    private Enemy enemy;
    private NavMeshAgent navMeshAgent;
    private FieldOfView fieldOfView;
    private Katana katana;

    private int currentPatrolIndex = 0;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemy = animator.GetComponent<Enemy>();
        navMeshAgent = enemy.navMeshAgent;
        fieldOfView = enemy.fieldOfView;
        katana = enemy.katana;

        navMeshAgent.enabled = true;
        navMeshAgent.isStopped = false;

        navMeshAgent.speed = speed;
        fieldOfView.viewRadius = viewRadius;

        katana.PutAway();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        GoInvestigate();
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

    private void GoInvestigate()
    {
        if (navMeshAgent.enabled)
        {
            navMeshAgent.destination = enemy.lastPlayerPos;
        }

        if(navMeshAgent.enabled && IsEnemyAtDestination())
        {
            //check around the area for the player
        }
    }

    private bool IsEnemyAtDestination()
    {
        if (navMeshAgent.pathPending) return false;
        if (!(navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)) return false;
        return !navMeshAgent.hasPath || navMeshAgent.velocity.sqrMagnitude == 0f;
    }
}
