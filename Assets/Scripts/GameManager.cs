using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("References")]
    public CannonManager cannonManager;

    [Header("Job Variables")]
    public bool hasJob;
    public JobData[] jobs;
    public JobData activeJob;

    void Update()
    {
        if (!hasJob)
        {
            hasJob = true;
            activeJob = jobs[Random.Range(0, jobs.Length - 1)];
            EventManager.SendNewJob(activeJob);
        }
    }
}
