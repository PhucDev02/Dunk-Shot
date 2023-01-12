using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;
using TMPro;
using System;

public class UI_Controller : MonoBehaviour
{

    [SerializeField] Transform flashTransition;
    [SerializeField] float a, timeFade;
    [SerializeField] TextMeshProUGUI[] txt;
    [SerializeField] Image[] panels;
    [SerializeField] Image[] buttons;
    [SerializeField] TextMeshProUGUI[] tokens, stars;
    private void Awake()
    {
        DOTween.KillAll();
        this.RegisterListener(EventID.OnChangeTheme, (param) => ApplyTheme());
        this.RegisterListener(EventID.OnPurchaseItem, (param) => UpdateCurrency());
        flashTransition.gameObject.SetActive(false);
    }

    private void ApplyTheme()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].color = GameManager.Instance.GetTheme().buttonColor;
        }
        for (int i = 0; i < txt.Length; i++)
        {
            txt[i].color = GameManager.Instance.GetTheme().buttonColor;
        }
    }

    private void Start()
    {
        ApplyTheme();
        ApplyDarkmode();
        UpdateCurrency();
    }
    public void Reload()
    {
        DOTween.KillAll();
        flashTransition.gameObject.SetActive(true);
        flashTransition.GetComponent<Image>().DOFade(a, timeFade).SetUpdate(true).OnComplete(() =>
        {
            Time.timeScale = 1;
            GameController.Instance.NewGame();
            flashTransition.GetComponent<Image>().DOFade(a, 0).SetUpdate(true);
            flashTransition.GetComponent<Image>().DOFade(0, timeFade).SetUpdate(true).OnComplete(() => { flashTransition.gameObject.SetActive(false); });
            //SceneManager.LoadScene(0);
        });
    }
    public void GoToChallenge(int type)
    {
        DOTween.KillAll();
        flashTransition.gameObject.SetActive(true);
        UI_Menu.Instance.Hide();
        flashTransition.GetComponent<Image>().DOFade(a, timeFade).SetUpdate(true).OnComplete(() =>
        {
            //wtf thing
            UI_Challenge.Instance.GoToChallenge(type);
            flashTransition.GetComponent<Image>().DOFade(a, 0).SetUpdate(true);
            flashTransition.GetComponent<Image>().DOFade(0, timeFade).SetUpdate(true).OnComplete(() => { flashTransition.gameObject.SetActive(false); });
        });
    }
    public void BackToChallenge()
    {
        DOTween.KillAll();
        flashTransition.gameObject.SetActive(true);
        UI_Menu.Instance.Show();
        flashTransition.GetComponent<Image>().DOFade(a, timeFade).SetUpdate(true).OnComplete(() =>
        {
            //wtf thing
            UI_Challenge.Instance.BackToChallenge();
            flashTransition.GetComponent<Image>().DOFade(a, 0);
            flashTransition.GetComponent<Image>().DOFade(0, timeFade).OnComplete(() => { flashTransition.gameObject.SetActive(false); });
        });
    }
    public void HardReload()
    {
        Time.timeScale = 1;
        GameController.Instance.NewGame();
    }
    public void SwitchDarkMode()
    {
        PlayerPrefs.SetInt("Darkmode", 1 - PlayerPrefs.GetInt("Darkmode"));
        ApplyDarkmode();
        this.PostEvent(EventID.OnSwitchDarkmode);
    }
    void ApplyDarkmode()
    {
        for (int i = 0; i < panels.Length; i++)
            if (PlayerPrefs.GetInt("Darkmode") == 1)
                panels[i].color = darkColor;
            else
                panels[i].color = lightColor;
    }
    void UpdateCurrency()
    {
        for (int i = 0; i < tokens.Length; i++)
        {
            tokens[i].text = PlayerPrefs.GetInt("Tokens").ToString();
        }
        for (int i = 0; i < stars.Length; i++)
        {
            stars[i].text = PlayerPrefs.GetInt("Stars").ToString();
        }
    }
    public void PauseGame()
    {
        Time.timeScale = 0;
    }
    public void ContinueGame()
    {
        Time.timeScale = 1;
    }
    [SerializeField] Color darkColor, lightColor;

    //
    [SerializeField] TextMeshProUGUI fps;
    private float timer;
    private void Update()
    {
        timer += Time.deltaTime;
        if (timer > 1)
        {
            fps.text = ((int)(1 / Time.deltaTime)).ToString();
            timer = 0;
        }
    }
}
