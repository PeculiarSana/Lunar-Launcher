using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO : Interpolate gun movement for more animation detail

public class GunManager : MonoBehaviour
{
    public float translationSpeed, elevationSpeed;

    public GameObject Translator, Elevator;

    float targetAzimuth = 0;
    float targetElevation = 0;

    //Register to events coming from interactable objects
    private void OnEnable()
    {
        EventManager.Azimuth += SetAzimuth;
        EventManager.Elevation += SetElevation;
    }

    void SetAzimuth(float value)
    {
        targetAzimuth = value;
    }
    public float GetAzimuth()
    {
        return Translator.transform.localRotation.eulerAngles.y;
    }

    void SetElevation(float value)
    {
        targetElevation = value;
    }
    
    public float GetElevation()
    {
        return -(Elevator.transform.localRotation.eulerAngles.x - 360);
    }

    void Update()
    {
        //Translation - Azimuth
        Quaternion transRot = Quaternion.Euler(new Vector3(Translator.transform.rotation.eulerAngles.x, targetAzimuth, Translator.transform.rotation.eulerAngles.z));
        Translator.transform.rotation = Quaternion.RotateTowards(Translator.transform.rotation, transRot, translationSpeed * Time.deltaTime);

        //Elevation
        Quaternion eleRot = Quaternion.Euler(new Vector3(-targetElevation, Elevator.transform.rotation.eulerAngles.y, Elevator.transform.rotation.eulerAngles.z));
        Elevator.transform.rotation = Quaternion.RotateTowards(Elevator.transform.rotation, eleRot, elevationSpeed * Time.deltaTime);
    }
}
