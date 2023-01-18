using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    // Start is called before the first frame update
    public static GameController Instance;
    void Awake()
    {
        Instance = this;
    }
    [SerializeField] BallController ball;

    [SerializeField] public int score, streak;
    public bool isPerfect;
    public int bounceCnt;
    public bool IsGameOver,reachNewBest;
    public bool challengeMode;
    public void NewGame()
    {
        Reset();
        UI_GameOver.Instance.NewGame();
        UI_SecondChange.Instance.NewGame();
        UI_Menu.Instance.NewGame();
        UI_Gameplay.Instance.NewGame();
        ball.NewGame();
        HoopsPooler.Instance.NewEndlessGame();
        CameraController.Instance.NewGame();
    }

    private void Start()
    {
        Application.targetFrameRate = 120;
        challengeMode = false;
        Reset();
        NewGame();
        this.RegisterListener(EventID.OnBounceSide, (param) => OnBounceSide());
        this.RegisterListener(EventID.OnBounceWall, (param) => OnBounceWall());
        this.RegisterListener(EventID.OnGameOver, (param) => OnGameOver());
        this.RegisterListener(EventID.OnSecondChange, (param) => ActiveSecondChange());
    }
    public void Reset()
    {
        score = 0;
        streak = 0;
        bounceCnt = 0;
        isPerfect = true;
        IsGameOver = false;
        reachNewBest = false;
        EffectGameplay.Instance.Reset();
    }
    private void OnBounceWall()
    {
        bounceCnt++;
    }

    private void OnBounceSide()
    {
        streak = 0;
        isPerfect = false;

    }
    public void UpdateScore()
    {
        if (HoopsPooler.Instance.IsValidShot())
        {
            if (isPerfect == true)
            {
                streak++;
            }
            score += (bounceCnt == 0 ? 1 : 2) * ((streak + 1) > 10 ? 10 : (streak + 1));
            executeBestScore();
            UI_Gameplay.Instance.ShowIncreasePoint((bounceCnt == 0 ? 1 : 2) * ((streak + 1) > 10 ? 10 : (streak + 1)), streak, bounceCnt);
            bounceCnt = 0;
        }
    }
    private void executeBestScore()
    {
        if (score > PlayerPrefs.GetInt("BestScore") && !challengeMode)
        {
            if (score > 20 && PlayerPrefs.GetInt("BestScore") != 0&&reachNewBest==false)
            {
                reachNewBest = true;
                EffectGameplay.Instance.NewBestEffect();
            }
            PlayerPrefs.SetInt("BestScore", score);
        }
    }
    public int GetScore()
    {
        return score;
    }
    private void OnGameOver()
    {
        if (score != 0)
        {
            if (!challengeMode)
                IsGameOver = true;
            StartCoroutine(WaitGameOver());
        }
        else
        {
            ball.Respawn();
            HoopsPooler.Instance.OnSecondChange();
        }
    }
    IEnumerator WaitGameOver()
    {
        yield return new WaitForSeconds(0.3f);
        if (!challengeMode)
        {
            if (UI_SecondChange.Instance.IsActivated == false && score >= 3)
            {
                UI_SecondChange.Instance.ActivePanel();
            }
            else if (score != 0)
                UI_GameOver.Instance.GameOver();
        }
        else
        {
            switch (ChallengeManager.Instance.type)
            {
                case 1:
                    NewBallGameplay.Instance.DecreaseLife();
                    break;
                case 6:
                    NewBallGameplay.Instance.DecreaseLife();
                    break;
                default:
                    PopupManager.Instance.ShowFailedPopup(ChallengeManager.Instance.type);
                    break;
            }
        }
        UI_Gameplay.Instance.HideButton();
    }
    private void ActiveSecondChange()
    {
        IsGameOver = false;
        streak = 0;
        bounceCnt = 0;
    }
    public void RespawnBall()
    {
        if (challengeMode)
        {
            CameraController.Instance.NewGame();
            HoopsPooler.Instance.Reset();
        }
        ball.Respawn();
    }
    public void RespawnAboveLastHoop()
    {
        ball.Respawn();
    }
}
