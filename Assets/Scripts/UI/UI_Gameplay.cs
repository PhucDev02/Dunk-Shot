using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
public class UI_Gameplay : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] TextMeshProUGUI score, stars_txt, increaseScore, bounceCnt, streakCnt;
    [SerializeField] GameObject pauseBtn;
    public static UI_Gameplay Instance;
    private void Awake()
    {
        Instance = this;
        pauseBtn.SetActive(false);
        HideScore();
    }
    public void ShowIncreasePoint(int point, int streak, int bounce)
    {
        if (streak == 0) streakCnt.text = "";
        else if (streak == 1) streakCnt.text = "Perfect!";
        else
            streakCnt.text = "Perfect x" + streak.ToString();
        //
        if (bounce == 0) bounceCnt.text = "";
        else if (bounce == 1) bounceCnt.text = "Bounce!";
        else
            bounceCnt.text = "Bounce x" + bounce.ToString();
        //
        increaseScore.text = "+" + point.ToString();

        streakCnt.transform.position = HoopsPooler.Instance.GetLastHoop().position + Vector3.up * 0.5f;
        bounceCnt.transform.position = streakCnt.transform.position;
        increaseScore.transform.position = streakCnt.transform.position;

        streakCnt.DOFade(1, 0.1f);
        streakCnt.transform.DOMoveY(streakCnt.transform.position.y + 0.5f, 0.5f).SetEase(Ease.InOutSine).OnComplete(() =>
         {
             streakCnt.DOFade(0, 0.2f);
         });

        bounceCnt.DOFade(1, 0.1f);
        bounceCnt.transform.DOMoveY(bounceCnt.transform.position.y + 0.4f, 0.5f).SetDelay(0.3f).SetEase(Ease.InOutSine).OnComplete(() =>
        {
            bounceCnt.DOFade(0, 0.2f);
        });

        increaseScore.DOFade(1, 0.1f).SetDelay(0.6f).OnComplete(() =>
        {
            UpdateScore();
            increaseScore.transform.DOMoveY(increaseScore.transform.position.y + 0.3f, 0.5f).SetEase(Ease.InOutSine).OnComplete(() =>
            {
                increaseScore.DOFade(0, 0.2f);
            });
        });
        // hien pf,bounce,diem

    }
    public void UpdateScore()
    {
        score.text = GameController.Instance.GetScore().ToString();
    }
    public void HideButton()
    {
        pauseBtn.GetComponent<Image>().DOFade(0, 0);
        pauseBtn.SetActive(false);
    }
    public void UnhideButton()
    {
        score.GetComponent<Image>().DOFade(1, 0);
        pauseBtn.GetComponent<Image>().DOFade(1, 0.4f);
        pauseBtn.SetActive(true);
    }
    public void HideScore()
    {
        score.GetComponent<Image>().DOFade(0, 0);
    }
}
