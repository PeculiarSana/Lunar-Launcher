using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public delegate void Event();
    public delegate void FloatEvent(float value);
    public delegate void BoolEvent(bool value);
    public delegate void JobEvent(JobData job);

    public static event FloatEvent Azimuth;
    public static event FloatEvent Elevation;
    public static event FloatEvent Velocity;
    public static event BoolEvent Propulsion;
    public static event BoolEvent ChangingPropulsion;
    public static event BoolEvent Arming;
    public static event BoolEvent Armed;
    public static event BoolEvent ReadyToFire;
    public static event Event Fire;
    public static event Event RequestJob;

    public static event JobEvent SendJob;
    public static event BoolEvent JobResult;

    public static void SendAzimuth(float f)
    {
        if (Azimuth != null)
            Azimuth(f);
    }

    public static void SendElevation(float f)
    {
        if (Elevation != null)
            Elevation(f);
    }

    public static void SendPropulsion(bool b)
    {
        if (Propulsion != null)
            Propulsion(b);
    }

    public static void SendChangingPropulsion(bool b)
    {
        if (ChangingPropulsion != null)
            ChangingPropulsion(b);
    }

    public static void SendVelocity(float f)
    {
        if (Velocity != null)
            Velocity(f);
    }

    public static void SendArming(bool b)
    {
        if (Arming != null)
            Arming(b);
    }

    public static void SendArmed(bool b)
    {
        if (Armed != null)
            Armed(b);
    }

    public static void SendReadyToFire(bool b)
    {
        if (ReadyToFire != null)
            ReadyToFire(b);
    }

    public static void SendFire()
    {
        if (Fire != null)
            Fire();
    }

    public static void RequestNewJob()
    {
        if (RequestJob != null)
            RequestJob();
    }

    public static void SendNewJob(JobData job)
    {
        if (SendJob != null)
            SendJob(job);
    }

    public static void SendJobResult(bool b)
    {
        if (JobResult != null)
            JobResult(b);
    }    
}
