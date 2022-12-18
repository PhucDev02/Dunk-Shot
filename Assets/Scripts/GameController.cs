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

    [SerializeField] private int score, streak, bounceCnt;
    public bool isPerfect;
    public bool IsGameOver;
    public void NewGame()
    {
        Reset();
        UI_GameOver.Instance.NewGame();
        UI_SecondChange.Instance.NewGame();
        UI_Menu.Instance.NewGame();
        UI_Gameplay.Instance.NewGame();
        HoopsPooler.Instance.NewGame();
        ball.NewGame();
        CameraController.Instance.NewGame();
    }
    private void Start()
    {
        Reset();
        this.RegisterListener(EventID.OnContactHoop, (param) => UpdateScore());
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
    private void UpdateScore()
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
        if(score>PlayerPrefs.GetInt("BestScore"))
        {
            PlayerPrefs.SetInt("BestScore", score);
        }
    }
    public int GetScore()
    {
        return score;
    }
    private void OnGameOver()
    {
        if(score!=0)
        {
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
        if (UI_SecondChange.Instance.IsActivated == false && score >= 3)
        {
            UI_SecondChange.Instance.ActivePanel();
        }
        else UI_GameOver.Instance.GameOver();
        UI_Gameplay.Instance.HideButton();
    }
    private void ActiveSecondChange()
    {
        IsGameOver = false;
    }
}
