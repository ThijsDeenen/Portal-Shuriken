 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetCount : MonoBehaviour
{
    private Enemy[] enemiesInGame;
    public List<Enemy> targetsInGame;
    private SceneController sceneController;

    void Start()
    {
        enemiesInGame = gameObject.GetComponentsInChildren<Enemy>();
        for (int i = 0; i < enemiesInGame.Length; i++)
        {
            if (enemiesInGame[i].isTarget)
            {
                targetsInGame.Add(enemiesInGame[i]);
            }
        }
        sceneController = gameObject.GetComponent<SceneController>();
        Debug.Log(targetsInGame);
    }

    public void KillTarget()
    {
        targetsInGame.RemoveAt(targetsInGame.Count - 1);
        if (targetsInGame.Count <= 0)
        {
            sceneController.GoToScene("WinScreen");
        }
    }
}
