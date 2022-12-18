using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using System;

public class UI_SecondChange : MonoBehaviour
{
    public static UI_SecondChange Instance;
    private void Awake()
    {
        Instance = this;
    }
    public bool IsActivated = false;
    private float timer;
    [SerializeField] GameObject panel;
    [SerializeField] Image clock;
    [SerializeField] TextMeshProUGUI score;

    [SerializeField] Transform clockObj, continueBtn, ball;
    [SerializeField] Transform notks;
    void Start()
    {
        timer = 5.0f;
        panel.SetActive(false);
        clockObj.DOScale(0, 0);
        continueBtn.DOScale(0, 0);
        notks.DOScale(0, 0); 
        this.RegisterListener(EventID.OnChangeTheme, (param) => ApplyThemeAndTheme());
        this.RegisterListener(EventID.OnSwitchDarkmode, (param) => ApplyThemeAndTheme());
        ApplyThemeAndTheme();
    }

    private void ApplyThemeAndTheme()
    {
        if (PlayerPrefs.GetInt("Darkmode") == 1)
        {
            score.color = GameManager.Instance.GetTheme().scoreDarkColor;
        }
        else
        {
            score.color = GameManager.Instance.GetTheme().scoreColor;
        }
    }

    // Update is called once per frame
    void Update()
    {
       if(panel.activeInHierarchy)
        {
            timer -= Time.deltaTime;
            clock.fillAmount = timer / 5.0f;
            if (timer <= 0)
            {
                GameController.Instance.NewGame();
            }
        }
    }
    public void ActivePanel()
    {
        if (IsActivated == false)
        {
            score.text = GameController.Instance.GetScore().ToString();
            panel.SetActive(true);

            animation();

            IsActivated = true;
        }
    }
    public void ActiveSecondChange()
    {
        panel.SetActive(false);
        UI_Gameplay.Instance.UnhideButton();
        this.PostEvent(EventID.OnSecondChange);
    }
    private new void animation()
    {
        clockObj.DOScale(1, 0.3f).SetEase(Ease.OutBack).OnComplete(() =>
        {
            ball.DORotate(Vector3.forward * 20, 1).SetEase(Ease.InOutQuad).SetLoops(-1, LoopType.Yoyo);
        });
        continueBtn.DOScale(1, 0.3f).SetEase(Ease.OutBack).OnComplete(() =>
        {
            continueBtn.DOScale(1.1f, 0.3f).SetEase(Ease.InOutQuad).SetLoops(-1, LoopType.Yoyo);
        });
        notks.DOScale(1, 0.5f).SetEase(Ease.OutBack).SetDelay(timer/3);
    }
    public void NewGame()
    {
        panel.SetActive(false);
        timer = 5.0f;

        clockObj.DOScale(0, 0);
        continueBtn.DOScale(0, 0);
        notks.DOScale(0, 0);
        IsActivated = false;
    }
}
