using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// TODO : Clamp control's maximum velocity to smooth it out, lower mouse speed when interacting with it
//Elevation is a value between x = 0 and x = 90

public class I_ElevationControl : MonoBehaviour
{
    [HideInInspector]
    public Vector3 firstSliderPos;
    [HideInInspector]
    public float firstMousePos, lastMousePos;
    public float sliderMin, sliderMax, startingPosition;

    float mouseDistance = 0;

    private void Start()
    {
        transform.localPosition = new Vector3(0, 0.5f, sliderMin + startingPosition / 112.5f);
        EventManager.SendElevation(GetTarget());
    }

    public void Interact()
    {
        if (Mouse.current.position.ReadValue().y != lastMousePos)
        {
            mouseDistance = Mouse.current.position.ReadValue().y - lastMousePos;
            transform.localPosition = new Vector3(0, 0.5f, Mathf.Clamp(transform.localPosition.z + (mouseDistance / 500), sliderMin, sliderMax));
        }

        EventManager.SendElevation(GetTarget());

        lastMousePos = Mouse.current.position.ReadValue().y;
        mouseDistance = 0;
    }

    public float GetTarget()
    {
        float distance = -(sliderMin - transform.localPosition.z);
        float target = distance * 112.5f;
        return target;
    }
}
