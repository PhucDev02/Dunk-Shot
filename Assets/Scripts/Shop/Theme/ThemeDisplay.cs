using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ThemeDisplay : MonoBehaviour
{
    public Theme theme;
    [SerializeField] GameObject orangeBorder, lockStatus, cover;
    [SerializeField] Image themeCell;
    [SerializeField] TextMeshProUGUI price;
    void Start()
    {
        themeCell.sprite = theme.themeCell;
        price.text = theme.price.ToString();
        if (ThemeController.Instance.GetUnlockStatus(theme) == 0) //locking
        {
            lockStatus.SetActive(true);
            cover.SetActive(true);
        }
        else //unlocked
        {
            unlock();
        }
        if (GameManager.Instance.GetTheme() == theme)
            orangeBorder.SetActive(true);

    }
    public void OnClick()
    {
        if (ThemeController.Instance.GetUnlockStatus(theme) == 1)//chon theme da mo khoa
        {
            if (GameManager.Instance.GetTheme() == theme) //chon dung theme cu
                UI_Customize.Instance.TurnOffCustomize();
            else //chon theme moi
            {
                GameManager.Instance.SetTheme(theme);
                this.PostEvent(EventID.OnChangeTheme);
                Logger.Log("chon theme ");
            }
        }
        else
        {
            if (PlayerPrefs.GetInt("Tokens") > theme.price)
            {
                Logger.Log("mua");
                PlayerPrefs.SetInt("Tokens", PlayerPrefs.GetInt("Tokens") - theme.price);
                unlock();
            }
            else Logger.Log("ko du tien");
        }
    }
    private void unlock()
    {
        UI_Controller.Instance.UpdateCurrency();
        ThemeController.Instance.SetUnlockStatus(theme, 1);
        lockStatus.SetActive(false);
        cover.SetActive(false);
    }
    public void UpdateChooseStatus()
    {
        if (theme.theme == GameManager.Instance.currentTheme)
        {
            orangeBorder.SetActive(true);
        }
        else orangeBorder.SetActive(false);
    }
}
