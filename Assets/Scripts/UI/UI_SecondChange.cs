using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
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
    void Start()
    {
        timer = 5.0f;
        panel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (IsActivated == true)
        {
            timer -= Time.deltaTime;
            clock.fillAmount = timer / 5.0f;
        }
    }
    public void ActivePanel()
    {
        if (IsActivated == false)
        {
            score.text = GameController.Instance.GetScore().ToString();
            panel.SetActive(true);
            IsActivated = true;
        }
    }
    public void ActiveSecondChange()
    {
        panel.SetActive(false);
        this.PostEvent(EventID.OnSecondChange);
    }
}
