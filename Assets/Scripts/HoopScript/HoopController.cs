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
    [SerializeField] NetController netController;
    Vector3 angle, scale;
    public int id;
    void Start()
    {
        angle = Vector3.zero;
        scale = Vector3.one;
        this.RegisterListener(EventID.OnShoot, (param) => Shoot());
        this.RegisterListener(EventID.OnDrag, (param) => Drag());
    }
    private void OnEnable()
    {
        //scale at inspector    
        transform.DOScale(0.36f, 0.5f).SetEase(Ease.OutCubic);
    }
    private void Shoot()
    {
        if (isHoldingBall == true && DragPanel.force.magnitude > 140)
        {
            isHoldingBall = false;
            ball.transform.position = anchor.position;
            netController.EnableSensor();
            ball.Shoot();
            netController.OnLaunch();
        }
        else netController.OnLaunchFailed();
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
            netController.OnContactHoop();

            HoopsPooler.Instance.SetIdLastHoop(id);
            this.PostEvent(EventID.OnContactHoop);
            //
        }
    }
    public void Disappear()
    {
        transform.DOScale(0, 0.5f).SetEase(Ease.OutCubic).OnComplete(() =>
        {
            gameObject.SetActive(false);
        }
        );
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
