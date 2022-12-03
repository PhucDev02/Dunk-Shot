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
    [SerializeField] SpriteRenderer topHoop, downHoop;
    [SerializeField] GameObject powerRing;
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
        topHoop.sprite = GameManager.Instance.GetTheme().topHoop;
        downHoop.sprite = GameManager.Instance.GetTheme().downHoop;
        transform.DOScale(0.36f, 0.4f).SetEase(Ease.OutBack);
    }
    private void Shoot()
    {
        if (isHoldingBall == true && DragPanel.force.magnitude > 140)
        {
            isHoldingBall = false;
            ball.transform.position = anchor.position;
            netController.EnableSensor();
            ball.Shoot();
        }
        else
            netController.OnLaunchFailed();
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

            if (GameController.Instance.GetScore() != 0 && HoopsPooler.Instance.IsValidShot())
                EffectContact();
        }
    }
    private void EffectContact()
    {
        topHoop.sprite = GameManager.Instance.GetTheme().topHoopDisable;
        downHoop.sprite = GameManager.Instance.GetTheme().downHoopDisable;
        powerRing.transform.localScale = GameManager.powerRingScale;
        powerRing.GetComponent<SpriteRenderer>().DOFade(1, 0);
        powerRing.SetActive(true);
        if (GameController.Instance.isPerfect)
        {
            powerRing.transform.DOScale(powerRing.transform.localScale * 2.6f, 0.5f).SetEase(Ease.OutCubic);
            powerRing.GetComponent<SpriteRenderer>().DOFade(0, 0.5f).OnComplete(() =>
            {
                powerRing.SetActive(false);
            });
        }
        else
        {
            powerRing.transform.DOScale(powerRing.transform.localScale * 2.0f, 0.5f);
            powerRing.GetComponent<SpriteRenderer>().DOFade(0, 0.5f).OnComplete(() =>
            {
                powerRing.SetActive(false);
            });
        }
    }
    public void Disappear()
    {
        transform.DOScale(0, 0.4f).SetEase(Ease.InBack).OnComplete(() =>
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
            ball.transform.SetPositionAndRotation(anchor.position, Quaternion.identity);
        }
    }
    public void ResetRotation()
    {
        transform.DORotate(Vector2.zero, 0.2f).SetEase(Ease.InOutExpo);

    }
}
