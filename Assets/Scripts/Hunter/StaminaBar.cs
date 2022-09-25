using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaBar : MonoBehaviour
{
    public Slider staminaSlider;

    public float maxStamina;
    public float currentStamina;

    private void Start()
    {
        currentStamina = maxStamina;
        staminaSlider.maxValue = maxStamina;
        staminaSlider.value = maxStamina;
    }


    public void UseStamina(float value)
    {
        if (currentStamina >=0)
        {
            currentStamina -= value;
            staminaSlider.value = currentStamina;
        }    
    }

    public void AddStamina(float value)
    {
        if (currentStamina < maxStamina)
        {
            currentStamina += value;
            staminaSlider.value = currentStamina;
        }
    }

}
