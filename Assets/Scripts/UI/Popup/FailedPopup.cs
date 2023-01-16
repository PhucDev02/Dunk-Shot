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
    public Transform board;
    public void ShowPopup()
    {
        gameObject.SetActive(true);
    }
    public void AssignPopup(int type)
    {
        description.color = UI_Challenge.Instance.GetChallengeColor(type);
        description.text = "Hoops completed " + GameplayChallengeManager.Instance.passedHoop + "/" + ChallengeManager.Instance.lastLevel.totalHoops;
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
        board.localScale = Vector3.zero;
        board.DOScale(1, 0.5f).SetEase(Ease.OutBack);
    }
    private void OnDisable()
    {
        board.DOKill();
    }
}
