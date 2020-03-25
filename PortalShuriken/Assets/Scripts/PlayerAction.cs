using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    public GameObject KunaiPrefab;
    public GameObject ThrownObjects;

    private List<GameObject> thrownKunais = new List<GameObject>();
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
    }

    private void ThrowKunai()
    {
        kunaiCount++;

        Vector3 direction = Camera.main.transform.forward;

        Vector3 position = Camera.main.transform.position;
        position += direction * 1f;
        position.y -= 0.1f;

        Quaternion rotation = Camera.main.transform.rotation;
        rotation *= Quaternion.Euler(90, 90, 90);

        var kunai = Instantiate(KunaiPrefab, position, rotation, ThrownObjects.transform);
        kunai.transform.name = "Kunai " + kunaiCount;
        thrownKunais.Add(kunai);

        kunai.GetComponent<Rigidbody>().velocity = direction * 50f;

        kunai.GetComponent<Kunai>().SetPlayer(transform.gameObject);
    }
}
