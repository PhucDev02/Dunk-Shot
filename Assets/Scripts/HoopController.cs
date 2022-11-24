using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class HoopController : MonoBehaviour
{
    // Start is called before the first frame update
    public bool isHoldingBall = false;
    [SerializeField] GameObject net;
    [SerializeField] BallController ball;
    [SerializeField] Transform anchor;

    Vector3 angle, scale;

    void Start()
    {
        angle = Vector3.zero;
        scale = Vector3.one;
        this.RegisterListener(EventID.OnShoot, (param) => Shoot());
        this.RegisterListener(EventID.OnDrag, (param) => Drag());
    }

    private void Shoot()
    {
        net.transform.localScale = Vector3.one;
        if (isHoldingBall == true && DragPanel.force.magnitude > 140)
        {
            isHoldingBall = false;
            ball.transform.position = anchor.position;
            ball.Shoot();
        }
        Projection.Instance.TurnOffTrajectory();
    }
    private void Drag()
    {
        if (isHoldingBall)
        {
            angle.z = DragPanel.GetAngle();
            scale.y = DragPanel.GetScale();
            if (DragPanel.force.magnitude > 10)
            {
                transform.rotation = Quaternion.Euler(angle);
                Projection.Instance.TurnOnTrajectory();
                Projection.Instance.SimulateTrajectory(ball, ball.transform.position);
            }
            net.transform.localScale = scale;

        }
    }
    public void ContactBall()
    {
        if (isHoldingBall == false)
        {
            isHoldingBall = true;
            ball.ContactHoop();
            ball.transform.SetParent(transform);
            transform.DORotate(Vector2.zero, 0.2f).SetEase(Ease.InOutExpo);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (isHoldingBall)
        {
            ball.transform.rotation = Quaternion.identity;
            ball.transform.position = anchor.position;
        }
    }
}
