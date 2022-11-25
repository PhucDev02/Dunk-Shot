using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraController : MonoBehaviour
{
    // Start is called before the first frame update
    public static CameraController Instance;
    [SerializeField] private BallController ball;
    private void Awake()
    {
        Instance = this;
    }

    private float lastPosition,timeGetHighest;
    void Start()
    {
        this.RegisterListener(EventID.OnShoot, (param) => MoveOnShoot());
        this.RegisterListener(EventID.OnContactHoop, (param) => OnContact());
    }

    private void MoveOnShoot()
    {
        timeGetHighest = ball.CalculateTimeGetHighest();
        transform.DOMoveY(Projection.Instance.GetHighestVerticlePoint(), timeGetHighest * 0.9f).SetEase(Ease.InOutQuad).OnComplete(() =>
        {
            transform.DOMoveY(lastPosition,timeGetHighest*0.8f).SetEase(Ease.InOutQuad).SetDelay(timeGetHighest*0.5f);
        });

        //Logger.Log(ball.CalculateBallSpeed().ToString());
        //Logger.Log(ball.CalculateTimeGetHighest().ToString());
    }
    private void OnContact()
    {
        transform.DOKill();
        lastPosition = transform.position.y ;
    }
    // Update is called once per fram
}
