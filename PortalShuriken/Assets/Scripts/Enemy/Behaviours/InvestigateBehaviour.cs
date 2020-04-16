using UnityEngine;
using UnityEngine.AI;

public class InvestigateBehaviour : StateMachineBehaviour
{
    public float speed = 6f;
    public float viewRadius = 60f;

    private Enemy enemy;
    private NavMeshAgent navMeshAgent;
    private FieldOfView fieldOfView;
    private Katana katana;

    private Vector3 lastPlayerPos;
    private float rotationAmount = 40f;

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
        if (navMeshAgent.enabled)
        {
            if (lastPlayerPos != enemy.lastPlayerPos)
            {
                Investigate();
            }
            if (IsEnemyAtDestination())
            {
                LookAround();
            }
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

    private void Investigate()
    {
        lastPlayerPos = enemy.lastPlayerPos;
        navMeshAgent.destination = lastPlayerPos;
    }

    private void LookAround()
    {
        var enemyRotation = enemy.transform.rotation.eulerAngles.y;
        var newRotation = enemyRotation + rotationAmount;
        if (newRotation > 360) { newRotation -= 360; }
        if (newRotation < 0) { newRotation += 360; }
        if ((rotationAmount > 0 && enemyRotation > newRotation) || (rotationAmount < 0 && enemyRotation < newRotation))
        {
            rotationAmount *= -1;
        }
        enemy.transform.Rotate(new Vector3(0, rotationAmount, 0) * Time.deltaTime);
    }

    private bool IsEnemyAtDestination()
    {
        if (navMeshAgent.pathPending) return false;
        if (!(navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)) return false;
        return !navMeshAgent.hasPath || navMeshAgent.velocity.sqrMagnitude == 0f;
    }
}
