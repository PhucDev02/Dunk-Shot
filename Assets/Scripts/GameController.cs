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
    [SerializeField] private int score, streak, bounceCnt;
    public bool isPerfect;
    public bool IsGameOver;
    private void Start()
    {
        Reset();
        this.RegisterListener(EventID.OnContactHoop, (param) => UpdateScore());
        this.RegisterListener(EventID.OnBounceSide, (param) => OnBounceSide());
        this.RegisterListener(EventID.OnBounceWall, (param) => OnBounceWall());
        this.RegisterListener(EventID.OnGameOver, (param) => OnGameOver());
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
            UI_Gameplay.Instance.ShowIncreasePoint((bounceCnt == 0 ? 1 : 2) * ((streak + 1) > 10 ? 10 : (streak + 1)), streak, bounceCnt);
            bounceCnt = 0;
        }
    }
    public int GetScore()
    {
        return score;
    }
    private void OnGameOver()
    {
        IsGameOver = true;
        if (UI_SecondChange.Instance.IsActivated == false)
        {
            UI_SecondChange.Instance.ActiveSecondChange();
        }
        else UI_GameOver.Instance.GameOver();
    }
}
