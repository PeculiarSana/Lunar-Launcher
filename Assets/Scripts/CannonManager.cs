using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO : Interpolate gun movement for more animation detail
// TODO : Check that the mode is fully switched before allowing firing 
//Manages all parameters of the Gun
//Every relevant event has a Get and Set

public class CannonManager : MonoBehaviour
{
    public GlobalVariables _globalVariables;
    [HideInInspector]
    public bool b_AdjustingTarget;

    public Transform Translator, Elevator, PayloadPoint;
    public PayloadManager payloadPrefab;

    float f_TargetAzimuth, f_TargetElevation, f_Velocity;
    Vector3 v_LastAzimuth, v_LastElevation;
    bool b_Propulsion, b_ChangingPropulsion, b_Arming, b_Armed, b_ReadyToFire;
    Animator animator;

    private void Start()
    {
        b_ChangingPropulsion = true;
        animator = GetComponent<Animator>();

        //Start at default positions
        Translator.rotation = Quaternion.Euler(0, _globalVariables.azimuth, 0);
        Elevator.localRotation = Quaternion.Euler(-_globalVariables.elevation, 0, 0);
    }

    //Register to events coming from interactable objects
    private void OnEnable()
    {
        EventManager.Azimuth += SetAzimuth;
        EventManager.Elevation += SetElevation;
        EventManager.Velocity += SetVelocity;
        EventManager.Propulsion += Propulsion;
        EventManager.ChangingPropulsion += ChangingPropulsion;
        EventManager.Arming += Arming;
        EventManager.Armed += Armed;
        EventManager.ReadyToFire += ReadyToFire;
        EventManager.Fire += Fire;
    }

    void SetAzimuth(float value)
    {
        f_TargetAzimuth = Mathf.Round(value);
    }
    ///<summary>Returns the current y rotation of the Translator
    ///</summary>
    public float GetAzimuth()
    {
        return Translator.localRotation.eulerAngles.y > 180 ? Translator.localRotation.eulerAngles.y - 360 : Translator.localRotation.eulerAngles.y;
    }

    void SetElevation(float value)
    {
        f_TargetElevation = Mathf.Round(value);
    }

    ///<summary>Returns the current x rotation of the Elevator, adjusted to fit between 0 and 90
    ///</summary>
    public float GetElevation() => -(Elevator.localRotation.eulerAngles.x - 360);

    void Propulsion(bool value)
    {
        gameObject.GetComponent<Animator>().SetBool("ActiveMode", value);
        b_Propulsion = value;
    }

    ///<summary>Returns the currently set Propulsion mode as a boolean, true being 'Magnetic'
    ///</summary>
    public bool GetPropulsion() => b_Propulsion;

    void SetVelocity(float value) => f_Velocity = value;

    ///<summary>Returns the currently selected Velocity, as a float from 2 to 20
    ///</summary>
    public float GetVelocity() => f_Velocity;

    void Arming(bool value)
    {
        b_Arming = value;
        if (b_Arming)
            StartCoroutine(Arm(_globalVariables.armingTime));
        else
            EventManager.SendArmed(false);
    }

    ///<summary>Returns a bool indicating if the Gun is arming
    ///</summary>
    public bool GetArming() =>  b_Arming;

    IEnumerator Arm(float time)
    {
        yield return new WaitForSeconds(time);
        EventManager.SendArmed(true);
    }

    void Armed(bool value) => b_Armed = value;

    ///<summary>Returns a bool to indicate if the Gun is armed
    ///</summary>
    public bool GetArmed() => b_Armed;

    void ChangingPropulsion(bool b) => b_ChangingPropulsion = b;

    ///<summary>Returns a bool to indicate if the Gun is changing propulsion mode, tracked by its animation
    ///</summary>
    public bool GetChangingPropulsion() => b_ChangingPropulsion;

    void ReadyToFire(bool b) =>  b_ReadyToFire = b;

    ///<summary>Returns a bool to indicate if the Gun is ready to fire
    ///</summary>
    public bool GetReadyToFire() => b_ReadyToFire;

    void Fire() => StartCoroutine(FireEffectTimer());

    IEnumerator FireEffectTimer()
    {
        GameObject payload = Instantiate(
            payloadPrefab.gameObject, 
            PayloadPoint.position, 
            Quaternion.Euler(new Vector3(Elevator.rotation.eulerAngles.x + 90, Translator.rotation.eulerAngles.y, 0)), 
            null);
        payload.GetComponent<PayloadManager>().velocity = f_Velocity * 20;
        yield return new WaitForSeconds(1);
    }

    void Update()
    {
        //Elevation and Azimuth run checks to compare their last rotation to their current, to check if the gun is currently moving or not
        //Translation - Azimuth
        Quaternion transRot = Quaternion.Euler(new Vector3(0, f_TargetAzimuth, 0));
        Translator.rotation = Quaternion.RotateTowards(Translator.rotation, transRot, _globalVariables.translationSpeed * Time.deltaTime);

        if (Translator.rotation.eulerAngles == v_LastAzimuth)
            b_AdjustingTarget = false;
        else
            b_AdjustingTarget = true;
        v_LastAzimuth = Translator.rotation.eulerAngles;

        //Elevation
        float target = Mathf.Clamp(f_TargetElevation, 0.01f, 90.0f);
        Quaternion eleRot = Quaternion.Euler(new Vector3(-target, Elevator.rotation.eulerAngles.y, Elevator.rotation.eulerAngles.z));
        Elevator.rotation = Quaternion.RotateTowards(Elevator.rotation, eleRot, _globalVariables.elevationSpeed * Time.deltaTime);

        if (Elevator.rotation.eulerAngles == v_LastElevation)
            b_AdjustingTarget = false;
        else
            b_AdjustingTarget = true;
        v_LastElevation = Elevator.rotation.eulerAngles;

        //-----------

        //Checks to see if the propulsion animation is running to know when the gun is done switching
        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
        {
            b_ChangingPropulsion = false;
        }

        //Checks to run to see if the Gun is ready to fire
        if (b_Armed && !b_ChangingPropulsion && !b_AdjustingTarget)
            EventManager.SendReadyToFire(true);
        else
            EventManager.SendReadyToFire(false);
    }
}
