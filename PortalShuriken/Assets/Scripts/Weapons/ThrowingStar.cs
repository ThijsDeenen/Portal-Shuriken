using UnityEngine;

public class ThrowingStar : MonoBehaviour
{
    private float ejectSpeed = 8f;
    private bool isFirst = true;
    private bool isStuck = false;
    private GameObject firstStar;
    public GameObject teleportParticles;
    private Vector3 newEnemyPos;
    private GameObject enemy;
    private Quaternion enemyRotation;
    private GameObject particles;

    private void Start()
    {

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
        if (col.gameObject.transform.GetComponent<Enemy>())
        {
            enemy = col.gameObject;

            if (!isFirst)
            {
                GetComponent<MeshRenderer>().enabled = false;
                var newPos = firstStar.transform.position + firstStar.transform.forward;
                var newRotation = firstStar.transform.rotation;
                newRotation = Quaternion.Euler(new Vector3(0f, newRotation.eulerAngles.y, 0f));

                particles = Instantiate(teleportParticles, col.gameObject.transform.position, Quaternion.identity);
                particles.GetComponent<TeleportParticles>().AssignPositions(transform.position, newPos, gameObject);
                enemy.SetActive(false);
                newEnemyPos = newPos;
                enemyRotation = newRotation;
            }
            else
            {
                transform.parent = enemy.transform;
                GetComponent<Collider>().enabled = false;
            }
        }
    }

    public void TeleportEnemy()
    {
        enemy.SetActive(true);
        enemy.GetComponent<Enemy>().StunEnemy();
        enemy.transform.position = newEnemyPos;
        enemy.transform.rotation = enemyRotation;
        enemy.GetComponent<Rigidbody>().velocity = firstStar.transform.forward * ejectSpeed;
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
