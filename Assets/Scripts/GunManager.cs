using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO : Interpolate gun movement for more animation detail
// TODO : Check that the mode is fully switched before allowing firing 
//Manages all parameters of the Gun
//Every relevant event has a Get and Set

public class GunManager : MonoBehaviour
{
    public float translationSpeed, elevationSpeed, armingTime;
    [HideInInspector]
    public bool b_AdjustingTarget;

    public GameObject Translator, Elevator, PayloadPoint;
    public PayloadManager payloadPrefab;

    float f_TargetAzimuth, f_TargetElevation, f_Velocity;
    Vector3 v_LastAzimuth, v_LastElevation;
    bool b_Propulsion, b_ChangingPropulsion, b_Arming, b_Armed, b_ReadyToFire;
    Animator animator;

    private void Start()
    {
        b_ChangingPropulsion = true;
        animator = GetComponent<Animator>();
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
        f_TargetAzimuth = value;
    }
    ///<summary>Returns the current y rotation of the Translator
    ///</summary>
    public float GetAzimuth()
    {
        return Translator.transform.localRotation.eulerAngles.y;
    }

    void SetElevation(float value)
    {
        f_TargetElevation = value;
    }

    ///<summary>Returns the current x rotation of the Elevator, adjusted to fit between 0 and 90
    ///</summary>
    public float GetElevation()
    {
        return -(Elevator.transform.localRotation.eulerAngles.x - 360);
    }

    void Propulsion(bool value)
    {
        gameObject.GetComponent<Animator>().SetBool("ActiveMode", value);
        b_Propulsion = value;
    }

    ///<summary>Returns the currently set Propulsion mode as a boolean, true being 'Magnetic'
    ///</summary>
    public bool GetPropulsion()
    {
        return b_Propulsion;
    }
    void SetVelocity(float value)
    {
        f_Velocity = value;
    }

    ///<summary>Returns the currently selected Velocity, as a float from 2 to 20
    ///</summary>
    public float GetVelocity()
    {
        return f_Velocity;
    }
    void Arming(bool value)
    {
        b_Arming = value;
        if (b_Arming)
            StartCoroutine(Arm(armingTime));
        else
            EventManager.SendArmed(false);
    }

    ///<summary>Returns a bool indicating if the Gun is arming
    ///</summary>
    public bool GetArming()
    {
        return b_Arming;
    }

    IEnumerator Arm(float time)
    {
        yield return new WaitForSeconds(time);
        EventManager.SendArmed(true);
    }

    void Armed(bool value)
    {
        b_Armed = value;
    }

    ///<summary>Returns a bool to indicate if the Gun is armed
    ///</summary>
    public bool GetArmed()
    {
        return b_Armed;
    }

    void ChangingPropulsion(bool b)
    {
        b_ChangingPropulsion = b;
    }

    ///<summary>Returns a bool to indicate if the Gun is changing propulsion mode, tracked by its animation
    ///</summary>
    public bool GetChangingPropulsion()
    {
        return b_ChangingPropulsion;
    }

    void ReadyToFire(bool b)
    {
        b_ReadyToFire = b;
    }

    ///<summary>Returns a bool to indicate if the Gun is ready to fire
    ///</summary>
    public bool GetReadyToFire()
    {
        return b_ReadyToFire;
    }

    void Fire()
    {
        StartCoroutine(FireEffectTimer());
    }

    IEnumerator FireEffectTimer()
    {
        GameObject payload = Instantiate(
            payloadPrefab.gameObject, 
            PayloadPoint.transform.position, 
            Quaternion.Euler(new Vector3(Elevator.transform.rotation.eulerAngles.x + 90, Translator.transform.rotation.eulerAngles.y, 0)), 
            null);
        payload.GetComponent<PayloadManager>().velocity = f_Velocity * 20;
        yield return new WaitForSeconds(1);
    }

    void Update()
    {
        //Elevation and Azimuth run checks to compare their last rotation to their current, to check if the gun is currently moving or not
        //Translation - Azimuth
        Quaternion transRot = Quaternion.Euler(new Vector3(Translator.transform.rotation.eulerAngles.x, f_TargetAzimuth, Translator.transform.rotation.eulerAngles.z));
        Translator.transform.rotation = Quaternion.RotateTowards(Translator.transform.rotation, transRot, translationSpeed * Time.deltaTime);
        if (Translator.transform.rotation.eulerAngles == v_LastAzimuth)
            b_AdjustingTarget = false;
        else
            b_AdjustingTarget = true;
        v_LastAzimuth = Translator.transform.rotation.eulerAngles;

        //Elevation
        Quaternion eleRot = Quaternion.Euler(new Vector3(-f_TargetElevation, Elevator.transform.rotation.eulerAngles.y, Elevator.transform.rotation.eulerAngles.z));
        Elevator.transform.rotation = Quaternion.RotateTowards(Elevator.transform.rotation, eleRot, elevationSpeed * Time.deltaTime);
        if (Elevator.transform.rotation.eulerAngles == v_LastElevation)
            b_AdjustingTarget = false;
        else
            b_AdjustingTarget = true;
        v_LastElevation = Elevator.transform.rotation.eulerAngles;

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
