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
    public Sprite foreground;
    public Color scoreColor,buttonColor,trajectoryColor,trajectoryDarkColor;
    public Color scoreDarkColor;
    public int price;
}
