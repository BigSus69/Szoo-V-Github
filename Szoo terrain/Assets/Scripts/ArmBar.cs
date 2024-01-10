using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArmBar : MonoBehaviour
{
    public Slider slider;
    public rayCastingScript rayCasting; // refererer til rayCastingScript.cs hvor vi har float startDistance
    public GameObject handRayCast;

    public void UpdateArmBar(float distance)
    {
        float startDistance = rayCasting.GetStartDistance();
        Debug.Log("startDistance to cat: " + startDistance);
        float pseudoInfinity = 1000000; // Replace this with a large number that represents "infinity" in your context
        slider.value = 1 - (distance / pseudoInfinity);
    }
}