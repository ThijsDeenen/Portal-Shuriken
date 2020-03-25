using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kunai : MonoBehaviour
{
    private GameObject player;

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.GetComponent<Player>())
        {
            Physics.IgnoreCollision(col.gameObject.GetComponent<CharacterController>(), GetComponent<CapsuleCollider>());
        }

        if (col.gameObject.GetComponent<Enemy>() || col.gameObject.transform.parent != null && col.gameObject.transform.parent.GetComponent<Enemy>())
        {
            player.transform.position = transform.position;
            Destroy(transform.gameObject);
        }
    }

    public void SetPlayer(GameObject player)
    {
        this.player = player;
    }
}
