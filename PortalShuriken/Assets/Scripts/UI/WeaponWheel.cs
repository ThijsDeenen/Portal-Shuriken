using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity;

public class WeaponWheel : MonoBehaviour
{
    private int currentWeapon;
    public GameObject[] weapons;
    private bool wheelIsOpened = false;
    private Animator anim;
    private KeyCode[] keys;
    private string pressedKey;
    private bool isOpen;
    
    void Start()
    {
        anim = GetComponent<Animator>();
        keys = new KeyCode[] { KeyCode.Alpha1, KeyCode.Alpha2, KeyCode.Alpha3, KeyCode.Alpha4, KeyCode.Tab };
    }

    void Update()
    {
        foreach (KeyCode key in keys){
            if (Input.GetKeyDown(key))
            {
                if (Input.inputString != "")
                {
                    pressedKey = Input.inputString;
                }
                else
                {
                    pressedKey = "Tab";
                }
                OpenWeaponWheel(pressedKey);
            }
        }
        foreach (KeyCode key in keys)
        {
            if (Input.GetKeyUp(key))
            {
                int keyCounter = 0;
                for (int i = 0; i < keys.Length; i++)
                {
                    if (!Input.GetKey(keys[i]))
                    {
                        keyCounter += 1;
                    }
                }
                Debug.Log(keyCounter);
                if (keyCounter == keys.Length)
                {
                    CloseWeaponWheel();
                }
            }
        }
    }

    public void OpenWeaponWheel(string keyInput)
    {
        if (!isOpen)
        {
            anim.SetTrigger("OpenWheel");
            isOpen = true;
        }
        if (keyInput != "Tab")
        {
            ChangeWeapon(int.Parse(keyInput));
        }
    }

    public void CloseWeaponWheel()
    {
        anim.SetTrigger("CloseWheel");
        isOpen = false;
    }
    public void ChangeWeapon(int number)
    {
        weapons[currentWeapon].GetComponent<WeaponWheelOption>().Disable();
        weapons[number - 1].GetComponent<WeaponWheelOption>().Enable();
        currentWeapon = number - 1;
    }
}