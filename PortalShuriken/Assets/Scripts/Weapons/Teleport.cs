using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    private Transform[] objectsToDisable;
    private Component[] behavioursToDisable;
    private GameObject cam;
    private Vector3 kunaiPos;
    public float teleportSpeed;
    private bool isTeleporting = false;
    private Vector3 targetDir;
    private Quaternion targetQuat;
    public float rotationSpeed;

    private void Start()
    {
        objectsToDisable = gameObject.GetComponentsInChildren<Transform>();

        cam = gameObject.transform.GetChild(0).gameObject;

        behavioursToDisable = gameObject.GetComponents(typeof(Behaviour));
    }

    private void Update()
    {
        if (isTeleporting)
        {
            targetQuat = Quaternion.LookRotation(targetDir);
            cam.transform.rotation = Quaternion.Slerp(cam.transform.rotation, targetQuat, rotationSpeed * Time.deltaTime);

            transform.position = Vector3.MoveTowards(transform.position, kunaiPos, teleportSpeed * Time.deltaTime);
        }
        if (transform.position == kunaiPos)
        {
            isTeleporting = false;
            EndTeleport();
        }
    }

    public void PrepareToTeleport(Vector3 kunai)
    {
        //disable all the player's child objects except the first (which is the camera)
        for (int i = 2; i < objectsToDisable.Length - 1; i++)
        {
            objectsToDisable[i].gameObject.SetActive(false);
        }
        //disable the ability to control the camera with the mouse
        cam.GetComponent<MouseLook>().enabled = false; 
        //disable all behaviours in Player gameObject
        foreach (Behaviour item in behavioursToDisable)
        {
            if (item != GetComponent(typeof(Teleport)))
            {
                item.enabled = false;
            }
        }
        gameObject.GetComponent<CharacterController>().enabled = false;
        kunaiPos = kunai;
        targetDir = (kunaiPos - transform.position).normalized;
        isTeleporting = true;
    }

    public void EndTeleport()
    {
        for (int i = 2; i < objectsToDisable.Length - 1; i++)
        {
            objectsToDisable[i].gameObject.SetActive(true);
        }

        cam.GetComponent<MouseLook>().enabled = true;

        foreach (Behaviour item in behavioursToDisable)
        {
            if (item != GetComponent(typeof(Teleport)))
            {
                item.enabled = true;
            }
        }
        gameObject.GetComponent<CharacterController>().enabled = true;
    }
}
