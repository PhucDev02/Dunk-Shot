using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementManager : MonoBehaviour
{
    // Start is called before the first frame update
    //
    public static AchievementManager Instance;
    private void Awake()
    {
        Instance = this;
        this.RegisterListener(EventID.OnPerfectx2, (param) => UpdatePerfect());
        this.RegisterListener(EventID.OnPerfectx3, (param) => UpdatePerfect());
        this.RegisterListener(EventID.OnBounceWall, (param) => UpdateBounce());
        this.RegisterListener(EventID.OnGameOver, (param) => UpdateTotalScore());

    }

    public void UpdatePerfect()
    {
        if (!GameController.Instance.challengeMode)
            PlayerPrefs.SetInt("PerfectCount", PlayerPrefs.GetInt("PerfectCount") + 1);
    }
    public void UpdateBounce()
    {
        if (!GameController.Instance.challengeMode)
            PlayerPrefs.SetInt("BounceCount", PlayerPrefs.GetInt("BounceCount") + 1);
    }
    public void UpdateTotalScore()
    {
        if (!GameController.Instance.challengeMode)
            PlayerPrefs.SetInt("TotalScore", PlayerPrefs.GetInt("TotalScore") + GameController.Instance.GetScore());
    }
    //bounce,perfect,sumScore,bestscore
    public void OnMenu()
    {
        //em lam de cho du chi tieu nen code phan nay khong duoc hay lam :<
        if(PlayerPrefs.GetInt("BounceCount")>=20)
        {
            PopupManager.Instance.ShowUnlockPopup(GameManager.Instance.balls[9]);
        }
        if (PlayerPrefs.GetInt("TotalScore") >= 100)
        {
            PopupManager.Instance.ShowUnlockPopup(GameManager.Instance.balls[11]);
        }
        if (PlayerPrefs.GetInt("PerfectCount") >= 10)
        {
            PopupManager.Instance.ShowUnlockPopup(GameManager.Instance.balls[10]);
        }
        if (PlayerPrefs.GetInt("BestScore") >= 80)
        {
            PopupManager.Instance.ShowUnlockPopup(GameManager.Instance.balls[12]);
        }
    }
}
