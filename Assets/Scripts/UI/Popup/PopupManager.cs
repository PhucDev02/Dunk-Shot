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
    [SerializeField]
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
