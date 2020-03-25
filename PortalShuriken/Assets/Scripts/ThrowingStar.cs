using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowingStar : MonoBehaviour
{
    private bool isFirst = true;
    private Vector3 positionFirstStar;

    void FixedUpdate()
    {
        RaycastHit hit;
        float distance = .25f;

        if (Physics.Raycast(transform.position, transform.forward, out hit, distance))
        {
            if (!hit.collider.gameObject.GetComponent<Enemy>() && hit.collider.gameObject.transform.parent != null && !hit.collider.gameObject.transform.parent.GetComponent<Enemy>())
            {
                GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            }
        }
    }

    void OnCollisionEnter(Collision col)
    {
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        if (!isFirst)
        {
            if (col.gameObject.GetComponent<Enemy>() || col.gameObject.transform.parent != null && col.gameObject.transform.parent.GetComponent<Enemy>())
            {
                col.gameObject.GetComponent<Rigidbody>().position = positionFirstStar;
                Destroy(transform.gameObject);
            }
        }
        
    }

    public void setFirstInfo(Vector3 position)
    {
        isFirst = false;
        positionFirstStar = position;
        GetComponent<Renderer>().material.SetColor("_Color", Color.blue);
    }
}
