using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class D_DevUI : MonoBehaviour
{
    public GameObject infoMenu;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F4))
        {
            infoMenu.SetActive(!infoMenu.activeInHierarchy);
        }
    }
}
