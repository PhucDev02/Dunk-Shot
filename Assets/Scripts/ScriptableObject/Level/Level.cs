using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : ScriptableObject
{
    public GameObject prefab;
    public ChallengeType type;
    public string id;
    public int totalHoops;
}
