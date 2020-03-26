using UnityEngine;
using System.Linq;

public class ThrowingStar : MonoBehaviour
{
    private bool isFirst = true;
    private GameObject firstStar;

    void FixedUpdate()
    {
        RaycastHit hit;
        float distance = .5f;

        if (Physics.Raycast(transform.position, transform.forward * -1, out hit, distance))
        {
            if (hit.collider.gameObject.transform.parent != null && !hit.collider.gameObject.transform.parent.GetComponent<Player>())
            {
                GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                transform.position = hit.point;
            }
        }
    }

    void OnCollisionEnter(Collision col)
    {
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        if (!isFirst)
        {
            if (col.gameObject.transform.parent != null && col.gameObject.transform.parent.GetComponent<Enemy>())
            {
                var newPos = firstStar.transform.position;
                var newRotation = firstStar.transform.rotation;
                newRotation = Quaternion.Euler(new Vector3(0f, newRotation.eulerAngles.y, 0f));

                col.gameObject.transform.parent.position = newPos;
                col.gameObject.transform.parent.rotation = newRotation;
                col.gameObject.transform.parent.GetComponent<CharacterController>().Move(firstStar.transform.forward * .5f);
                col.gameObject.transform.parent.GetComponent<EnemyMovement>().addVelocity(firstStar.transform.forward * 15f);
                Destroy(transform.gameObject);
            }
        }

    }

    public void setFirstStarInfo(GameObject firstStar)
    {
        this.firstStar = firstStar;
        isFirst = false;
        GetComponent<Renderer>().material.SetColor("_Color", Color.blue);
    }
}
