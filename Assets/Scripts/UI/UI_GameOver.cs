using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
    using DG.Tweening;
using System;

public class UI_GameOver : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject panel;
    [SerializeField] Transform getNewBall, capture, playAgain, setting;
    [SerializeField] TextMeshProUGUI bestScore, score,currentScore;
    public static UI_GameOver Instance;
    private void Awake()
    {
        Instance = this;
        this.RegisterListener(EventID.OnChangeTheme, (param) => ApplyThemeAndTheme());
        this.RegisterListener(EventID.OnSwitchDarkmode, (param) => ApplyThemeAndTheme());
        this.RegisterListener(EventID.OnGameOver, (param) => UpdateScore());
    }

    private void ApplyThemeAndTheme()
    {

        if (PlayerPrefs.GetInt("Darkmode") == 1)
        {
            currentScore.color = GameManager.Instance.GetTheme().scoreDarkColor;
        }
        else
        {
            currentScore.color = GameManager.Instance.GetTheme().scoreColor;
        }
    }
    private void UpdateScore()
    {
        currentScore.text = GameController.Instance.GetScore().ToString();
    }
    private void Start()
    {
        ApplyThemeAndTheme();
        ResetTransform();
        panel.SetActive(false);
    }
    public void NewGame()
    {
        Start();
    }
    public void ResetTransform()
    {
        bestScore.DOFade(0, 0);
        score.DOFade(0, 0);
        getNewBall.DOScale(0.2f, 0);
        capture.DOScale(0, 0);
        playAgain.DOScale(0, 0);
        setting.DOScale(0, 0);
    }
    public void GameOver()
    {
        score.text = PlayerPrefs.GetInt("BestScore").ToString();
        panel.SetActive(true);
        bestScore.DOFade(1, 0.5f);
        score.DOFade(1, 0.5f);

        getNewBall.DOScale(1, 0.3f);

        capture.DOScale(1, 0.4f).SetDelay(0.15f).SetEase(Ease.OutBack);

        playAgain.DOScale(1, 0.4f).SetDelay(0.3f).SetEase(Ease.OutBack);

        setting.DOScale(1, 0.4f).SetDelay(0.45f).SetEase(Ease.OutBack);
    }

}
