using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

//Script to check the job conditions and report if the player has either succeeded or failed in fulfilling them
public class JobManager : MonoBehaviour
{
    public GameManager _gm;
    public Destinations destinationsData;
    public TextMeshProUGUI jobResult;
    public float f_JobEndDelay = 2, f_JobResultFade = 3;

    JobData activeJob = null;
    JobData lastJob = null;
    TextUtilities textUtilities;
    bool b_JobEnding;

    private void Start()
    {
        _gm = GetComponent<GameManager>();
        jobResult.color = new Color(jobResult.color.r, jobResult.color.g, jobResult.color.b, 0);
        textUtilities = new TextUtilities();
    }
    private void OnEnable()
    {
        EventManager.Fire += JobCheck;
    }

    private void Update()
    {
        if (lastJob != activeJob && activeJob != null && !b_JobEnding)
        {
            StartCoroutine(NewJobText());
        }
        lastJob = activeJob;
        activeJob = _gm.activeJob;
    }

    void JobCheck()
    {
        bool correctPropulsion = false;
        bool correctCoords = false;
        bool correctVelocity = false;
        //Type/Propulsion Check
        switch (activeJob.cargoType)
        {
            case CargoType.Basic:
                correctPropulsion = true;
                break;
            case CargoType.Electric:
                if (!_gm.cannonManager.GetPropulsion())
                    correctPropulsion = true;
                break;
            case CargoType.Fragile:
                if (_gm.cannonManager.GetPropulsion())
                    correctPropulsion = true;
                break;
            case CargoType.Weapon:
                if (_gm.cannonManager.GetPropulsion())
                    correctPropulsion = true;
                break;
        }

        
        foreach(DestinationVariables v in destinationsData.destinations)
        {
            if (v.name == activeJob.destination.ToString())
            {
                //Destination
                if (_gm.cannonManager.GetElevation() == v.coordinates.x && _gm.cannonManager.GetAzimuth() == v.coordinates.y)
                    correctCoords = true;
                //Velocity
                if (_gm.cannonManager.GetVelocity() >= v.velocityMin && _gm.cannonManager.GetVelocity() <= v.velocityMax)
                    correctVelocity = true;
            }
        }

        StartCoroutine(JobEndText(f_JobEndDelay, correctPropulsion, correctCoords, correctVelocity));

        Debug.Log($"correctPropulsion: {correctPropulsion}, correctCoords: {correctCoords}, correctVelocity: {correctVelocity}", gameObject);
    }

    private IEnumerator NewJobText()
    {
        jobResult.text = "New job received.";
        jobResult.color = new Color(1, 1, 1, 0);
        yield return StartCoroutine(textUtilities.FadeInText(1, jobResult));
        yield return new WaitForSeconds(f_JobResultFade);
        yield return StartCoroutine(textUtilities.FadeOutText(1, jobResult));
    }

    private IEnumerator JobEndText(float time, bool prop, bool coord, bool velo)
    {
        b_JobEnding = true;
        yield return new WaitForSeconds(time);

        if (prop && coord && velo)
        {
            jobResult.text = "Job Succeeded!";
            jobResult.color = new Color(0, 1, 0, 0);
        }
        else
        {
            jobResult.text = "Job Failed!";
            jobResult.color = new Color(1, 0, 0, 0);
        }
        _gm.activeJob = null;
        yield return StartCoroutine(textUtilities.FadeInText(1, jobResult));
        yield return new WaitForSeconds(f_JobResultFade);
        yield return StartCoroutine(textUtilities.FadeOutText(1, jobResult));
        b_JobEnding = false;
        yield return new WaitForSeconds(4);
        EventManager.RequestNewJob();
    }
}
