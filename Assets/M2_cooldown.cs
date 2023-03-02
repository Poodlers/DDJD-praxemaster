using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class M2_cooldown : MonoBehaviour
{
    public Slider slider;

    public void SetMaxCooldown(int cooldown)
    {
        slider.maxValue = cooldown;
        slider.value = cooldown;
    }
    public void SetCoolDown(int cooldown)
    {
        slider.value = cooldown;
    }
}
