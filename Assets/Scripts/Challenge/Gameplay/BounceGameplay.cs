using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
public class BounceGameplay : MonoBehaviour
{
    public static BounceGameplay Instance;
    public void Awake()
    {
        Instance = this;
    }

    [SerializeField] TextMeshProUGUI bounceTxt;
    public void Reset()
    {
        GameplayChallengeManager.Instance.bounceCount = 0;
        bounceTxt.text = GameplayChallengeManager.Instance.bounceCount + "/" + ((BounceLevel)ChallengeManager.Instance.lastLevel).totolBounce;
    }
    public void UpdateBounce()
    {
        bounceTxt.text = GameplayChallengeManager.Instance.bounceCount + "/" + ((BounceLevel)ChallengeManager.Instance.lastLevel).totolBounce;
    }
    public bool IsCompleted()
    {
        if(GameplayChallengeManager.Instance.bounceCount >= ((BounceLevel)ChallengeManager.Instance.lastLevel).totolBounce)
                return true;
        else return false;
    }
}
