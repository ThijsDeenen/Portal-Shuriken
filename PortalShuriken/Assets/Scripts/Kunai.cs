using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kunai : MonoBehaviour
{
    private GameObject player;
    private bool broken = false;

    void OnCollisionEnter(Collision col)
    {
        if (!broken)
        {
            if (col.gameObject.GetComponent<Enemy>() || col.gameObject.transform.parent != null && col.gameObject.transform.parent.GetComponent<Enemy>())
            {
                var position = transform.position;
                if (position.y < 0)
                {
                    position.y = 0;
                }
                player.transform.position = position;
                Destroy(transform.gameObject);
            }
            else if (!col.gameObject.GetComponent<Player>())
            {
                broken = true;
            }
        }
    }

    public void SetPlayer(GameObject player)
    {
        this.player = player;
    }
}
