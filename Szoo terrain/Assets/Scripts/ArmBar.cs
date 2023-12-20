using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmBar : MonoBehaviour
{
    public Slider slider;

    public void UpdateArmBar(float Pealth)
    {
        float pseudoInfinity = 1000000; // Replace this with a large number that represents "infinity" in your context
        slider.value = 1 - (Pealth / pseudoInfinity);
    }
}
