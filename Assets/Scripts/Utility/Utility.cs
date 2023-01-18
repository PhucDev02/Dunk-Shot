using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utility
{
    public static string GetThemeString(Theme theme)
    {
        if (theme.theme == Themes.Classic)
            return "Classic";
        if (theme.theme == Themes.Christmas)
            return "Christmas";
        if (theme.theme == Themes.Cloudy_Sky)
            return "Cloudy_Sky";
        if (theme.theme == Themes.Notebook)
            return "Notebook";
        if (theme.theme == Themes.Vikings)
            return "Vikings";
        if (theme.theme == Themes.Circus)
            return "Circus";
        if (theme.theme == Themes.Neon)
            return "Neon";
        if (theme.theme == Themes.Jungle)
            return "Jungle";
        if (theme.theme == Themes.Egypt)
            return "Egypt";
        if (theme.theme == Themes.Sakuras)
            return "Sakuras";
        if (theme.theme == Themes.Lunch)
            return "Lunch";
        return "Classic";
    }
    public static void PlayShootSound()
    {
        if (GameController.Instance.streak >= 3)
        {
            if (DragPanel.force.magnitude <= DragPanel.maxMagnitude *3/ 5)
                AudioManager.Instance.Play("PowerLowShoot");
            else if (DragPanel.force.magnitude < DragPanel.maxMagnitude * 4/5)
                AudioManager.Instance.Play("PowerMidShoot");
            else
                AudioManager.Instance.Play("PowerHighShoot");
        }
        else
        {
            if (DragPanel.force.magnitude <= DragPanel.maxMagnitude *3/5)
                AudioManager.Instance.Play("LowShoot");
            else if (DragPanel.force.magnitude < DragPanel.maxMagnitude * 4/5)
                AudioManager.Instance.Play("MidShoot");
            else
                AudioManager.Instance.Play("HighShoot");
        }
    }
}
