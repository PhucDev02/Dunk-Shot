using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
public class ScoreGameplay : MonoBehaviour
{
    public static ScoreGameplay Instance;
    public void Awake()
    {
        Instance = this;

    }

    [SerializeField] TextMeshProUGUI scoreTxt;
    public void Reset()
    {
        scoreTxt.text = GameController.Instance.GetScore() + "/" + ((ScoreLevel)ChallengeManager.Instance.lastLevel).totolScore;
    }
    public void UpdateScore()
    {
        scoreTxt.text = GameController.Instance.GetScore() + "/" + ((ScoreLevel)ChallengeManager.Instance.lastLevel).totolScore;
    }
    public bool IsCompleted()
    {
        if (GameController.Instance.GetScore() >= ((ScoreLevel)ChallengeManager.Instance.lastLevel).totolScore)
            return true;
        else return false;
    }
}
