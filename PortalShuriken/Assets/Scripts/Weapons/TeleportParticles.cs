using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportParticles : MonoBehaviour
{
    private bool isTeleporting = false;
    private Vector3 endPosition;
    private GameObject throwingStar;
    public float speed;

    void Update()
    {
        //check if enemy is currently teleporting and if so, move from original position to target shuriken
        if (isTeleporting)
        {
            transform.position = Vector3.MoveTowards(transform.position, endPosition, speed * Time.deltaTime);
        }
        //if particles have reached endposition, teleport the enemy towards endposition
        if (transform.position == endPosition)
        {
            throwingStar.GetComponent<ThrowingStar>().TeleportEnemy();
        }
    }

    public void AssignPositions(Vector3 startPos, Vector3 endPos, GameObject targetStar)
    {
        //declare values for teleportation and start teleportation
        endPosition = endPos;
        transform.position = startPos;
        throwingStar = targetStar;
        isTeleporting = true;
    }
}
