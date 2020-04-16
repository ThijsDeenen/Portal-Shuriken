using UnityEngine;
using UnityEngine.AI;

public class FleeBehaviour : StateMachineBehaviour
{
    public float speed = 10f;
    public float viewRadius = 80f;

    private Enemy enemy;
    private NavMeshAgent navMeshAgent;
    private FieldOfView fieldOfView;
    private Katana katana;
    private GameObject player;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemy = animator.GetComponent<Enemy>();
        navMeshAgent = enemy.navMeshAgent;
        fieldOfView = enemy.fieldOfView;
        katana = enemy.katana;
        player = enemy.player;

        navMeshAgent.enabled = true;
        navMeshAgent.isStopped = false;

        navMeshAgent.speed = speed;
        fieldOfView.viewRadius = viewRadius;

        katana.PutAway();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        var lookRotation = Quaternion.LookRotation(enemy.transform.position - player.transform.position);

        Vector3 runTo = enemy.transform.position + lookRotation * Vector3.forward;

        NavMeshHit hit;
        NavMesh.SamplePosition(runTo, out hit, 5, 1 << NavMesh.GetNavMeshLayerFromName("Default"));

        navMeshAgent.destination = hit.position;
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}
