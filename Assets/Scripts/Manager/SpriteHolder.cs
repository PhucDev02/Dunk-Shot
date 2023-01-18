using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteHolder : MonoBehaviour
{
    // Start is called before the first frame update
    public static SpriteHolder Instance;
    private void Awake()
    {
        Instance = this;
    }
    [SerializeField] public Sprite orangeTag,purpleTag;
}
