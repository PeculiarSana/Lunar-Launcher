using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CargoType
{
    Basic,
    Fragile,
    Electric,
    Weapon
}
public enum Destination
{
    Mars,
    Mercury,
    Venus
}

public class JobData : ScriptableObject
{
    public string company;
    public string contents;
    public CargoType cargoType;
    public Destination destination;

}
