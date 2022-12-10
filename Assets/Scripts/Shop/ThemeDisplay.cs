using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ThemeDisplay : MonoBehaviour
{
    public Theme theme;
    [SerializeField] GameObject orangeBorder,lockStatus,cover;
    [SerializeField] Image themeCell;
    [SerializeField] TextMeshProUGUI price;
    void Start()
    {
        themeCell.sprite = theme.themeCell;
        price.text = theme.price.ToString();
        if(ThemeController.Instance.GetLockStatus(theme)==1) //locking
        {
            lockStatus.SetActive(true);
            cover.SetActive(true);
        }
        else //unlocked
        {
            cover.SetActive(false);
            lockStatus.SetActive(false);
        }
        if(GameManager.Instance.GetTheme()==theme)
            orangeBorder.SetActive(true);

    }

}
