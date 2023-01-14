using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;
public class FailedPopup : MonoBehaviour
{
    public Image buttonImg, reward;
    public TextMeshProUGUI description;
    public void ShowPopup()
    {
        gameObject.SetActive(true);
    }
    public void AssignPopup(int type)
    {
        buttonImg.sprite = UI_Challenge.Instance.GetSpriteChallenge(type);
        if (type == 1)
        {
            reward.sprite = UI_Challenge.Instance.mysteriousBall;
        }
        else
            reward.sprite = UI_Challenge.Instance.token;
    }
    private void OnEnable()
    {
        transform.localScale = Vector3.zero;
        transform.DOScale(1, 0.5f).SetEase(Ease.OutBack);
    }
    private void OnDisable()
    {
        transform.DOKill();
    }
}
