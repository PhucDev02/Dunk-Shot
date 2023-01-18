using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
public class EffectGameplay : MonoBehaviour
{
    [SerializeField] Image flash;
    [SerializeField] TextMeshProUGUI newBest;
    public static EffectGameplay Instance;
    private void Awake()
    {
        Instance = this;
        this.RegisterListener(EventID.OnChangeBall, (param) => ApplyColor());
    }
    private void Start()
    {
        ApplyColor();
    }
    private void ApplyColor()
    {
        flash.color = GameManager.Instance.GetBallSelected().smokeColor.Evaluate(0.5f);
        flash.DOKill();
        flash.DOFade(0, 0);
    }
    public void PerfectEffect(int streak)
    {
        if (streak >= 10)
            AudioManager.Instance.Play("PerfectX10");
        if (streak >= 1 && streak <= 9)
            AudioManager.Instance.Play("PerfectX" + streak);
        if (streak == 0)
            AudioManager.Instance.Play("ScoreSimple");
        if (streak >= 3)
        {
            flash.DOKill();
            flash.DOFade(0.8f, 0);
            flash.DOFade(0, 0.2f);
            Handheld.Vibrate();
        }
    }
    public void NewBestEffect()
    {
        AudioManager.Instance.Play("NewBest");
        newBest.DOFade(1, 0.1f).OnComplete(() =>
            newBest.DOFade(0, 0.5f).SetDelay(2.0f)
        ); ;
    }
    public void Reset()
    {
        newBest.DOKill();
        flash.DOKill();
        newBest.DOFade(0, 0);
        flash.DOFade(0, 0);
    }
}
