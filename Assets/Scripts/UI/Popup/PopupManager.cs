using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class PopupManager : MonoBehaviour
{
    public static PopupManager Instance;
    [SerializeField] GameObject panel;
    private void Awake()
    {
        Instance = this;
        this.RegisterListener(EventID.OnShowPopup, (param) => ShowPopup((Ball)(param)));
    }
    [SerializeField] VideoPopup videoPopup;
    [SerializeField] MissionPopup missionPopup;
    [SerializeField] ChallengePopup challengePopup;
    [SerializeField] SecretPopup secretPopup;
    [SerializeField] FortunePopup fortunePopup;
    [SerializeField] UnlockPopup unlockPopup;
    [SerializeField] WinTokenChallengePopup winTokenPopup;
    [SerializeField] WinNewBallPopup winBallPopup;
    [SerializeField] FailedPopup failedPopup;
    public void ShowFailedPopup(int type)
    {
        failedPopup.AssignPopup(type);
        failedPopup.ShowPopup();
    }
    public void ShowWinBallPopup(Ball ball)
    {
        winBallPopup.AssignPopup(ball);
        winBallPopup.ShowPopup();
    }
    public void ShowWinTokenPopup(int type)
    {
        panel.SetActive(false);
        winTokenPopup.AssignPopup(type);
        winTokenPopup.ShowPopup();
        UI_Controller.Instance.UpdateCurrency(PlayerPrefs.GetInt("Stars"), PlayerPrefs.GetInt("Tokens") + 20);
    }
    public void ShowUnlockPopup(Ball ball)
    {
        if (BallShopController.Instance.GetUnlockStatus(ball.id) == 0)
        {
            panel.SetActive(true);
            unlockPopup.AssignPopup(ball);
            unlockPopup.ShowPopup();
        }
    }
    public void ShowPopup(Ball ball)
    {
        Logger.Log("showPopup");
        panel.SetActive(true);
        switch (ball.type)
        {
            case BallType.Video:
                videoPopup.AssignPopup(ball);
                videoPopup.ShowPopup();
                break;
            case BallType.Mission:
                missionPopup.AssignPopup(ball);
                missionPopup.ShowPopup();
                break;
            case BallType.Challenge:
                challengePopup.AssignPopup(ball);
                challengePopup.ShowPopup();
                break;
            case BallType.Secret:
                secretPopup.AssignPopup(ball);
                secretPopup.ShowPopup();
                break;
            default:
                fortunePopup.AssignPopup(ball);
                fortunePopup.ShowPopup();
                break;
        }
    }
}
