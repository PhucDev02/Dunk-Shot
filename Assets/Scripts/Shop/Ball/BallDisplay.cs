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
            ballPreview.transform.DOScale(0.94f, 0.45f).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo).SetUpdate(true);
            buffer.SetActive(true);
        }
    }
    public void UpdateSelectStatus()
    {
        if (PlayerPrefs.GetInt("Ball_" + ball.id) == 1)
            if (PlayerPrefs.GetInt("IdBallSelected") == ball.id)
            {
                buffer.SetActive(true);
                buffer.GetComponent<Image>().transform.localScale = Vector3.zero;
                buffer.GetComponent<Image>().transform.DOScale(1, 0.3f).SetEase(Ease.OutExpo).SetUpdate(true);
                ballPreview.transform.DOScale(0.94f, 0.45f).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo).SetUpdate(true).SetDelay(0.11f);
            }
            else
            {
                buffer.SetActive(false);
                ballPreview.transform.localScale = Vector3.one;
                ballPreview.transform.DOScale(1, 0.3f).SetEase(Ease.OutExpo).SetUpdate(true).OnComplete(()=> {
                    ballPreview.transform.DOKill();
                });

            }
    }
    protected void unlock()
    {
        Logger.Log("unlock");
        this.PostEvent(EventID.OnPurchaseItem);
        PlayerPrefs.SetInt("Ball_" + ball.id, 1);
        ballPreview.transform.localScale = Vector3.one * 0.8f;
        ballPreview.transform.DOScale(1, 0.5f).SetEase(Ease.OutExpo).SetUpdate(true);
        lockStatus.SetActive(false);
        //PlayerPrefs.SetInt("IdBallSelected", ball.id);
        //this.PostEvent(EventID.OnChangeBall);

    }
}
