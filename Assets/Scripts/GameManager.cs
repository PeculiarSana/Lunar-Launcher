using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("References")]
    public CannonManager cannonManager;
    public Destinations destinationsData;

    [Header("Job Variables")]
    public JobData[] jobs;
    public JobData activeJob;

    private void Start()
    {
        if (activeJob == null)
        {
            CreateNewJob();
        }
    }

    public void OnEnable()
    {
        EventManager.RequestJob += CreateNewJob;
    }

    public void CreateNewJob()
    {
        activeJob = jobs[Random.Range(0, jobs.Length)];
        EventManager.SendNewJob(activeJob);
    }
}
