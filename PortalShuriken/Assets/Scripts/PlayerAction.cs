using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Security.Cryptography;

public class PlayerAction : MonoBehaviour
{
    public GameObject KunaiPrefab;
    public GameObject ThrowingStarPrefab;
    public GameObject shuriken;

    private List<GameObject> thrownKunais = new List<GameObject>();
    private GameObject firstThrownStar;
    private GameObject seccondThrownStar;
    private int kunaiCount;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ThrowKunai();
        }
        if (Input.GetMouseButtonDown(1))
        {
            ThrowStar();
        }
    }

    private void ThrowKunai()
    {
        kunaiCount++;

        if (thrownKunais.Count >= 10)
        {
            GameObject Kunai = thrownKunais.First().gameObject;
            Destroy(Kunai);
            thrownKunais.Remove(Kunai);
        }

        Vector3 direction = Camera.main.transform.forward;

        Vector3 position = Camera.main.transform.position;
        position += direction * 1f;
        position.y -= 0.1f;

        Quaternion rotation = Camera.main.transform.rotation;
        rotation *= Quaternion.Euler(90, 90, 90);

        var kunai = Instantiate(KunaiPrefab, position, rotation, shuriken.transform);
        kunai.transform.name = "Kunai " + kunaiCount;
        thrownKunais.Add(kunai);

        kunai.GetComponent<Rigidbody>().velocity = direction * 50f;

        kunai.GetComponent<Kunai>().SetPlayer(transform.gameObject);
    }

    private void ThrowStar()
    {
        Vector3 direction = Camera.main.transform.forward;

        Vector3 position = Camera.main.transform.position;
        position += direction * 1f;
        position.y -= 0.1f;

        Quaternion rotation = Camera.main.transform.rotation;

        var thrownStar = Instantiate(ThrowingStarPrefab, position, rotation, shuriken.transform);
        thrownStar.transform.name = "First Star";

        thrownStar.GetComponent<Rigidbody>().velocity = direction * 50f;

        if (firstThrownStar == null)
        {
            firstThrownStar = thrownStar;
        }
        else
        {
            if (seccondThrownStar != null)
            {
                Destroy(seccondThrownStar.gameObject);
            }
            seccondThrownStar = thrownStar;
            seccondThrownStar.GetComponent<ThrowingStar>().setFirstInfo(firstThrownStar.transform.position);
        }
        
        
    }
}
