using UnityEngine;
using UnityEngine.AI;

public class FleeBehaviour : StateMachineBehaviour
{
    public float speed = 8f;
    public float viewRadius = 80f;

    private Enemy enemy;
    private NavMeshAgent navMeshAgent;
    private FieldOfView fieldOfView;
    private Katana katana;

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

    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}
