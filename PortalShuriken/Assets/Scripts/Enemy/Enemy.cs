using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public bool isTarget = false;
    public int health = 100;
    public float awareness = 0f;
    public float awarenessIncreaseSpeed = 20;
    public float awarenessDecreaseSpeed = 5;
    public Vector3 lastPlayerPos;
    public float distanceToPlayer = 1000;
    public int scareThreshold = 50;
    public bool isScared = false;
    public bool isBrave = false;

    [HideInInspector] public NavMeshAgent navMeshAgent;
    [HideInInspector] public Rigidbody rigidbody;
    [HideInInspector] public GameObject player;
    public Katana katana;
    public FieldOfView fieldOfView;
    public FieldOfView chaseDetection;
    public List<Transform> patrolPositions;
    public Animator anim;

    private Animator enemyAI;
    private TargetCount targetCount;
    private float lastFallVelocity = 0;
    private bool hasDied = false;

    // Start is called before the first frame update
    void Start()
    {
        enemyAI = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        rigidbody = GetComponent<Rigidbody>();
        targetCount = gameObject.transform.parent.GetComponent<TargetCount>();
        if(isScared)
        {
            enemyAI.SetBool("scared", isScared);
        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdateAwareness();
        UpdateAI();

        if (health < scareThreshold && !isScared && !isBrave)
        {
            ScareCheck();
        }

        if (transform.position.y < -100 || health <= 0)
        {
            if (!hasDied)
            {
                katana.GetComponent<Animator>().SetTrigger("Die");
                gameObject.GetComponent<Animator>().enabled = false;
                gameObject.GetComponent<NavMeshAgent>().enabled = false;
                fieldOfView.gameObject.SetActive(false);
                anim.SetTrigger("Die");
                hasDied = true;
            }
        }

        if (rigidbody.velocity.y < -10 || lastFallVelocity < 0)
        {
            CalculateFallDamage();
        }
    }

    public void UpdateHealth(bool playerDamage, int change)
    {
        health += change;

        if (playerDamage && enemyAI.GetCurrentAnimatorStateInfo(0).IsName("Patrol"))
        {
            health = 0;
        }
    }

    private void UpdateAwareness()
    {
        if (fieldOfView.visibleTargets.Count > 0)
        {
            if (player == null)
            {
                player = fieldOfView.visibleTargets[0].gameObject;
            }

            var distance = Vector3.Distance(fieldOfView.visibleTargets[0].position, transform.position);
            awareness += awarenessIncreaseSpeed * (fieldOfView.viewRadius / distance) * Time.deltaTime;

            lastPlayerPos = player.transform.position;
            distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);
        }
        else if (enemyAI.GetCurrentAnimatorStateInfo(0).IsName("Chase") && chaseDetection.visibleTargets.Count > 0)
        {
            lastPlayerPos = chaseDetection.visibleTargets[0].position;
            distanceToPlayer = 1000f;
        }
        else
        {
            awareness -= awarenessDecreaseSpeed * Time.deltaTime;
            distanceToPlayer = 1000f;
        }

        awareness = Mathf.Clamp(awareness, 0, 100);
    }


    private void UpdateAI()
    {
        enemyAI.SetFloat("awareness", awareness);
        enemyAI.SetFloat("distance to player", distanceToPlayer);

        var color = Color.Lerp(Color.green, Color.red, awareness / 100);
        color.a = .5f;
        fieldOfView.viewMeshFilter.GetComponent<Renderer>().material.color = color;
    }

    private void ScareCheck()
    {
        if (UnityEngine.Random.Range(1, 101) < (30 + 45 * Convert.ToInt32(isTarget)))
        {
            isScared = true;
        }
        else
        {
            isBrave = true;
        }
        enemyAI.SetBool("scared", isScared);
    }

    private void CalculateFallDamage()
    {
        if (rigidbody.velocity.y > lastFallVelocity)
        {
            UpdateHealth(false, (int)lastFallVelocity * 4);
        }
        lastFallVelocity = rigidbody.velocity.y;
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
        rigidbody.isKinematic = false;

        StartCoroutine("Focus");
    }

    private IEnumerator Focus()
    {
        yield return new WaitForSeconds(5f);
        enemyAI.SetBool("stunned", false);
        rigidbody.isKinematic = true;
        yield return null;
    }

    public void Die()
    {
        if (isTarget)
        {
            targetCount.KillTarget();
        }
        Destroy(transform.gameObject);
    }
}
