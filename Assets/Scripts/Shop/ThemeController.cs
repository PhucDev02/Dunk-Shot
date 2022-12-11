using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThemeController : MonoBehaviour
{
    public static ThemeController Instance;
    [SerializeField] Transform content;
    [SerializeField] GameObject child;
    private void Awake()
    {
        Instance = this;
        this.RegisterListener(EventID.OnChangeTheme, (param) => UpdateUITheme());
        PlayerPrefs.SetInt("Classic", 1);
    }
    public void InitThemeShop()
    {
        for(int i=0;i<GameManager.Instance.themes.Length;i++)
        {
            var tmp = Instantiate(child, content);
            tmp.GetComponent<ThemeDisplay>().theme = GameManager.Instance.themes[i];
        }
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
        foreach(Transform obj in content)
        {
            obj.GetComponent<ThemeDisplay>().UpdateChooseStatus();
        }
    }
}
