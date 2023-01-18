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
    [SerializeField] GameObject pauseBtn, holder;
    [SerializeField] Image background;
    public static UI_Gameplay Instance;
    private float offsetTime;
    private void Awake()
    {
        Instance = this;
        this.RegisterListener(EventID.OnChangeTheme, (param) => ApplyThemeAndDarkmode());
        this.RegisterListener(EventID.OnSwitchDarkmode, (param) => ApplyThemeAndDarkmode());
        pauseBtn.SetActive(false);
        HideScore();
    }
    private void Start()
    {
        ApplyThemeAndDarkmode();
    }
    private void FixedUpdate()
    {
        holder.transform.position = HoopsPooler.Instance.GetLastHoop().position + Vector3.up * 0.5f;
    }
    void ApplyThemeAndDarkmode()
    {
        if (PlayerPrefs.GetInt("Darkmode") == 1)
        {
            background.sprite = GameManager.Instance.GetTheme().darkBackground;
            score.color = GameManager.Instance.GetTheme().scoreDarkColor;
        }
        else
        {
            background.sprite = GameManager.Instance.GetTheme().lightBackground;
            score.color = GameManager.Instance.GetTheme().scoreColor;
        }
    }
    public void ShowIncreasePoint(int point, int streak, int bounce)
    {
        if (streak == 0) streakCnt.text = "";
        else if (streak == 1)
            streakCnt.text = "Perfect!";
        else
            streakCnt.text = "Perfect x" + streak.ToString();
        if (streak == 2)
            this.PostEvent(EventID.OnPerfectx2);
        else if (streak >= 3)
        {
            this.PostEvent(EventID.OnPerfectx3);
            CinemachineShake.Instance.ShakeCamera(0.5f, 0.1f);
        }//
        EffectGameplay.Instance.PerfectEffect(streak);
        if (bounce == 0) bounceCnt.text = "";
        else if (bounce == 1) bounceCnt.text = "Bounce!";
        else
            bounceCnt.text = "Bounce x" + bounce.ToString();
        //
        if (streak != 0)
        {
            if (bounce != 0)
                offsetTime = 0;
            else offsetTime = 0.3f;
        }
        else
        {
            if (bounce == 0)
                offsetTime = 0.6f;
            else
                offsetTime = 0.3f;
        }

        increaseScore.text = "+" + point.ToString();
        streakCnt.transform.localPosition = Vector3.zero;
        bounceCnt.transform.localPosition = Vector3.zero;
        increaseScore.transform.localPosition = Vector3.zero;
        // hien pf,bounce,diem
        animPerfect();
        animBounce();
        animPoint();
    }
    private void animPoint()
    {
        UpdateScore();
        increaseScore.DOFade(0, 0).SetDelay(0.6f - offsetTime).OnComplete(() =>
          {
              increaseScore.DOFade(1, 0.2f);
              increaseScore.transform.DOLocalMoveY(holder.transform.position.y + 50, 1.3f).SetEase(Ease.InOutSine);
              increaseScore.DOFade(0, 0.2f).SetDelay(0.7f);
          });

    }
    private void animBounce()
    {
        bounceCnt.DOFade(0, 0).SetDelay(0.3f - offsetTime).OnComplete(() =>
          {
              bounceCnt.DOFade(1, 0.2f);
              bounceCnt.transform.DOLocalMoveY(holder.transform.position.y + 80, 1.3f).SetEase(Ease.InOutSine);
              bounceCnt.DOFade(0, 0.2f).SetDelay(0.65f);
          });
    }
    private void animPerfect()
    {
        streakCnt.DOFade(0, 0).SetDelay(0).OnComplete(() =>
        {
            streakCnt.DOFade(1, 0.2f);
            streakCnt.transform.DOLocalMoveY(holder.transform.position.y + 110, 1.3f).SetEase(Ease.InOutSine);
            streakCnt.DOFade(0, 0.2f).SetDelay(0.6f);
        });

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
        score.DOFade(1, 0);
        pauseBtn.GetComponent<Image>().DOFade(1, 0.4f);
        pauseBtn.SetActive(true);
    }
    public void HideScore()
    {
        score.DOFade(0, 0);
    }
    public void NewGame()
    {
        bounceCnt.gameObject.SetActive(true);
        increaseScore.gameObject.SetActive(true);
        streakCnt.gameObject.SetActive(true);
        bounceCnt.DOFade(0, 0);
        increaseScore.DOFade(0, 0);
        streakCnt.DOFade(0, 0);
        UpdateScore();
    }
    public void SetUpUIChallenge(int type)
    {
        switch (type)
        {
            case 1:
                bounceCnt.gameObject.SetActive(false);
                increaseScore.gameObject.SetActive(false);
                streakCnt.gameObject.SetActive(true);
                break;
            case 2:
                bounceCnt.gameObject.SetActive(true);
                increaseScore.gameObject.SetActive(false);
                streakCnt.gameObject.SetActive(true);
                break;
            case 3:
                bounceCnt.gameObject.SetActive(false);
                increaseScore.gameObject.SetActive(false);
                streakCnt.gameObject.SetActive(true);
                break;
            case 4:
                bounceCnt.gameObject.SetActive(true);
                increaseScore.gameObject.SetActive(true);
                streakCnt.gameObject.SetActive(true);
                break;
            case 5:
                bounceCnt.gameObject.SetActive(true);
                increaseScore.gameObject.SetActive(false);
                streakCnt.gameObject.SetActive(true);
                break;
            default:
                bounceCnt.gameObject.SetActive(false);
                increaseScore.gameObject.SetActive(false);
                streakCnt.gameObject.SetActive(true);
                break;

        }
    }
}
