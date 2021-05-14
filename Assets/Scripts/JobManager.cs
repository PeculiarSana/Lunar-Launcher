using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script to check the job conditions and report if the player has either succeeded or failed in fulfilling them
public class JobManager : MonoBehaviour
{
    public GameManager _gm;
    public Destinations destinationsData;

    JobData activeJob;

    private void Start()
    {
        _gm = GetComponent<GameManager>();
    }
    private void OnEnable()
    {
        EventManager.Fire += JobCheck;
    }

    void JobCheck()
    {
        bool correctPropulsion = false;
        bool correctCoords = false;
        bool correctVelocity = false;
        activeJob = _gm.activeJob;
        //Type/Propulsion Check
        switch (activeJob.cargoType)
        {
            case CargoType.Basic:
                correctPropulsion = true;
                break;
            case CargoType.Electric:
                if (!_gm.cannonManager.GetPropulsion())
                    correctPropulsion = true;
                break;
            case CargoType.Fragile:
                if (_gm.cannonManager.GetPropulsion())
                    correctPropulsion = true;
                break;
            case CargoType.Weapon:
                if (_gm.cannonManager.GetPropulsion())
                    correctPropulsion = true;
                break;
        }

        
        foreach(DestinationVariables v in destinationsData.destinations)
        {
            if (v.name == activeJob.destination.ToString())
            {
                //Destination
                if (_gm.cannonManager.GetElevation() == v.coordinates.x && _gm.cannonManager.GetAzimuth() == v.coordinates.y)
                    correctCoords = true;
                //Velocity
                if (_gm.cannonManager.GetVelocity() >= v.velocityMin && _gm.cannonManager.GetVelocity() <= v.velocityMax)
                    correctVelocity = true;
            }
        }
        

        Debug.Log($"correctPropulsion: {correctPropulsion}, correctCoords: {correctCoords}, correctVelocity: {correctVelocity}", gameObject);

    }
}
