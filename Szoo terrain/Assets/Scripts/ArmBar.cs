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
    public HandController handController; // Add this line

public float speed = 1f;

void Update()
{
    float startDistance = rayCasting.GetStartDistance();
    float distanceToCat = handController.DistanceToCat; 

    // Calculate the difference between startDistance and distanceToCat
    float distanceDifference = Mathf.Abs(startDistance - distanceToCat);

    // Normalize the distance difference to a range between 0 and 1
    // Assuming that the maximum possible difference is equal to startDistance
    float targetValue = Mathf.Clamp01(distanceDifference / startDistance);

    // Set the slider value
    slider.value = targetValue;

    // Add debug logs
    Debug.Log("distanceDifference: " + distanceDifference);
    Debug.Log("slider.value: " + slider.value);
}
}