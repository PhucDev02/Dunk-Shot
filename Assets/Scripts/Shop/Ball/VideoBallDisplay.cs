using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;
public class VideoBallDisplay : BallDisplay
{

    [SerializeField] TextMeshProUGUI numberVideo;
    [SerializeField] Image fill;
    private void Start()
    {
        this.RegisterListener(EventID.OnWatchAds, (param) => UpdateVideoCount((Ball)param));
        numberVideo.text = ball.videosCount.ToString();
        fill.fillAmount = 1.0f * PlayerPrefs.GetInt("Ball_" + ball.id + "_VideoWatched") / ball.videosCount;
    }
    void UpdateVideoCount(Ball ball)
    {
        if (ball.id == this.ball.id)
        {
            int tmp = PlayerPrefs.GetInt("Ball_" + ball.id + "_VideoWatched");
            fill.DOFillAmount((tmp * 1.0f / ball.videosCount), 0.5f).SetEase(Ease.InOutSine).SetUpdate(true).OnComplete(() =>
            {
                if (tmp / ball.videosCount == 1)
                {
                    unlock();
                }
            });

        }
    }
    public void OnClick()
    {
        if (BallShopController.Instance.GetUnlockStatus(ball.id) == 0)
        {
            this.PostEvent(EventID.OnShowPopup, ball);

        }
        else //da mo khoa
        {
            if (PlayerPrefs.GetInt("IdBallSelected") == ball.id) //chon dung theme cu
                UI_Customize.Instance.TurnOffCustomize();
            else //chon theme moi
            {
                PlayerPrefs.SetInt("IdBallSelected", ball.id);
                this.PostEvent(EventID.OnChangeBall);
                //do sth with ball
                Logger.Log("chon ball ");
            }
        }
    }
}
