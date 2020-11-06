using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadyToFireIndicator : MonoBehaviour
{
    public Light bulb;
    public Material on, off;
    public GunManager gunManager;

    private void Start()
    {
        ReadyToFire(gunManager.GetReadyToFire());
    }
    private void OnEnable()
    {
        EventManager.ReadyToFire += ReadyToFire;
    }

    void ReadyToFire(bool b)
    {
        bulb.gameObject.SetActive(b);
        if (b)
            gameObject.GetComponent<MeshRenderer>().material = on;
        else
            gameObject.GetComponent<MeshRenderer>().material = off;
    }
}
