using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

// TODO: Set up input for other devices

public class InputManager : MonoBehaviour
{
    GameObject mouseAbove, target;

    int rayMask = 1 << 10;

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, rayMask))
        {
            mouseAbove = hit.collider.gameObject;
            if (Input.GetMouseButtonDown(0))
            {
                target = hit.collider.gameObject;
                switch (target.gameObject.tag)
                {
                    case "TargettingBar":
                        target.GetComponent<I_TargettingBar>().Interact();
                        break;
                    case "PropulsionControl":
                        target.GetComponent<I_PropulsionControl>().Interact();
                        EventManager.SendChangingPropulsion(true);
                        break;
                    case "ArmingControl":
                        target.GetComponent<I_ArmingControl>().Interact();
                        break;
                    case "FireButton":
                        target.GetComponent<I_Fire>().Interact();
                        break;
                }
            }

        }
        else
            mouseAbove = null;
        if (target != null && Input.GetMouseButtonUp(0))
        {
            target = null;
        }

        //Click and hold
        if (target != null)
            //Check the tag of each interactable here to run appropriate code
            switch (target.gameObject.tag)
            {
                case "AzimuthControl":
                    I_AzimuthControl azi = target.GetComponent<I_AzimuthControl>();
                    if (Input.GetMouseButtonDown(0))
                    {
                        azi.firstMousePos = Mouse.current.position.ReadValue().x;
                        azi.lastMousePos = Mouse.current.position.ReadValue().x;
                    }
                    azi.Interact();
                    break;
                case "ElevationControl":
                    I_ElevationControl ele = target.GetComponent<I_ElevationControl>();
                    if (Input.GetMouseButtonDown(0))
                    {
                        ele.firstMousePos = Mouse.current.position.ReadValue().y;
                        ele.lastMousePos = Mouse.current.position.ReadValue().y;
                    }
                    ele.Interact();
                    break;
                case "VelocityControl":
                    I_VelocityControl vel = target.GetComponent<I_VelocityControl>();
                    if (Input.GetMouseButtonDown(0))
                    {
                        vel.firstMousePos = Mouse.current.position.ReadValue().y;
                        vel.lastMousePos = Mouse.current.position.ReadValue().y;
                    }
                    vel.Interact();
                    break;
            }
    }

    public GameObject GetMouseTarget()
    {
        if (mouseAbove != null)
            return mouseAbove;
        else
            return null;
    }
}