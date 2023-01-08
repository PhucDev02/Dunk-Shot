using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum BallType
{
    Normal,
    Video,
    Mission,
    Secret,
    Challenge,
    Fortune

}
[CreateAssetMenu(fileName = "Ball", menuName = "Ball")]
public class Ball : ScriptableObject
{
    public int id;
    public BallType type;
    public int price;
    public string description;
    public Gradient color,smokeColor;
    public Sprite spriteBall,spriteFlare;
    public int videosCount;
}
