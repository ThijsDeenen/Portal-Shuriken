using UnityEngine;

public class Kunai : MonoBehaviour
{
    private GameObject player;
    private Teleport teleport;
    private bool broken = false;

    private void Start()
    {
        teleport = player.GetComponent<Teleport>();
    }
    void OnCollisionEnter(Collision col)
    {
        if (!broken)
        {
            if (col.gameObject.GetComponent<Enemy>() || col.gameObject.transform.parent != null && col.gameObject.transform.parent.GetComponent<Enemy>())
            {
                var position = transform.position;
                teleport.PrepareToTeleport(transform.position);
                //player.transform.position = position;
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
