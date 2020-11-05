using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class I_TargettingBar : MonoBehaviour
{

    public void Interact()
    {
        transform.parent.GetComponent<Animator>().SetBool("Active", !transform.parent.GetComponent<Animator>().GetBool("Active"));
    }
}
