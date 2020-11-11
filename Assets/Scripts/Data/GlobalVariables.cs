using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalVariables : ScriptableObject
{
    [Header("Cannon Variables")]
    public float azimuth;
    public float elevation;
    public float armingTime;
    public float translationSpeed;
    public float elevationSpeed;
    [Range(2, 20)]
    public float velocity;
    public bool propulsion;

    [Header("Slider Variables")]
    public float azimuthSliderScale, elevationSliderScale;
}