using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Theme", menuName = "Theme")]
public class Theme : ScriptableObject
{
    public Themes theme;
    public Sprite themeCell;
    public Sprite topHoop, downHoop,topHoopDisable,downHoopDisable;
    public Sprite darkBackground, lightBackground;
    public Sprite foreground,darkForeground;
    public Color scoreColor,buttonColor,trajectoryColor,trajectoryDarkColor;
    public Color scoreDarkColor;
    public int price;
    public float alphaLight, alphaDark;
    //
    [Header("Obstacle")]
    public Sprite shield1;
    public Sprite shield2;
    public Sprite shield3;
    public Sprite shield4;
    public Sprite wall1;
    public Sprite wall2;
    public Sprite wall3;
    public Sprite wall4;
    public Sprite bouncer;
}
