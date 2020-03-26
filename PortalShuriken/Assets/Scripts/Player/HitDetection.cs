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
        if (col.gameObject.transform.parent != null && col.gameObject.transform.parent.GetComponent<Enemy>())
        {
            player.UpdateHealth(-20);
        }
    }
}
