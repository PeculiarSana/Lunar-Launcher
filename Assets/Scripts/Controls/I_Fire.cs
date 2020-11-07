using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class I_Fire : MonoBehaviour
{
    public GunManager gunManager;
    public void Interact()
    {
        if (gunManager.GetReadyToFire())
        {
            EventManager.SendFire();
        }
    }
}
