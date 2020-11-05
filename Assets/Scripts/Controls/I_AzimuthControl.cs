using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// TODO : Clamp control's maximum velocity to smooth it out, lower mouse speed when interacting with it
// TODO : Replace startingPosition with a referenced variable from the Gun itself
//Azimuth is a value between y = -135 and y = 135

public class I_AzimuthControl : MonoBehaviour
{
    [HideInInspector]
    public Vector3 firstSliderPos;
    [HideInInspector]
    public float firstMousePos, lastMousePos;
    public float sliderMin, sliderMax, startingPosition;

    float mouseDistance = 0;

    private void Start()
    {
        transform.localPosition = new Vector3(0, 0.5f, startingPosition / 337.5f);
        EventManager.SendAzimuth(GetTarget());
    }

    public void Interact()
    {
        if (Mouse.current.position.ReadValue().x != lastMousePos)
        {
            mouseDistance = Mouse.current.position.ReadValue().x - lastMousePos;
            transform.localPosition = new Vector3(0, 0.5f, Mathf.Clamp(transform.localPosition.z + (mouseDistance / 500), sliderMin, sliderMax));
        }

        EventManager.SendAzimuth(GetTarget());

        lastMousePos = Mouse.current.position.ReadValue().x;
        mouseDistance = 0;
    }

    public float GetTarget()
    {
        float target = transform.localPosition.z * 337.5f;
        return target;
    }
}
