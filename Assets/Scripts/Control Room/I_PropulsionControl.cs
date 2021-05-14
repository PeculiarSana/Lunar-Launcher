using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// TODO : Smooth flick animation
//Propulsion is simply a true or false value

//Runs in Edit Mode to allow the default state to be changed
[ExecuteInEditMode]
public class I_PropulsionControl : MonoBehaviour
{
    public GlobalVariables _globalVariables;
    public bool currentState;
    public float onPos, offPos;
    public GameObject flick;
    public GameObject[] indicators;
    public Material[] indicatorMats;
    public CannonManager gunManager;
    private void Start()
    {
        StateSwitch(_globalVariables.propulsion);
    }

    private void Update()
    {
        if (currentState != _globalVariables.propulsion && !Application.isPlaying)
            StateSwitch(_globalVariables.propulsion);

        if (currentState && !gunManager.GetChangingPropulsion())
        {
            indicators[0].GetComponent<MeshRenderer>().material = indicatorMats[0];
            indicators[1].GetComponent<MeshRenderer>().material = indicatorMats[1];
            indicators[2].GetComponent<MeshRenderer>().material = indicatorMats[1];
        }
        else if (!currentState && !gunManager.GetChangingPropulsion())
        {
            indicators[0].GetComponent<MeshRenderer>().material = indicatorMats[1];
            indicators[1].GetComponent<MeshRenderer>().material = indicatorMats[0];
            indicators[2].GetComponent<MeshRenderer>().material = indicatorMats[1];
        }
        else
        {
            indicators[0].GetComponent<MeshRenderer>().material = indicatorMats[1];
            indicators[1].GetComponent<MeshRenderer>().material = indicatorMats[1];
            indicators[2].GetComponent<MeshRenderer>().material = indicatorMats[2];
        }
    }
    public void Interact()
    {
        StateSwitch(!currentState);
    }

    void StateSwitch(bool b)
    {
        float pos;
        if (b)
            pos = onPos;
        else
            pos = offPos;
        flick.transform.localRotation = Quaternion.Euler(new Vector3(pos, 0, 0));
        currentState = b;
        if (Application.isPlaying)
            EventManager.SendPropulsion(b);  
    }
}
