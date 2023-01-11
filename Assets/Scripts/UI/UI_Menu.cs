using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;

public class UI_Menu : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject panel;
    [SerializeField] Image darkmodeBtn;
    public static UI_Menu Instance;
    [SerializeField] Sprite darkmodeOn, darkmodeOff;
    void Awake()
    {
        Instance = this;
        this.RegisterListener(EventID.OnSwitchDarkmode, (param) => ApplyDarkmode());
    }

    private void Start()
    {
        ApplyDarkmode();
    }
    public void Hide()
    {
        panel.GetComponent<CanvasGroup>().DOFade(0, 0.3f).OnComplete(() =>
        {
            panel.SetActive(false);
        });
        UI_Gameplay.Instance.UnhideButton();
    }
    public void Show()
    {
        panel.GetComponent<CanvasGroup>().DOFade(1, 0.3f).OnComplete(() =>
        {
            panel.SetActive(true);
        });
        UI_Gameplay.Instance.HideButton();
    }
    private void ApplyDarkmode()
    {
        if(PlayerPrefs.GetInt("Darkmode")==1)
        {
            darkmodeBtn.sprite = darkmodeOn;
        }
        else
            darkmodeBtn.sprite = darkmodeOff;
    }
    public void NewGame()
    {
        panel.SetActive(true);
        panel.GetComponent<CanvasGroup>().DOFade(1, 0.3f);
    }

}
