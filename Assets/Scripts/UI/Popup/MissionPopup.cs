using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class MissionPopup : Popup
{
    [SerializeField] Image fill;
    [SerializeField] TextMeshProUGUI progress;
    public override void AssignPopup(Ball ball)
    {
        base.AssignPopup(ball);
        //fill
        preview.sprite = ball.spriteBall; 
        if (ball.id == 9)
        {
            fill.fillAmount = 1.0f * PlayerPrefs.GetInt("BounceCount") / 20;
            progress.text = $"{PlayerPrefs.GetInt("BounceCount")}/20";
        }
        if (ball.id == 11)
        {
            fill.fillAmount = 1.0f * PlayerPrefs.GetInt("TotalScore") / 100;
            progress.text = $"{PlayerPrefs.GetInt("TotalScore")}/100";
        }
        if (ball.id == 10)
        {
            fill.fillAmount = 1.0f * PlayerPrefs.GetInt("PerfectCount") / 10;
            progress.text = $"{PlayerPrefs.GetInt("PerfectCount")}/10";
        }
        if (ball.id == 12)
        {
            fill.fillAmount = 1.0f * PlayerPrefs.GetInt("BestScore") / 80;
            progress.text = $"{PlayerPrefs.GetInt("BestScore")}/80";
        }
    }
}
