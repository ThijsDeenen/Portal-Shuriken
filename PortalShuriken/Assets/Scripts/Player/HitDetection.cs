using UnityEngine;

public class HitDetection : MonoBehaviour
{
    public Player player;

    void Start()
    {

    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.transform.parent != null && col.gameObject.transform.parent.GetComponent<Enemy>())
        {
            player.UpdateHealth(-20);
        }
    }
}
