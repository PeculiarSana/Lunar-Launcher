using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class D_AzimuthText : MonoBehaviour
{
    public GunManager gunManager;

    float targetAzimuth;
    float currentAzimuth;

    private void OnEnable()
    {
        EventManager.Azimuth += GetAzimuth;
    }
    
    void GetAzimuth(float value)
    {
        targetAzimuth = Mathf.Round(value);
    }

    void Update()
    {
        currentAzimuth = gunManager.GetAzimuth();   
        if (gunManager.GetAzimuth() > 136)
            currentAzimuth -= 360;
        gameObject.GetComponent<TextMeshPro>().text = "Azimuth - Target: " + targetAzimuth + " Current: " + Mathf.Round(currentAzimuth);
    }
}
