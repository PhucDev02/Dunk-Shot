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
    private void Awake()
    {
        DOTween.KillAll();
        this.RegisterListener(EventID.OnChangeTheme, (param) => ApplyTheme());
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
        flashTransition.GetComponent<Image>().DOFade(a, 0);
        flashTransition.GetComponent<Image>().DOFade(0, timeFade);
    }
    public void Reload()
    {
        flashTransition.GetComponent<Image>().DOFade(a, timeFade).OnComplete(() =>
        {
            SceneManager.LoadScene(0);
        });
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
    [SerializeField] Color darkColor, lightColor;

}
