using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum TriggerEvent
{
    AzimuthMin,
    AzimuthPlus,
    ElevationMin,
    ElevationPlus
}

public class EventManager : MonoBehaviour
{
    public delegate void SliderEvent(float value);
    public delegate void ButtonEvent();

    public static event SliderEvent Azimuth;
    public static event SliderEvent Elevation;

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
}
