using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
public class UI_Challenge : MonoBehaviour
{
    public static UI_Challenge Instance;
    //
    [SerializeField] GameObject panel;
    [SerializeField] GameObject challengeCanvas;
    [SerializeField] GameObject descriptionPanel, pausePanel;
    [SerializeField] GameObject scoreHUD;
    [SerializeField] GameObject[] headerChallenge;
    // description
    [Header("Description")]
    [SerializeField] Image reward;
    [SerializeField] Image banner;
    [SerializeField] Image playbtn;
    [SerializeField] TextMeshProUGUI challengeName, description;
    [SerializeField] Image closeBtn;
    //top element
    [Header("Top Element")]
    [SerializeField] Image board;
    [SerializeField] TextMeshProUGUI challengeNameTopElm, hoopPassed;
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
    [SerializeField] public Sprite token;
    [SerializeField] public Sprite mysteriousBall;
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
    private void Start()
    {
        UpdateProgress();
    }
    //NewBall, 1
    //Collect, 2
    //Time, 3
    //Score,4
    //Bounce,5
    //NoAim 6
    public Sprite GetSpriteChallenge(int type)
    {
        switch (type)
        {
            case 1:
                return newBallBtn;
            case 2:
                return collectBtn;
            case 3:
                return timeBtn;
            case 4:
                return scoreBtn;
            case 5:
                return bounceBtn;
            default:
                return noAimBtn;
        }
    }
    public Color GetChallengeColor(int type)
    {
        switch (type)
        {
            case 1:
                return orange;
            case 2:
                return turquoise;
            case 3:
                return blue;
            case 4:
                return green;
            case 5:
                return pink;
            default:
                return lightRed;
        }
    }
    private int lastType;
    public void GoToChallenge(int type)
    {
        GameController.Instance.challengeMode = true;
        PlayAnim();
        panel.SetActive(false);
        descriptionPanel.SetActive(true);
        pausePanel.SetActive(false);
        challengeCanvas.SetActive(true);
        scoreHUD.SetActive(false);
        AssignChallenge(type);

        GameController.Instance.RespawnBall();
        HoopsPooler.Instance.LoadHoop();
    }
    public void SetText(int type)
    {
        challengeName.text = "Challenge " + (ChallengeManager.Instance.lastLevel.id + 1);
        challengeNameTopElm.text = "Challenge " + (ChallengeManager.Instance.lastLevel.id + 1);
        switch (type)
        {
            case 1:
                description.text = "Score " + ChallengeManager.Instance.lastLevel.totalHoops + " hoops";
                break;
            case 2:
                description.text = "Collect all tokens";
                break;
            case 3:
                description.text = "Complete in " + ((TimeLevel)ChallengeManager.Instance.lastLevel).seconds + " seconds";
                break;
            case 4:
                description.text = "Complete with score " + ((ScoreLevel)ChallengeManager.Instance.lastLevel).totolScore;
                break;
            case 5:
                description.text = "Complete with " + ((BounceLevel)ChallengeManager.Instance.lastLevel).totolBounce + " bounces";
                break;
            default:
                description.text = "Complete with no aim";
                break;
        }

    }
    public void BackToChallenge()
    {
        GameController.Instance.challengeMode = false;
        GameController.Instance.NewGame();
        HideHeader();
        panel.SetActive(true);
        pausePanel.SetActive(false);
        challengeCanvas.SetActive(false);
        scoreHUD.SetActive(true);
        GameController.Instance.RespawnBall();
    }
    public void HideHeader()
    {
        for (int i = 0; i < headerChallenge.Length; i++)
        {
            headerChallenge[i].SetActive(false);
        }
    }
    [Header("Fill")]
    [SerializeField] Image newBallFill;
    [SerializeField] Image collectFill;
    [SerializeField] Image timeFill;
    [SerializeField] Image scoreFill;
    [SerializeField] Image bounceFill;
    [SerializeField] Image noAimFill;
    [Header("Progress Text")]
    [SerializeField] TextMeshProUGUI newBallTxt;
    [SerializeField] TextMeshProUGUI collectTxt;
    [SerializeField] TextMeshProUGUI timeTxt;
    [SerializeField] TextMeshProUGUI scoreTxt;
    [SerializeField] TextMeshProUGUI bounceTxt;
    [SerializeField] TextMeshProUGUI noAimTxt;
    public void UpdateProgress()
    {
        int tmp = 0;
        for (int i = 0; i < ChallengeManager.Instance.newBallLevels.Length; i++)
        {
            if (PlayerPrefs.GetInt("NewBall" + i) == 1)
            {
                tmp += 1;
            }
        }
        newBallTxt.text = ((100 * tmp) / ChallengeManager.Instance.newBallLevels.Length) + "%";
        newBallFill.fillAmount = 1.0f * tmp / ChallengeManager.Instance.newBallLevels.Length;

        tmp = 0;
        for (int i = 0; i < ChallengeManager.Instance.collectLevels.Length; i++)
        {
            if (PlayerPrefs.GetInt("Collect" + i) == 1)
            {
                tmp += 1;
            }
        }
        collectTxt.text = ((100 * tmp) / ChallengeManager.Instance.collectLevels.Length) + "%";
        collectFill.fillAmount = 1.0f * tmp / ChallengeManager.Instance.collectLevels.Length;

        tmp = 0;
        for (int i = 0; i < ChallengeManager.Instance.timeLevels.Length; i++)
        {
            if (PlayerPrefs.GetInt("Time" + i) == 1)
            {
                tmp += 1;
            }
        }
        timeTxt.text = ((100 * tmp) / ChallengeManager.Instance.timeLevels.Length) + "%";
        timeFill.fillAmount = 1.0f * tmp / ChallengeManager.Instance.timeLevels.Length;


        tmp = 0;
        for (int i = 0; i < ChallengeManager.Instance.scoreLevels.Length; i++)
        {
            if (PlayerPrefs.GetInt("Score" + i) == 1)
            {
                tmp += 1;
            }
        }
        scoreTxt.text = ((100 * tmp) / ChallengeManager.Instance.scoreLevels.Length) + "%";
        scoreFill.fillAmount = 1.0f * tmp / ChallengeManager.Instance.scoreLevels.Length;


        tmp = 0;
        for (int i = 0; i < ChallengeManager.Instance.bounceLevels.Length; i++)
        {
            if (PlayerPrefs.GetInt("Bounce" + i) == 1)
            {
                tmp += 1;
            }
        }
        bounceTxt.text = ((100 * tmp) / ChallengeManager.Instance.bounceLevels.Length) + "%";
        bounceFill.fillAmount = 1.0f * tmp / ChallengeManager.Instance.bounceLevels.Length;

        tmp = 0;
        for (int i = 0; i < ChallengeManager.Instance.noAimLevels.Length; i++)
        {
            if (PlayerPrefs.GetInt("NoAim" + i) == 1)
            {
                tmp += 1;
            }
        }
        noAimTxt.text = ((100 * tmp) / ChallengeManager.Instance.noAimLevels.Length) + "%";
        noAimFill.fillAmount = 1.0f * tmp / ChallengeManager.Instance.noAimLevels.Length;

    }
    public void AssignChallenge(int type)
    {
        if (type == -1)
        {
            descriptionPanel.SetActive(false);
            AssignChallenge(lastType);
            return;
        }
        else
        {
            lastType = type;
        }
        headerChallenge[type - 1].SetActive(true);
        switch (type)
        {
            case 1:
                banner.sprite = newBall;
                reward.sprite = mysteriousBall;
                descriptionPause.color = orange;
                board.color = orange;
                playbtn.sprite = newBallBtn;
                restartBtn.sprite = newBallBtn;
                rewardPause.sprite = mysteriousBall;
                //name
                break;
            case 2:
                banner.sprite = collect;
                reward.sprite = token;
                descriptionPause.color = turquoise;
                playbtn.sprite = collectBtn;
                restartBtn.sprite = collectBtn;
                board.color = turquoise;
                rewardPause.sprite = token;
                break;
            case 3:
                banner.sprite = time;
                reward.sprite = token;
                descriptionPause.color = blue;
                playbtn.sprite = timeBtn;
                restartBtn.sprite = timeBtn;
                rewardPause.sprite = token;
                board.color = blue;
                break;
            case 4:
                banner.sprite = score;
                reward.sprite = token;
                descriptionPause.color = green;
                playbtn.sprite = scoreBtn;
                restartBtn.sprite = scoreBtn;
                board.color = green;
                rewardPause.sprite = token;
                break;
            case 5:
                banner.sprite = bounce;
                reward.sprite = token;
                descriptionPause.color = pink;
                playbtn.sprite = bounceBtn;
                restartBtn.sprite = bounceBtn;
                board.color = pink;
                rewardPause.sprite = token;
                break;
            default: //noAim
                banner.sprite = noAim;
                reward.sprite = token;
                descriptionPause.color = lightRed;
                playbtn.sprite = noAimBtn;
                restartBtn.sprite = noAimBtn;
                board.color = lightRed;
                rewardPause.sprite = token;
                break;
        }
        ChallengeManager.Instance.SetChallengeLevel(lastType);
        UI_Gameplay.Instance.SetUpUIChallenge(lastType);
        SetText(lastType);
    }
    public void PlayAnim()
    {
        playbtn.transform.DOScale(1.05f, 0.8f).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);
        closeBtn.transform.localScale = Vector3.zero;
        closeBtn.transform.DOScale(1, 0.5f).SetDelay(1.5f).SetEase(Ease.OutCubic);
    }
    public void StopAnim()
    {
        playbtn.transform.DOKill();
    }
}
