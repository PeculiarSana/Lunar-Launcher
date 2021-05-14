using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class I_Fire : MonoBehaviour
{
    public CannonManager gunManager;
    public void Interact()
    {
        if (gunManager.GetReadyToFire())
        {
            EventManager.SendFire();
        }
    }
}
