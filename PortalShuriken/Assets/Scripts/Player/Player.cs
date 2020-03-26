using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    [SerializeField] private int health = 100;
    public UnityEvent healthUpdateEvent = new UnityEvent();

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateHealth(int change)
    {
        health += change;
        healthUpdateEvent.Invoke();
    }

    public int GetHealth()
    {
        return health;
    }
}
