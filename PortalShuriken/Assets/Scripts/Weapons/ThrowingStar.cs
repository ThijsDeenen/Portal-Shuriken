using UnityEngine;
using System.Linq;
using UnityEngine.AI;

public class ThrowingStar : MonoBehaviour
{
    private float ejectSpeed = 8f;
    private bool isFirst = true;
    private bool isStuck = false;
    private GameObject firstStar;
    public GameObject teleportParticles;
    private Vector3 newEnemyPos;
    private GameObject teleportingEnemy;
    private Quaternion enemyRotation;
    private GameObject throwingStar;
    private GameObject particles;

    private void Start()
    {
        throwingStar = this.gameObject;
    }

    void FixedUpdate()
    {
        RaycastHit hit;
        float distance = .5f;

        if (!isStuck && Physics.Raycast(transform.position, transform.forward, out hit, distance))
        {
            if (!(hit.collider.gameObject.transform.parent != null && hit.collider.gameObject.transform.parent.GetComponent<Player>()))
            {
                GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                transform.position = hit.point;
                transform.rotation = Quaternion.LookRotation(hit.normal);
                isStuck = true;
            }
        }
    }

    void OnCollisionEnter(Collision col)
    {
        if (!isFirst)
        {
            if (col.gameObject.transform.GetComponent<Enemy>())
            {
                var newPos = firstStar.transform.position + firstStar.transform.forward;
                var newRotation = firstStar.transform.rotation;
                newRotation = Quaternion.Euler(new Vector3(0f, newRotation.eulerAngles.y, 0f));

                teleportingEnemy = col.gameObject;
                particles = Instantiate(teleportParticles, col.gameObject.transform.position, Quaternion.identity);
                particles.GetComponent<TeleportParticles>().AssignPositions(transform.position, newPos, throwingStar);
                teleportingEnemy.SetActive(false);
                newEnemyPos = newPos;
                enemyRotation = newRotation;
            }
        }
    }

    public void TeleportEnemy()
    {
        teleportingEnemy.SetActive(true);
        teleportingEnemy.GetComponent<Enemy>().StunEnemy();
        teleportingEnemy.GetComponent<Rigidbody>().isKinematic = false;
        teleportingEnemy.transform.position = newEnemyPos;
        teleportingEnemy.transform.rotation = enemyRotation;
        teleportingEnemy.GetComponent<Rigidbody>().velocity = firstStar.transform.forward * ejectSpeed;
        Destroy(particles);
        Destroy(transform.gameObject);
    }

    public void SetFirstStarInfo(GameObject firstStar)
    {
        if (firstStar != null)
        {
            this.firstStar = firstStar;
            isFirst = false;
        }
        GetComponent<Renderer>().material.SetColor("_Color", Color.blue);
    }
}
