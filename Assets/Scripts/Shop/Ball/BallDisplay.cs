using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;
public class BallDisplay : MonoBehaviour
{
    public Ball ball;
    [SerializeField] protected GameObject buffer, lockStatus;
    [SerializeField] protected Image ballPreview;
    private void Awake()
    {
        this.RegisterListener(EventID.OnChangeBall, (param) => UpdateSelectStatus());
    }
    private void Start()
    {
    }
    public void Init(Ball ball)
    {
        this.ball = ball;
        ballPreview.sprite = ball.spriteBall;
        if (BallShopController.Instance.GetUnlockStatus(ball.id) == 0)
        {
            lockStatus.SetActive(true);
        }
        else
        {
            lockStatus.SetActive(false);
        }
        if (PlayerPrefs.GetInt("IdBallSelected") == ball.id)
        {
            buffer.SetActive(true);
        }
        Logger.Log("a");
    }
    public void UpdateSelectStatus()
    {
        if (PlayerPrefs.GetInt("Ball_" + ball.id) == 1)
            if (PlayerPrefs.GetInt("IdBallSelected") == ball.id)
            {
                buffer.SetActive(true);
            }
            else buffer.SetActive(false);
    }
}
