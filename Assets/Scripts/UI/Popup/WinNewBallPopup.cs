using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;
public class WinNewBallPopup : MonoBehaviour
{
    public new Transform light;
    public Transform board;
    public Image preview;
    private Ball ball;
    public void ShowPopup()
    {
        gameObject.SetActive(true);
    }
    public void AssignPopup(Ball ball)
    {
        this.ball = ball;
        preview.sprite = ball.spriteBall;
    }
    private void OnEnable()
    {
        light.transform.DOKill(); 
        light.localScale = Vector3.zero;
        light.DOScale(1, 0.5f).SetEase(Ease.OutBack).SetDelay(0.5f);
        light.transform.transform.DORotate(Vector3.forward * 360, 4.0f, RotateMode.FastBeyond360).SetLoops(-1, LoopType.Restart).SetEase(Ease.Linear).SetDelay(0.5f);
        board.localScale = Vector3.zero;
        board.DOScale(1, 0.5f).SetEase(Ease.OutBack).SetDelay(0.5f);
    }
    private void OnDisable()
    {
        light.transform.DOKill();
        board.DOKill();
    }
    public void OnClickEquip()
    {
        gameObject.SetActive(false);
        PlayerPrefs.SetInt("IdBallSelected", ball.id);
        this.PostEvent(EventID.OnChangeBall);
    }
}
