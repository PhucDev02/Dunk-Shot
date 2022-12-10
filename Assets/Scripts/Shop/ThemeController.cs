using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThemeController : MonoBehaviour
{
    public static ThemeController Instance;
    [SerializeField] Transform content;
    [SerializeField] GameObject child;
    private void Awake()
    {
        Instance = this;
        PlayerPrefs.SetInt("Classic", 0);
        PlayerPrefs.SetInt("Cloud_Sky", 1);
        PlayerPrefs.SetInt("Christmas", 1);
    }
    public void InitThemeShop()
    {
        for(int i=0;i<GameManager.Instance.themes.Length;i++)
        {
            var tmp = Instantiate(child, content);
            tmp.GetComponent<ThemeDisplay>().theme = GameManager.Instance.themes[i];
        }
    }
    public void SetLockStatus(Theme theme,int status)
    {
        PlayerPrefs.SetInt(getThemeString(theme), status);
    }
    public int GetLockStatus(Theme theme)
    {
        return PlayerPrefs.GetInt(getThemeString(theme));
    }
    string getThemeString(Theme theme)
    {
        if (theme.theme == Themes.Classic)
            return "Classic";
        if (theme.theme == Themes.Christmas)
            return "Christmas";
        if (theme.theme == Themes.Cloudy_Sky)
            return "Cloud_Sky";
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
}
