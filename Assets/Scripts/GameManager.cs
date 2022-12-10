using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum Themes
{
    Classic,
    Christmas,
    Cloudy_Sky,
    Notebook,
    Vikings,
    Circus,
    Neon,
    Jungle,
    Egypt,
    Sakuras,
    Lunch
}
public class GameManager : MonoBehaviour
{

    public Theme[] themes;
    public static GameManager Instance;
    public Themes currentTheme;
    private void Awake()
    {
        Instance = this;
        SetTheme(PlayerPrefs.GetString("Theme"));
    }
    public Theme GetTheme()
    {
        foreach (Theme t in themes)
        {
            if (t.theme == currentTheme)
            {
                return t;
            }
        }
        return themes[0];
    }
    public void SetTheme(string name)
    {
        if (name == "" || name == "Classic")
            currentTheme = Themes.Cloudy_Sky;
        if (name == "Christmas")
            currentTheme = Themes.Christmas;
        if (name == "Cloudy_Sky")
            currentTheme = Themes.Cloudy_Sky;
        if (name == "Notebook")
            currentTheme = Themes.Notebook;
        if (name == "Vikings")
            currentTheme = Themes.Vikings;
        if (name == "Circus")
            currentTheme = Themes.Circus;
        if (name == "Neon")
            currentTheme = Themes.Neon;
        if (name == "Jungle")
            currentTheme = Themes.Jungle;
        if (name == "Egypt")
            currentTheme = Themes.Egypt;
        if (name == "Sakuras")
            currentTheme = Themes.Sakuras;
        if (name == "Lunch")
            currentTheme = Themes.Lunch;
    }

    ///////////////////////////////// inspector
    public static Vector3 powerRingScale = new Vector3(1.3f, 0.9f, 1);
    public static Vector3 initPositionCamera = new Vector3(-1.26f, -1.51f, 0.19f);
}
