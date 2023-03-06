using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerWeaponManager : MonoBehaviour
{

    [SerializeField] private CoffeeGun cofffeWeapon;
    [SerializeField] private SwordAttack colher;

    private int weaponID = 1;

    void Start()
    {

    }

    public void OnFire()
    {
        switch (weaponID)
        {
            case 0:
                cofffeWeapon.OnFire();
                break;
            case 1:
                colher.OnFire();
                break;
        }

    }

    public void OnRightClick()
    {
        switch (weaponID)
        {
            case 0:
                cofffeWeapon.OnRightClick();
                break;
            case 1:
                colher.OnRightClick();
                break;
        }
    }



    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            SetWeapon(0);
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            SetWeapon(1);
        }

    }

    public void OnDefeated()
    {
        switch (weaponID)
        {
            case 0:
                cofffeWeapon.transform.Rotate(0, 0, -90);

                break;
            case 1:
                colher.transform.Rotate(0, 0, -90);
                break;
        }
    }

    void SetWeapon(int weaponInput)
    {
        switch (weaponInput)
        {
            case 0:
                cofffeWeapon.gameObject.SetActive(true);
                colher.gameObject.SetActive(false);
                weaponID = 0;
                break;
            case 1:
                cofffeWeapon.gameObject.SetActive(false);
                colher.gameObject.SetActive(true);
                weaponID = 1;
                break;
        }
    }
}
