using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity;

public class WeaponWheel : MonoBehaviour
{
    public int currentWeapon;
    public GameObject[] weapons;
    private Animator wheelAnim;
    public Animator katanaAnim;
    private KeyCode[] keys;
    private string pressedKey;
    private bool isOpen;
    private bool katanaDrawn;
    
    void Start()
    {
        wheelAnim = GetComponent<Animator>();
        keys = new KeyCode[] { KeyCode.Alpha1, KeyCode.Alpha2, KeyCode.Alpha3, KeyCode.Alpha4, KeyCode.Tab };
        DrawKatana();
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
                if (keyCounter == keys.Length)
                {
                    CloseWeaponWheel();
                }
            }
        }
    }

    private void OpenWeaponWheel(string keyInput)
    {
        if (!isOpen)
        {
            wheelAnim.SetTrigger("OpenWheel");
            isOpen = true;
        }
        if (keyInput != "Tab")
        {
            ChangeWeapon(int.Parse(keyInput));
        }
    }

    private void CloseWeaponWheel()
    {
        wheelAnim.SetTrigger("CloseWheel");
        isOpen = false;
    }
    private void ChangeWeapon(int number)
    {
        weapons[currentWeapon].GetComponent<WeaponWheelOption>().Disable();
        currentWeapon = number - 1;
        weapons[currentWeapon].GetComponent<WeaponWheelOption>().Enable();
        if (currentWeapon == 0)
        {
            DrawKatana();
        }
        else
        {
            PutAwayKatana();
        }
    }

    private void DrawKatana()
    {
        if (!katanaDrawn)
        {
            katanaAnim.SetTrigger("Draw Katana");
            katanaDrawn = true;
        }
    }

    private void PutAwayKatana()
    {
        if (katanaDrawn)
        {
            katanaAnim.SetTrigger("Put Away Katana");
            katanaDrawn = false;
        }
    }
}