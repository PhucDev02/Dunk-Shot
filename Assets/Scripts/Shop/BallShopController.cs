using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallShopController : MonoBehaviour
{
    // Start is called before the first frame update
    private void Awake()
    {
        PlayerPrefs.SetInt("Ball_0", 1);
        PlayerPrefs.SetInt("IdBallSelected",1);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
