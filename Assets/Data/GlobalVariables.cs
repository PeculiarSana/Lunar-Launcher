using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Global Variables", order = 51)]
public class GlobalVariables : ScriptableObject
{
    [Header("Cannon Defaults")]
    public float translationSpeed;
    public float elevationSpeed;
    public float armingTime;
    [Tooltip("Value from -135 to 135")]
    public float azimuth;
    [Tooltip("Value from 0 to 90")]
    public float elevation;
    [Tooltip("Value from 2 to 20")]
    [Range(2.0f, 20.0f)]
    public float velocity;
    [Tooltip("True = Magnetic, False = Light-gas")]
    public bool propulsion;

    [Header("Cannon Runtime Variables")]
    public bool adjustingTarget;
    public bool changingPropulsion;
    public bool readyToFire;
    public bool arming;
    public bool armed;

    [Header("Targetting Sliders")]
    public AzimuthVariables azimuthVariables;
    public ElevationVariables elevationVariables;
}

[Serializable]
public class AzimuthVariables
{
    public float slideScale;
}

[Serializable]
public class ElevationVariables
{
    public float slideScale;
}
