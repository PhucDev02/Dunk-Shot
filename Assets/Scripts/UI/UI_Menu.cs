using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class UI_Menu : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject panel;
    public static UI_Menu Instance;
    void Awake()
    {
        Instance = this;
    }
    public void NewGame()
    {
        panel.GetComponent<CanvasGroup>().DOFade(0, 0.3f).OnComplete(() =>
        {
            panel.SetActive(false);
        });
        UI_Gameplay.Instance.UnhideButton();
    }
}
