using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    public WeaponWheel weaponWheel;
    public Katana katana;
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
            switch (weaponWheel.currentWeapon)
            {
                case 0:
                    SwingKatana();
                    break;
                case 1:
                    ThrowKunai();
                    break;
                case 2:
                    ThrowSecondStar();
                    break;
            }

        }
        if (Input.GetMouseButtonDown(1))
        {
            switch (weaponWheel.currentWeapon)
            {
                case 0:

                    break;
                case 1:

                    break;
                case 2:
                    ThrowFirstStar();
                    break;
            }
        }
    }

    private void SwingKatana()
    {
        katana.Swing();
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

    private void ThrowFirstStar()
    {
        var thrownStar = ThrowStar();

        if (firstThrownStar != null)
        {
            Destroy(firstThrownStar);
        }

        firstThrownStar = thrownStar;
        thrownStar.transform.name = "First Star";
    }

    private void ThrowSecondStar()
    {
        var thrownStar = ThrowStar();

        if (seccondThrownStar != null)
        {
            Destroy(seccondThrownStar.gameObject);
        }

        seccondThrownStar = thrownStar;
        thrownStar.transform.name = "Second Star";
        seccondThrownStar.GetComponent<ThrowingStar>().SetFirstStarInfo(firstThrownStar);
    }

    private GameObject ThrowStar()
    {
        Vector3 direction = Camera.main.transform.forward;

        Vector3 position = Camera.main.transform.position;
        position += direction * 1f;
        position.y -= 0.1f;

        Quaternion rotation = Camera.main.transform.rotation;

        var thrownStar = Instantiate(ThrowingStarPrefab, position, rotation, shuriken.transform);
        thrownStar.GetComponent<Rigidbody>().velocity = direction * 50f;

        return thrownStar;
    }
}
