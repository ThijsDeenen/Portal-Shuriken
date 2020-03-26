using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class HealthBarController : MonoBehaviour
{
    public Player player;
    public Image health;

    private UnityEvent healthUpdateEvent;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnEnable()
    {
        healthUpdateEvent = player.healthUpdateEvent;
        healthUpdateEvent.AddListener(UpdateHealthBar);
    }

    void OnDisable()
    {
        healthUpdateEvent.RemoveListener(UpdateHealthBar);
    }

    private void UpdateHealthBar()
    {
        health.fillAmount = (player.GetHealth() / 100f);
    }
}
