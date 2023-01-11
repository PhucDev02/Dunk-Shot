using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UI_Challenge : MonoBehaviour
{
    public static UI_Challenge Instance;
    //
    [SerializeField] GameObject panel;
    [SerializeField] GameObject challengeCanvas;
    [SerializeField] GameObject descriptionPanel, pausePanel;
    // description
    [Header("Description")]
    [SerializeField] Image reward;
    [SerializeField] Image banner;
    [SerializeField] Image playbtn;
    [SerializeField] TextMeshProUGUI challengeName, description;
    //top element
    [Header("Top Element")]
    [SerializeField] Image board;
    [SerializeField] TextMeshProUGUI challengeNameTopElm;
    //pause
    [Header("Pause popup")]
    [SerializeField] TextMeshProUGUI descriptionPause;
    [SerializeField] Image rewardPause;
    [SerializeField] Image restartBtn;
    //
    #region Sprite and color
    [Header("Banner Sprite")]
    [SerializeField] Sprite newBall;
    [SerializeField] Sprite collect;
    [SerializeField] Sprite time;
    [SerializeField] Sprite score;
    [SerializeField] Sprite bounce;
    [SerializeField] Sprite noAim;
    //

    [Header("Banner Sprite")]
    [SerializeField] Sprite newBallBtn;
    [SerializeField] Sprite collectBtn;
    [SerializeField] Sprite timeBtn;
    [SerializeField] Sprite scoreBtn;
    [SerializeField] Sprite bounceBtn;
    [SerializeField] Sprite noAimBtn;
    //
    [Header("Reward")]
    [SerializeField] Sprite token;
    [SerializeField] Sprite mysteriousBall;
    //
    [Header("Color")]
    [SerializeField] Color orange;
    [SerializeField] Color turquoise;
    [SerializeField] Color blue;
    [SerializeField] Color green;
    [SerializeField] Color pink;
    [SerializeField] Color lightRed;
    #endregion
    private void Awake()
    {
        Instance = this;
    }
    //NewBall, 1
    //Collect, 2
    //Time, 3
    //Score,4
    //Bounce,5
    //NoAim 6
    public void GoToChallenge(int type)
    {
        AssignChallenge(type);
        panel.SetActive(false);
        descriptionPanel.SetActive(true);
        challengeCanvas.SetActive(true);
    }
    public void BackToChallenge()
    {
        panel.SetActive(true);
        pausePanel.SetActive(false);
        challengeCanvas.SetActive(false);
    }
    public void AssignChallenge(int type)
    {
        switch(type)
        {
            case 1:
                banner.sprite = newBall;
                reward.sprite = mysteriousBall;
                descriptionPause.color = orange;
                board.color = orange;
                playbtn.sprite = newBallBtn;
                restartBtn.sprite = newBallBtn;
                //name
                break;
            case 2:
                banner.sprite = collect;
                reward.sprite = token;
                descriptionPause.color = turquoise;
                playbtn.sprite = collectBtn;
                restartBtn.sprite = collectBtn;
                board.color = turquoise;
                break;
            case 3:
                banner.sprite = time;
                reward.sprite = token;
                descriptionPause.color = blue;
                playbtn.sprite = timeBtn;
                restartBtn.sprite = timeBtn;
                board.color = blue;
                break;
            case 4:
                banner.sprite = score;
                reward.sprite = token;
                descriptionPause.color = green;
                playbtn.sprite = scoreBtn;
                restartBtn.sprite = scoreBtn;
                restartBtn.color = green;
                break;
            case 5:
                banner.sprite = bounce;
                reward.sprite = token;
                descriptionPause.color = pink;
                playbtn.sprite = bounceBtn;
                restartBtn.sprite = bounceBtn;
                board.color = pink;
                break;
            default: //noAim
                banner.sprite = noAim;
                reward.sprite = token;
                descriptionPause.color = lightRed;
                playbtn.sprite = noAimBtn;
                restartBtn.sprite = noAimBtn;
                board.color = lightRed;
                break;
        }
    }
}
