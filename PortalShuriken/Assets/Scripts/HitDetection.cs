using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitDetection : MonoBehaviour
{
    private Player player;

    void Start()
    {
        player = transform.parent.GetComponent<Player>();
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.GetComponent<Enemy>())
        {
            player.UpdateHealth(-20);
        }
    }
}
