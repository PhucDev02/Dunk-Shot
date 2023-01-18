using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class NewBallGameplay : MonoBehaviour
{
    [SerializeField] int lifes = 3;
    [SerializeField] Image lifeImg1, lifeImg2, lifeImg3;

    public static NewBallGameplay Instance;
    private void Awake()
    {
        Instance = this;
        Reset();
    }
    private void OnEnable()
    {
        Reset();
    }
    public void Reset()
    {
        lifes = 3;
        lifeImg1.transform.DOScale(1, 0);
        lifeImg2.transform.DOScale(1, 0);
        lifeImg3.transform.DOScale(1, 0);
    }
    public void DecreaseLife()
    {
        lifes--;
        AudioManager.Instance.Play("LostLife");
        if (lifes == 2)
        {
            lifeImg3.transform.DOScale(0, 0.5f).SetEase(Ease.InBack);
            GameController.Instance.RespawnAboveLastHoop();

            return;
        }
        if (lifes == 1)
        {
            lifeImg2.transform.DOScale(0, 0.5f).SetEase(Ease.InBack);
            GameController.Instance.RespawnAboveLastHoop();
            return;
        }
        if (lifes == 0)
        {
            lifeImg1.transform.DOScale(0, 0.5f).SetEase(Ease.InBack).OnComplete(() =>
            {
                PopupManager.Instance.ShowFailedPopup(ChallengeManager.Instance.type);
            });
            return;
        }
    }
}
