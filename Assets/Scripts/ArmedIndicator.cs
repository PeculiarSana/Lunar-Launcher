using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmedIndicator : MonoBehaviour
{
    public Light bulb;
    public Material on, off;
    public GunManager gunManager;

    private void Start()
    {
        Armed(gunManager.GetArmed());
    }
    private void OnEnable()
    {
        EventManager.Armed += Armed;
    }

    void Armed(bool b)
    {
        bulb.gameObject.SetActive(b);
        if (b)
            gameObject.GetComponent<MeshRenderer>().material = on;
        else
            gameObject.GetComponent<MeshRenderer>().material = off;
    }
}
