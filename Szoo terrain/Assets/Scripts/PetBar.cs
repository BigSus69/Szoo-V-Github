using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PetBar : MonoBehaviour
{
    public Slider slider;
    public Camera camera;

    public void UpdatePealthBar(float Pealth, float MaxPealth)
    {
        slider.value = Pealth / MaxPealth;
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = camera.transform.rotation;
    }
}
