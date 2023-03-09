using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerWeaponManager : MonoBehaviour
{

    [SerializeField] private CoffeeGun cofffeWeapon;
    [SerializeField] private SwordAttack colher;

    M2_cooldown m2_cooldown;
    private int weaponID = 1;

    void Start()
    {
        m2_cooldown = GameObject.Find("M2_Cooldown").GetComponent<M2_cooldown>();

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

    public void OnRightClick(InputAction.CallbackContext context)
    {
        switch (weaponID)
        {
            case 0:
                cofffeWeapon.OnRightClick(context);
                break;
            case 1:
                if (context.started) colher.OnRightClick();
                break;
        }
    }



    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            SetWeapon(0);
            m2_cooldown.switchSprites(0);
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            SetWeapon(1);
            m2_cooldown.switchSprites(1);
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
