using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class D_ElevationText : MonoBehaviour
{
    public GunManager gunManager;

    float targetElevation;
    float currentElevation;

    private void OnEnable()
    {
        EventManager.Elevation += GetElevation;
    }
    
    void GetElevation(float value)
    {
        targetElevation = Mathf.Round(value);
    }

    void Update()
    {
        currentElevation = gunManager.GetElevation();
        gameObject.GetComponent<TextMeshPro>().text = "Elevation - Target: " + targetElevation + " Current: " + Mathf.Round(currentElevation);
    }
}
