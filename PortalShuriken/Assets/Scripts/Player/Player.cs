using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    [SerializeField] private int health = 100;
    [HideInInspector] public UnityEvent healthUpdateEvent = new UnityEvent();
    public Death death;
    private Transform[] objectsToDisable;
    private Component[] behavioursToDisable;
    private GameObject cam;
    private void Start()
    {
        objectsToDisable = gameObject.GetComponentsInChildren<Transform>();

        cam = gameObject.transform.GetChild(0).gameObject;

        behavioursToDisable = gameObject.GetComponents(typeof(Behaviour));
    }
    public void UpdateHealth(int change)
    {
        health += change;
        healthUpdateEvent.Invoke();
        if (health <= 0)
        {
            Die();
        }
    }

    public int GetHealth()
    {
        return health;
    }

    private void Die()
    {
        death.GameOverUI();
        for (int i = 2; i < objectsToDisable.Length - 1; i++)
        {
            objectsToDisable[i].gameObject.SetActive(false);
        }

        cam.GetComponent<MouseLook>().enabled = false;

        foreach (Behaviour item in behavioursToDisable)
        {
            if (item != GetComponent(typeof(Teleport)))
            {
                item.enabled = false;
            }
        }
        gameObject.GetComponent<CharacterController>().enabled = false;
    }
}
