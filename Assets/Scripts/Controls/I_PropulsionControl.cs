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
    public bool defaultState, currentState;
    public float onPos, offPos;
    public GameObject flick;
    private void Start()
    {
        StateSwitch(defaultState);
    }

    private void Update()
    {
        if (currentState != defaultState && !Application.isPlaying)
            StateSwitch(defaultState);
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
