using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class UnlockPopup : Popup
{
    private Ball ball;
    public override void AssignPopup(Ball ball)
    {
        base.AssignPopup(ball);
        this.ball = ball;
        preview.sprite = ball.spriteBall;
        BallShopController.Instance.UnlockBall(ball.id);
    }
    public void OnClickEquip()
    {
        PlayerPrefs.SetInt("IdBallSelected", ball.id);
        this.PostEvent(EventID.OnChangeBall);
        gameObject.SetActive(false);
    }
    public override void ShowPopup()
    {
        AudioManager.Instance.Play("PopupUnlock");
        gameObject.SetActive(true);
        transform.localScale = Vector3.zero;
        transform.DOScale(1, 0.5f).SetEase(Ease.OutExpo).SetUpdate(true);
    }
}
