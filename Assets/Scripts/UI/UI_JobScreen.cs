using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_JobScreen : MonoBehaviour
{
    public GameManager _gm;
    public TextMeshProUGUI contents, type, destination, company;
    public GameObject jobScreen, jobScreenButton;

    private void OnEnable()
    {
        EventManager.SendJob += JobInfo;
    }

    private void Update()
    {
        if (_gm.activeJob == null)
        {
            JobInfo(null);
        }
    }

    void JobInfo(JobData job)
    {
        if (job == null)
        {
            contents.text = "No active job.";
            type.text = "";
            destination.text = "";
            company.text = "";
        }
        else
        {
            contents.text = $"Contents: {job.contents}";
            type.text = job.cargoType.ToString();
            destination.text = $"Destination: {job.destination}";
            company.text = $"Source: {job.company}";
        }
    }

    void ToggleJobScreen()
    {
        jobScreen.SetActive(!jobScreen.activeInHierarchy);
        jobScreenButton.SetActive(!jobScreenButton.activeInHierarchy);
    }
}
