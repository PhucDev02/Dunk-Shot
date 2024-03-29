using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum Themes
{
    Classic,//
    Christmas,//
    Cloudy_Sky,//
    Notebook,//
    Vikings,//
    Circus,//
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
    ///
    private void Awake()
    {
        Instance = this;
        SetTheme(PlayerPrefs.GetString("Theme"));
        if (PlayerPrefs.GetInt("Tokens") == 0)
            SetTokenValue(999);
        else
            SetTokenValue(PlayerPrefs.GetInt("Tokens"));
        if (PlayerPrefs.GetInt("Stars") == 0)
            SetStarValue(400);
        else
            SetStarValue(PlayerPrefs.GetInt("Stars"));
        for (int i = 0; i < balls.Length; i++)
            balls[i].id = i;

        ThemeController.Instance.InitThemeShop();
        BallShopController.Instance.InitBallShop();


    }
    #region theme execute
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
    public void SetTheme(Theme theme)
    {
        PlayerPrefs.SetString("Theme", Utility.GetThemeString(theme));
        SetTheme(PlayerPrefs.GetString("Theme"));
    }
    public void SetTheme(string name)
    {
        PlayerPrefs.SetString("Theme", name);
        if (name == "" || name == "Classic")
        {
            currentTheme = Themes.Classic;
            PlayerPrefs.SetString("Theme", "Classic");
        }
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
    #endregion

    private void SetTokenValue(int value)
    {
        PlayerPrefs.SetInt("Tokens", value);
    }
    private void SetStarValue(int value)
    {
        PlayerPrefs.SetInt("Stars", value);
    }
    //////////////// ball 
    public Ball[] balls;
    public Ball GetBallSelected()
    {
        return balls[PlayerPrefs.GetInt("IdBallSelected")];
    }
    ///////////////////////////////// inspector
    public static Vector3 powerRingScale = new Vector3(1.3f, 0.9f, 1);
    public static Vector3 initPositionCamera = new Vector3(-1.26f, -1.51f, 0.19f);
    public static Vector3 initPosFirstHoop = new Vector3(-1.26f, -0.81f, 0.19f);
    public static Vector3 initPosSecondHoop = new Vector3(1.185f, 0.694f, 0.188f);
    public static Vector3 initPosBall = new Vector3(-1.26f, 0.255f, 0);
    public Sprite net,goldenNet;
}
