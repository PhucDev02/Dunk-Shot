using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public enum ObstacleType
{
    Bouncer,
    Shield1,
    Shield2,
    Shield3,
    Shield4,
    Wall1,
    Wall2,
    Wall3,
    Wall4
}
public class Obstacle : MonoBehaviour
{
    [SerializeField] ObstacleType type;
    [SerializeField] SpriteRenderer sprite;
    private Quaternion startRotation;
    private Vector3 startScale;
    private void Awake()
    {
        startRotation = transform.rotation;
        startScale = transform.lossyScale;
        this.RegisterListener(EventID.OnChangeTheme, (param) => ApplyTheme());
    }
    private void Start()
    {
        ApplyTheme();

    }
    public void ApplyTheme()
    {

    }
    public void Reset()
    {
        transform.localScale = startScale;
        transform.rotation = startRotation;
        transform.DOKill();
    }
    public void Appear()
    {
        transform.localScale = Vector3.zero;
        transform.DOScale(1f, 0.3f).SetEase(Ease.OutExpo).SetDelay(0.1f);
    }
    public void Disappear()
    {
        transform.DOKill(); // kill anim rotate
        transform.DOScale(0f, 0.2f).SetEase(Ease.InCubic).OnComplete(() =>
        {
            ObjectPool.Instance.Recall(gameObject);
        });
    }
}