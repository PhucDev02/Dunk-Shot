using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
public class VideoPopup : Popup
{
    [SerializeField] Image fill;
    [SerializeField] TextMeshProUGUI progress;
    private Ball ball;
    public override void AssignPopup(Ball ball)
    {
        //fill
        this.ball = ball;
        fill.fillAmount = 1.0f * PlayerPrefs.GetInt("Ball_" + ball.id + "_VideoWatched") / ball.videosCount;
        progress.text = PlayerPrefs.GetInt("Ball_" + ball.id + "_VideoWatched") + "/" + ball.videosCount;
    }
    public void Watch()
    {
        int tmp = PlayerPrefs.GetInt("Ball_" + ball.id + "_VideoWatched");
        PlayerPrefs.SetInt("Ball_" + ball.id + "_VideoWatched", tmp + 1);
        tmp += 1;
        this.PostEvent(EventID.OnWatchAds, ball);
        progress.text = PlayerPrefs.GetInt("Ball_" + ball.id + "_VideoWatched") + "/" + ball.videosCount;
        fill.DOFillAmount((tmp * 1.0f / ball.videosCount), 0.5f).SetEase(Ease.InOutSine).SetUpdate(true).OnComplete(() =>
        {
            if (tmp / ball.videosCount == 1)
            {
                HidePopup();
            }
        });
    }
}
