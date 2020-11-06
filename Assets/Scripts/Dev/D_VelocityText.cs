using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class D_VelocityText : MonoBehaviour
{
    public GunManager gunManager;

    float velocity;

    private void OnEnable()
    {
        EventManager.Velocity += GetVelocity;
    }
    
    void GetVelocity(float value)
    {
        velocity = Mathf.Round(value);
    }

    void Update()
    {
        gameObject.GetComponent<TextMeshPro>().text = Mathf.Round(velocity) + "km/s";
    }
}
