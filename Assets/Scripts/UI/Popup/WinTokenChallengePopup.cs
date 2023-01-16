using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;
public class WinTokenChallengePopup : MonoBehaviour
{
    public new Transform light;
    public TextMeshProUGUI description, amount;
    public Transform board;
    public Image buttonImg;
    public void ShowPopup()
    {
        gameObject.SetActive(true);
    }
    public void AssignPopup(int type)
    {
        description.color = UI_Challenge.Instance.GetChallengeColor(type);
        buttonImg.sprite = UI_Challenge.Instance.GetSpriteChallenge(type );
        amount.text = "+" + 20;
    }
    private void OnEnable()
    {
        light.transform.DOKill();
        light.transform.transform.DORotate(Vector3.forward * 360, 4.0f, RotateMode.FastBeyond360).SetLoops(-1, LoopType.Restart).SetEase(Ease.Linear).SetDelay(0.5f);
        board.localScale = Vector3.zero;
        board.DOScale(1, 0.5f).SetEase(Ease.OutBack).SetDelay(0.5f);
    }
    private void OnDisable()
    {
        light.transform.DOKill();
        gameObject.SetActive(false);
        board.DOKill();
    }
}
