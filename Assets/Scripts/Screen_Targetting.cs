using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Screen_Targetting : MonoBehaviour
{
    public Text t_AzimuthTarget, t_AzimuthCurrent, t_ElevationTarget, t_ElevationCurrent;
    public Image i_AzimuthBar, i_ElevationBar;
    public CannonManager gunManager;

    float f_AzimuthTarget, f_ElevationTarget;

    private void OnEnable()
    {
        EventManager.Azimuth += GetAzimuth;
        EventManager.Elevation += GetElevation;
    }

    void Update()
    {
        t_AzimuthTarget.text = "-> " + Mathf.Round(f_AzimuthTarget).ToString();
        t_AzimuthCurrent.text = gunManager.GetAzimuth() <= 180 ? Mathf.Round(gunManager.GetAzimuth()).ToString() : (Mathf.Round(gunManager.GetAzimuth()) - 360).ToString();

        t_ElevationTarget.text = "-> " + Mathf.Round(f_ElevationTarget).ToString();
        t_ElevationCurrent.text = Mathf.Round(gunManager.GetElevation()).ToString();

        i_AzimuthBar.rectTransform.anchoredPosition = new Vector2(gunManager.GetAzimuth() <= 180 ? -gunManager.GetAzimuth() * 10 - 1350 : -1350 + (-gunManager.GetAzimuth() + 360) * 10, i_AzimuthBar.rectTransform.anchoredPosition.y);
        i_ElevationBar.rectTransform.anchoredPosition = new Vector2(i_ElevationBar.rectTransform.anchoredPosition.x, -gunManager.GetElevation() * 10);
    }

    void GetAzimuth(float f)
    {
        f_AzimuthTarget = f;
    }

    void GetElevation(float f)
    {
        f_ElevationTarget = f;
    }
}
