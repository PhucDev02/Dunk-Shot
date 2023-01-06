using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Ball", menuName = "Ball")]
public class Ball : ScriptableObject
{
    public int price;
    public string description;
    public Gradient color,smokeColor;
    public Sprite spriteBall,spriteFlare;
}
