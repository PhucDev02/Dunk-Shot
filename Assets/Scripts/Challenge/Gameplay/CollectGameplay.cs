using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class CollectGameplay : MonoBehaviour
{
    public static CollectGameplay Instance;
    public void Awake()
    {
        Instance = this;
    }
    [SerializeField] TextMeshProUGUI txt;

    public void Reset()
    {
        txt.text = GameplayChallengeManager.Instance.tokenCount + "/" + ((CollectLevel)ChallengeManager.Instance.lastLevel).totalTokens;
    }
    public void UpdateToken()
    {
        txt.text = GameplayChallengeManager.Instance.tokenCount + "/" + ((CollectLevel)ChallengeManager.Instance.lastLevel).totalTokens;
    }
    public bool IsCompleted()
    {
        if (GameplayChallengeManager.Instance.tokenCount >= ((CollectLevel)ChallengeManager.Instance.lastLevel).totalTokens)
            return true;
        else return false;
    }
}
