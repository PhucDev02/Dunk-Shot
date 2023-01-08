using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BallTrail : MonoBehaviour
{
    [SerializeField] ParticleSystem smoke, flare, smokeFlare;
    [SerializeField] SpriteRenderer spriteBall;

    private void Awake()
    {
        this.RegisterListener(EventID.OnChangeBall, (param) => ApplySkin());
        this.RegisterListener(EventID.OnPerfectx2, (param) => Smoke());
        this.RegisterListener(EventID.OnPerfectx3, (param) => Flare());
        this.RegisterListener(EventID.OnBounceSide, (param) => DeactiveEffect());
        this.RegisterListener(EventID.OnGameOver, (param) => DeactiveEffect());
        this.RegisterListener(EventID.OnSecondChange, (param) => DeactiveEffect());

    }
    void Start()
    {
        ApplySkin();
    }
    private void ApplySkin()
    {
        try
        {
            flare.textureSheetAnimation.SetSprite(0, GameManager.Instance.GetBallSelected().spriteFlare);

            ParticleSystem.ColorOverLifetimeModule color = flare.colorOverLifetime;
            color.color = GameManager.Instance.GetBallSelected().color;
            color = smokeFlare.colorOverLifetime;
            color.color = GameManager.Instance.GetBallSelected().smokeColor;
            spriteBall.sprite = GameManager.Instance.GetBallSelected().spriteBall;
        }
        catch { }
    }
    private void Smoke()
    {
        try
        {
            smoke.Play();
        }
        catch
        {
        }
    }
    private void Flare()
    {
        try
        {
            smoke.Stop();
            flare.Play();
            smokeFlare.Play();
        }
        catch
        {

        }
    }
    public void DeactiveEffect()
    {
        try
        {
            smoke.Stop();
            flare.Stop();
            smokeFlare.Stop();
        }
        catch
        {
        }
    }

}

