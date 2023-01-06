using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BallTrail : MonoBehaviour
{
    [SerializeField] ParticleSystem smoke, flare,smokeFlare;
    [SerializeField] SpriteRenderer spriteBall;
    void Start()
    {
        spriteBall.sprite = GameManager.Instance.GetBallSelected().spriteBall;

        flare.textureSheetAnimation.SetSprite(0,GameManager.Instance.GetBallSelected().spriteFlare);
        
        ParticleSystem.ColorOverLifetimeModule color = flare.colorOverLifetime;  
        color.color= GameManager.Instance.GetBallSelected().color;
        color = smokeFlare.colorOverLifetime;
        color.color = GameManager.Instance.GetBallSelected().smokeColor;
    }

    void Update()
    {

    }
}
