using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// TODO : Clamp control's maximum velocity to smooth it out, lower mouse speed when interacting with it
//Velocity is a value between 2 and 20

public class I_VelocityControl : MonoBehaviour
{
    [HideInInspector]
    public Vector3 firstSliderPos;
    [HideInInspector]
    public float firstMousePos, lastMousePos;
    public float sliderMin, sliderMax, startingPosition, slideMultiplier;

    float mouseDistance = 0;

    private void Start()
    {
        transform.localPosition = new Vector3(0, 0.5f, sliderMin + startingPosition / 22.5f);
        EventManager.SendVelocity(GetTarget());
    }

    public void Interact()
    {
        if (Mouse.current.position.ReadValue().y != lastMousePos)
        {
            mouseDistance = Mouse.current.position.ReadValue().y - lastMousePos;
            transform.localPosition = new Vector3(0, 0.5f, Mathf.Clamp(transform.localPosition.z + (mouseDistance / (100 * slideMultiplier)), sliderMin, sliderMax));
        }

        EventManager.SendVelocity(GetTarget());

        lastMousePos = Mouse.current.position.ReadValue().y;
        mouseDistance = 0;
    }

    public float GetTarget()
    {
        float distance = -(sliderMin - transform.localPosition.z);
        float target = 2 + distance * 22.5f;
        return target;
    }
}
