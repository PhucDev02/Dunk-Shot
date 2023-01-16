using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ChallengeCellDisplay : MonoBehaviour
{
    [SerializeField] Image fill;
    [SerializeField] GameObject completeObj;
    [SerializeField] TextMeshProUGUI progressTxt;
    [SerializeField] Button btn;
    public int type;
    public void UpdateProgress()
    {
        int tmp = 0;
        if (type == 1)
        {
            for (int i = 0; i < ChallengeManager.Instance.newBallLevels.Length; i++)
            {
                if (PlayerPrefs.GetInt("NewBall" + i) == 1)
                {
                    tmp += 1;
                }
            }
            progressTxt.text = ((100 * tmp) / ChallengeManager.Instance.newBallLevels.Length) + "%";
            fill.fillAmount = 1.0f * tmp / ChallengeManager.Instance.newBallLevels.Length;
        }
        if (type == 2)
        {
            for (int i = 0; i < ChallengeManager.Instance.collectLevels.Length; i++)
            {
                if (PlayerPrefs.GetInt("Collect" + i) == 1)
                {
                    tmp += 1;
                }
            }
            progressTxt.text = ((100 * tmp) / ChallengeManager.Instance.collectLevels.Length) + "%";
            fill.fillAmount = 1.0f * tmp / ChallengeManager.Instance.collectLevels.Length;


        }
        if (type == 3)
        {
            for (int i = 0; i < ChallengeManager.Instance.timeLevels.Length; i++)
            {
                if (PlayerPrefs.GetInt("Time" + i) == 1)
                {
                    tmp += 1;
                }
            }
            progressTxt.text = ((100 * tmp) / ChallengeManager.Instance.timeLevels.Length) + "%";
            fill.fillAmount = 1.0f * tmp / ChallengeManager.Instance.timeLevels.Length;
        }
        if (type == 4)
        {
            for (int i = 0; i < ChallengeManager.Instance.scoreLevels.Length; i++)
            {
                if (PlayerPrefs.GetInt("Score" + i) == 1)
                {
                    tmp += 1;
                }
            }
            progressTxt.text = ((100 * tmp) / ChallengeManager.Instance.scoreLevels.Length) + "%";
            fill.fillAmount = 1.0f * tmp / ChallengeManager.Instance.scoreLevels.Length;
        }
        if (type == 5)
        {
            for (int i = 0; i < ChallengeManager.Instance.bounceLevels.Length; i++)
            {
                if (PlayerPrefs.GetInt("Bounce" + i) == 1)
                {
                    tmp += 1;
                }
            }
            progressTxt.text = ((100 * tmp) / ChallengeManager.Instance.bounceLevels.Length) + "%";
            fill.fillAmount = 1.0f * tmp / ChallengeManager.Instance.bounceLevels.Length;
        }
        if (type == 6)
        {
            for (int i = 0; i < ChallengeManager.Instance.noAimLevels.Length; i++)
            {
                if (PlayerPrefs.GetInt("NoAim" + i) == 1)
                {
                    tmp += 1;
                }
            }
            progressTxt.text = ((100 * tmp) / ChallengeManager.Instance.noAimLevels.Length) + "%";
            fill.fillAmount = 1.0f * tmp / ChallengeManager.Instance.noAimLevels.Length;
        }
        setStatus();
    }
    private void setStatus()
    {
        if (fill.fillAmount == 1)
        {
            completeObj.SetActive(true);
            btn.interactable=false;
        }
    }
}
