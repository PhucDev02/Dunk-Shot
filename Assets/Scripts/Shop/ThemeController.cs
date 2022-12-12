using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThemeController : MonoBehaviour
{
    public static ThemeController Instance;
    [SerializeField] Transform normalTheme,seasonsTheme;
    [SerializeField] GameObject child;
    private void Awake()
    {
        Instance = this;
        this.RegisterListener(EventID.OnChangeTheme, (param) => UpdateUITheme());
        PlayerPrefs.SetInt("Classic", 1);
    }
    public void InitThemeShop()
    {
        GameObject tmp;
        for(int i=0;i<GameManager.Instance.themes.Length-1;i++)
        {
            tmp = Instantiate(child, normalTheme);
            tmp.GetComponent<ThemeDisplay>().theme = GameManager.Instance.themes[i];
        }
        tmp = Instantiate(child, seasonsTheme);
        tmp.GetComponent<ThemeDisplay>().theme = GameManager.Instance.themes[GameManager.Instance.themes.Length-1];
    }
    public void SetUnlockStatus(Theme theme,int status)
    {
        PlayerPrefs.SetInt(Utility.GetThemeString(theme), status);
    }
    public int GetUnlockStatus(Theme theme)
    {
        return PlayerPrefs.GetInt(Utility.GetThemeString(theme));
    }
    
    public void UpdateUITheme()
    {
        foreach(Transform obj in normalTheme)
        {
            obj.GetComponent<ThemeDisplay>().UpdateChooseStatus();
        }
        foreach (Transform obj in seasonsTheme)
        {
            if(obj.GetComponent<ThemeDisplay>()!=null)
            obj.GetComponent<ThemeDisplay>().UpdateChooseStatus();
        }
    }
}
