using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public int health = 100;
    public float awareness = 0f;
    public float awarenessIncreaseSpeed = 30;
    public float awarenessDecreaseSpeed = 20;
    public Vector3 lastPlayerPos;

    public FieldOfView fieldOfView;
    public NavMeshAgent navMeshAgent;
    public Rigidbody rigidbody;
    public List<Transform> patrolPositions;

    private Animator enemyAI;

    public 

    // Start is called before the first frame update
    void Start()
    {
        enemyAI = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (fieldOfView.visibleTargets.Count > 0)
        {
            var distance = Vector3.Distance(fieldOfView.visibleTargets[0].position, transform.position);
            awareness += awarenessIncreaseSpeed * (fieldOfView.viewRadius / distance) * Time.deltaTime;
            lastPlayerPos = fieldOfView.visibleTargets[0].position;
        }
        else
        {
            awareness -= awarenessDecreaseSpeed * Time.deltaTime;
        }

        awareness = Mathf.Clamp(awareness, 0, 100);
        enemyAI.SetFloat("awareness", awareness);

        var color = Color.Lerp(Color.green, Color.red, awareness / 100);
        color.a = .5f;
        fieldOfView.viewMeshFilter.GetComponent<Renderer>().material.color = color;

        if (transform.position.y < -100)
        {
            Destroy(transform.gameObject);
        }
    }

    public void StunEnemy()
    {
        enemyAI.SetBool("stunned", true);
        enemyAI.SetTrigger("stun");
        if (navMeshAgent.enabled)
        {
            navMeshAgent.isStopped = true;
            navMeshAgent.enabled = false;
        }
    }
}
