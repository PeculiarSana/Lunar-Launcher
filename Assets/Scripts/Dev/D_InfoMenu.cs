using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class D_InfoMenu : MonoBehaviour
{
    public bool defaultState;
    public InputManager inputManager;
    public GunManager gunManager;
    public TextMeshProUGUI t_Azimuth, t_AzimuthTarget, t_Elevation, t_ElevationTarget, t_Velocity, t_AdjustingTarget, t_Propulsion, t_ArmedStatus, t_ReadyToFire;
    public TextMeshProUGUI t_FPS;
    public TextMeshProUGUI t_MousePosition, t_MouseTarget;

    float f_Azimuth, f_AzimuthTarget, f_Elevation, f_ElevationTarget, f_Velocity;
    float deltaTime = 0.0f;
    bool b_Propulsion, b_Arming, b_Armed, b_ReadyToFire;

    private void Start()
    {
        gameObject.SetActive(defaultState);
    }

    void Update()
    {
        f_Azimuth = gunManager.GetAzimuth();
        EventManager.Azimuth += GetAzimuth;
        f_Elevation = gunManager.GetElevation();
        EventManager.Elevation += GetElevation;
        f_Velocity = gunManager.GetVelocity();
        b_Propulsion = gunManager.GetPropulsion();
        b_Arming = gunManager.GetArming();
        b_Armed = gunManager.GetArmed();
        b_ReadyToFire = gunManager.GetReadyToFire();


        t_Azimuth.text = "Azimuth: " + f_Azimuth;
        t_AzimuthTarget.text = "Target: " + f_AzimuthTarget;
        t_Elevation.text = "Elevation: " + f_Elevation;
        t_ElevationTarget.text = "Target: " + f_ElevationTarget;
        t_AdjustingTarget.text = "Adjusting Target: " + gunManager.b_AdjustingTarget;
        t_Velocity.text = "Velocity: " + f_Velocity + "km/s";

        if (b_Propulsion)
            t_Propulsion.text = "Propulsion Mode: Magnetic" + " - Switching: " + gunManager.GetChangingPropulsion();
        else
            t_Propulsion.text = "Propulsion Mode: Light-gas" + " - Switching: " + gunManager.GetChangingPropulsion();  

        if (b_Armed)
            t_ArmedStatus.text = "Arming Status: " + "Armed";
        else if (b_Arming)
            t_ArmedStatus.text = "Arming Status: " + "Arming...";
        else
            t_ArmedStatus.text = "Arming Status: " + "Not Armed";

        t_ReadyToFire.text = "Ready to fire: " + b_ReadyToFire;

        //-------------------------------------------------------

        t_MousePosition.text = "Mouse Position: " + Mouse.current.position.ReadValue();
        t_MouseTarget.text = inputManager.GetMouseTarget() != null ? "Mouse Target: " + inputManager.GetMouseTarget().name : "Mouse Target: null";

        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
        float msec = deltaTime * 1000.0f;
        float fps = 1.0f / deltaTime;
        t_FPS.text = string.Format("{0:0.0} ms ({1:0.} fps)", msec, fps);
    }

    void GetAzimuth(float f)
    {
        f_AzimuthTarget = f;
    }

    void GetElevation(float f)
    {
        f_ElevationTarget = f;
    }
}
