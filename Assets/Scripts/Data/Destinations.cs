using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Data script for Destination variables, i.e. information regarding the coordinates and various settings needed for all destinations

[CreateAssetMenu(fileName = "Destinations", menuName = "Data/Destinations", order = 2)]
public class Destinations : ScriptableObject
{
    public DestinationVariables[] destinations;
}

[Serializable]
public class DestinationVariables
{
    public string name;
    public Vector2 coordinates;
    public float velocityMin = 2, velocityMax = 2;
}
