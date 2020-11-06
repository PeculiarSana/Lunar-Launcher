using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class D_InfoMenu : MonoBehaviour
{
    public bool defaultState;
    public GunManager gunManager;
    public TextMeshProUGUI t_Azimuth, t_Elevation, t_Velocity, t_Propulsion, t_ArmedStatus, t_ReadyToFire;

    float azimuth, elevation, velocity;
    bool propulsion, arming, armed, readyToFire;

    private void Start()
    {
        gameObject.SetActive(defaultState);
    }

    void Update()
    {
        azimuth = gunManager.GetAzimuth();
        elevation = gunManager.GetElevation();
        velocity = gunManager.GetVelocity();
        propulsion = gunManager.GetPropulsion();
        arming = gunManager.GetArming();
        armed = gunManager.GetArmed();
        readyToFire = gunManager.GetReadyToFire();


        t_Azimuth.text = "Azimuth: " + azimuth;
        t_Elevation.text = "Elevation: " + elevation;
        t_Velocity.text = "Velocity: " + velocity + "km/s";

        if (propulsion)
            t_Propulsion.text = "Propulsion Mode: Magnetic" + " - Switching: " + gunManager.GetChangingPropulsion();
        else
        {

        }
            t_Propulsion.text = "Propulsion Mode: Light-gas" + " - Switching: " + gunManager.GetChangingPropulsion();

        if (armed)
            t_ArmedStatus.text = "Arming Status: " + "Armed";
        else if (arming)
            t_ArmedStatus.text = "Arming Status: " + "Arming...";
        else
            t_ArmedStatus.text = "Arming Status: " + "Not Armed";

        t_ReadyToFire.text = "Ready to fire: " + readyToFire;
    }
}
