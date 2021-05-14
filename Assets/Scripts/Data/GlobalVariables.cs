using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Global Variables", menuName = "Data/GlobalVariables", order = 1)]
public class GlobalVariables : ScriptableObject
{
    [Header("Cannon Variables")]
    public float azimuth;
    public float elevation;
    public float armingTime;
    [Tooltip("Default 4")]
    public float translationSpeed;
    [Tooltip("Default 2.8")]
    public float elevationSpeed;
    [Range(2, 20)]
    public float velocity;
    public bool propulsion;

    [Header("Slider Variables")]
    public float azimuthSliderScale, elevationSliderScale;
}