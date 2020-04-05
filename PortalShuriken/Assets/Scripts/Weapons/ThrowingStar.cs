using UnityEngine;
using System.Linq;
using UnityEngine.AI;

public class ThrowingStar : MonoBehaviour
{
    private float ejectSpeed = 8f;
    private bool isFirst = true;
    private bool isStuck = false;
    private GameObject firstStar;

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

                col.gameObject.GetComponent<Enemy>().StunEnemy();
                col.gameObject.GetComponent<Rigidbody>().isKinematic = false;
                col.gameObject.transform.position = newPos;
                col.gameObject.transform.rotation = newRotation;
                col.gameObject.GetComponent<Rigidbody>().velocity = firstStar.transform.forward * ejectSpeed;
                Destroy(transform.gameObject);
            }
        }

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
