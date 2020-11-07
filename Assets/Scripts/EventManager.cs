using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public delegate void SliderEvent(float value);
    public delegate void ButtonEvent();
    public delegate void ToggleEvent(bool value);

    public static event SliderEvent Azimuth;
    public static event SliderEvent Elevation;
    public static event SliderEvent Velocity;
    public static event ToggleEvent Propulsion;
    public static event ToggleEvent ChangingPropulsion;
    public static event ToggleEvent Arming;
    public static event ToggleEvent Armed;
    public static event ToggleEvent ReadyToFire;
    public static event ButtonEvent Fire;

    public static void SendAzimuth(float f)
    {
        if (Azimuth != null)
            Azimuth(f);
    }

    public static void SendElevation(float f)
    {
        if (Elevation != null)
            Elevation(f);
    }

    public static void SendPropulsion(bool b)
    {
        if (Propulsion != null)
            Propulsion(b);
    }

    public static void SendChangingPropulsion(bool b)
    {
        if (ChangingPropulsion != null)
            ChangingPropulsion(b);
    }

    public static void SendVelocity(float f)
    {
        if (Velocity != null)
            Velocity(f);
    }

    public static void SendArming(bool b)
    {
        if (Arming != null)
            Arming(b);
    }

    public static void SendArmed(bool b)
    {
        if (Armed != null)
            Armed(b);
    }

    public static void SendReadyToFire(bool b)
    {
        if (ReadyToFire != null)
            ReadyToFire(b);
    }

    public static void SendFire()
    {
        if (Fire != null)
            Fire();
    }
}
