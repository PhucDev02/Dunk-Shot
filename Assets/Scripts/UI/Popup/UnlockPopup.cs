using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockPopup : Popup
{
    private Ball ball;
    public override void AssignPopup(Ball ball)
    {
        base.AssignPopup(ball);
        this.ball = ball;
        preview.sprite = ball.spriteBall;
    }
    public void OnClickEquip()
    {
        PlayerPrefs.SetInt("IdBallSelected", ball.id);
        this.PostEvent(EventID.OnChangeBall);
        gameObject.SetActive(false);
    }
}
