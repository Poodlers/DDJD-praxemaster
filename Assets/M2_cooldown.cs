using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class M2_cooldown : MonoBehaviour
{
    public Slider slider;
    public Image UI_M2;
    public Image fill;
    public Sprite colher_M2;
    public Sprite coffee_M2;

    void Start()
    {

        UI_M2.sprite = colher_M2;
    }

    public void switchSprites(int weaponID)
    {
        if (weaponID == 0)
        {
            UI_M2.sprite = coffee_M2;
        }
        else if (weaponID == 1)
        {
            UI_M2.sprite = colher_M2;
        }
    }

    public void SetMaxCooldown(float cooldown)
    {
        slider.maxValue = cooldown;
        slider.value = cooldown;
    }
    public void SetCoolDown(float cooldown)
    {
        slider.value = cooldown;
    }
}
