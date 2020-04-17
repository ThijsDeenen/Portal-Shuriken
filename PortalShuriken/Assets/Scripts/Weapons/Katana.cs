using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Katana : MonoBehaviour
{
    public int swingDamage = 30;

    private Animator katanaAnim;
    private Collider katanaCol;

    private List<GameObject> hitEnemies = new List<GameObject>();
    private bool drawn = false;

    // Start is called before the first frame update
    void Start()
    {
        katanaAnim = GetComponent<Animator>();
        katanaCol = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (katanaCol.enabled && !katanaAnim.GetCurrentAnimatorStateInfo(0).IsName("Swing"))
        {
            katanaCol.enabled = false;
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.transform.parent && col.transform.parent.GetComponent<Enemy>() && transform.parent.GetComponent<Player>())
        {
            foreach (GameObject e in hitEnemies)
            {
                if (e == col.transform.parent.gameObject)
                {
                    return;
                }
            }
            col.transform.parent.GetComponent<Enemy>().UpdateHealth(-swingDamage);
            hitEnemies.Add(col.transform.parent.gameObject);
        }
        else if (col.GetComponent<Player>() && transform.parent.GetComponent<Enemy>())
        {
            foreach (GameObject e in hitEnemies)
            {
                if (e == col.gameObject)
                {
                    return;
                }
            }
            col.GetComponent<Player>().UpdateHealth(-swingDamage);
            hitEnemies.Add(col.gameObject);
        }
    }

    public void Draw()
    {
        if (!drawn)
        {
            katanaAnim.SetTrigger("Draw Katana");
            drawn = true;
        }
    }

    public void PutAway()
    {
        if (drawn)
        {
            katanaAnim.SetTrigger("Put Away Katana");
            drawn = false;
        }
    }

    public void Swing()
    {
        if (katanaCol.enabled == false)
        {
            hitEnemies.Clear();
            katanaCol.enabled = true;
            katanaAnim.SetTrigger("Swing Katana");
        }
    }

    public void EnableTrail()
    {
        transform.GetChild(4).gameObject.SetActive(true);
    }

    public void DisableTrail()
    {
        transform.GetChild(4).gameObject.SetActive(false);
    }
}
