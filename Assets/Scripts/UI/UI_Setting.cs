using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Setting : MonoBehaviour
{
    [SerializeField] GameObject darkmodeOn, darkmodeOff;
    [SerializeField] GameObject soundOn, soundOff;
    [SerializeField] GameObject vibrationOn, vibrationOff;
    [SerializeField] Color orange, gray;
    [SerializeField] Image darkmodeBtn, soundBtn, vibraBtn;
    void Awake()
    {
        this.RegisterListener(EventID.OnSwitchDarkmode, (param) => UpdateDarkmode());
        UpdateDarkmode();
        UpdateAllowSound();
        UpdateAllowVibration();
    }
    void UpdateDarkmode()
    {
        if(PlayerPrefs.GetInt("Darkmode")==1)
        {
            AudioManager.Instance.Play("DarkmodeOn");
            darkmodeOff.SetActive(false);
            darkmodeOn.SetActive(true);
            darkmodeBtn.color = orange;
        }
        else
        {
            AudioManager.Instance.Play("DarkmodeOff");
            darkmodeOff.SetActive(true);
            darkmodeOn.SetActive(false);
            darkmodeBtn.color = gray;
        }
    }
    void UpdateAllowSound()
    {
        if (PlayerPrefs.GetInt("DontAllowSound") == 1) //tat am thanh
        {
            soundOff.SetActive(true);
            soundOn.SetActive(false);
            soundBtn.color = gray;
        }
        else
        {
            AudioManager.Instance.Play("ButtonClick");
            soundOff.SetActive(false);
            soundOn.SetActive(true);
            soundBtn.color = orange;
        }
    }
    void UpdateAllowVibration()
    {
        if (PlayerPrefs.GetInt("AllowVibration") == 1)
        {
            Utility.Vibrate();
            vibrationOff.SetActive(false);
            vibrationOn.SetActive(true);
            vibraBtn.color = orange;
        }
        else
        {
            vibrationOff.SetActive(true);
            vibrationOn.SetActive(false);
            vibraBtn.color = gray;
        }
    }
    public void SwitchDarkMode()
    {
        PlayerPrefs.SetInt("Darkmode", 1 - PlayerPrefs.GetInt("Darkmode"));
        UpdateDarkmode();
        this.PostEvent(EventID.OnSwitchDarkmode);
    }
    public void SwitchAllowSound()
    {
        PlayerPrefs.SetInt("DontAllowSound", 1 - PlayerPrefs.GetInt("DontAllowSound"));
        UpdateAllowSound();
    }
    public void SwitchAllowVibration()
    {
        PlayerPrefs.SetInt("AllowVibration", 1 - PlayerPrefs.GetInt("AllowVibration"));
        UpdateAllowVibration();
    }
}
