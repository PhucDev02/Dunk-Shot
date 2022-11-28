using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
public class UI_Gameplay : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] TextMeshProUGUI score, stars_txt, increaseScore, bounceCnt, streakCnt;
    public static UI_Gameplay Instance;
    private void Awake()
    {
        Instance = this;
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

        streakCnt.DOFade(1, 0.1f).OnComplete(() =>
        {
            streakCnt.rectTransform.DOLocalMoveY(150, 0.4f).SetEase(Ease.InOutSine).OnComplete(() =>
             {
                 streakCnt.DOFade(0, 0.2f);
             }).SetLoops(1, LoopType.Restart);
        });
        bounceCnt.DOFade(1, 0.1f).SetDelay(0.3f).OnComplete(() =>
        {
            bounceCnt.rectTransform.DOLocalMoveY(150, 0.4f).SetEase(Ease.InOutSine).OnComplete(() =>
            {
                bounceCnt.DOFade(0, 0.2f);
            }).SetLoops(1, LoopType.Restart);

        });
        increaseScore.DOFade(1, 0.1f).SetDelay(0.6f).OnComplete(() =>
        {
            UpdateScore();
            increaseScore.rectTransform.DOLocalMoveY(130, 0.4f).SetEase(Ease.InOutSine).OnComplete(() =>
            {
                increaseScore.DOFade(0, 0.2f);
            }).SetLoops(1, LoopType.Restart);
        });
        // hien pf,bounce,diem

    }
    public void UpdateScore()
    {
        score.text = GameController.Instance.GetScore().ToString();
    }
}
