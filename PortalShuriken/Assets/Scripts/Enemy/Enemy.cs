using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

public class Enemy : MonoBehaviour
{
    public bool isTarget = false;
    public int health = 100;
    public float awareness = 0f;
    public float awarenessIncreaseSpeed = 30;
    public float awarenessDecreaseSpeed = 20;
    public Vector3 lastPlayerPos;
    public float distanceToPlayer = 1000;
    public int scareThreshold = 50;
    public bool isScared = false;
    public bool isBrave = false;

    [HideInInspector] public NavMeshAgent navMeshAgent;
    [HideInInspector] public Rigidbody rigidbody;
    public Katana katana;
    public GameObject player;
    public FieldOfView fieldOfView;
    public List<Transform> patrolPositions;

    private Animator enemyAI;

    // Start is called before the first frame update
    void Start()
    {
        enemyAI = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (fieldOfView.visibleTargets.Count > 0)
        {
            var distance = Vector3.Distance(fieldOfView.visibleTargets[0].position, transform.position);
            awareness += awarenessIncreaseSpeed * (fieldOfView.viewRadius / distance) * Time.deltaTime;
            lastPlayerPos = fieldOfView.visibleTargets[0].position;
            if (player == null)
            {
                player = fieldOfView.visibleTargets[0].gameObject;
            }
        }
        else
        {
            awareness -= awarenessDecreaseSpeed * Time.deltaTime;
        }

        if (player != null)
        {
            distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);
        }

        awareness = Mathf.Clamp(awareness, 0, 100);
        enemyAI.SetFloat("awareness", awareness);
        enemyAI.SetFloat("distance to player", distanceToPlayer);

        var color = Color.Lerp(Color.green, Color.red, awareness / 100);
        color.a = .5f;
        fieldOfView.viewMeshFilter.GetComponent<Renderer>().material.color = color;

        if (health < scareThreshold && !isScared && !isBrave)
        {
            if (UnityEngine.Random.Range(1,101) < (30 + 45 * Convert.ToInt32(isTarget)))
            {
                isScared = true;
            }
            else
            {
                isBrave = true;
            }
            enemyAI.SetBool("scared", isScared);
        }    

        if (transform.position.y < -100 || health < 0)
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

    public void UpdateHealth(int change)
    {
        health += change;
    }
}
