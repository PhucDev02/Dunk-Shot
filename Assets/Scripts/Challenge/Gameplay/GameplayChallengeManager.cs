using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayChallengeManager : MonoBehaviour
{
    public static GameplayChallengeManager Instance;

    public int passedHoop;
    public int bounceCount;
    private void Awake()
    {
        Instance = this;
        passedHoop = 0;
        this.RegisterListener(EventID.OnEffectHoop, (param) => { UpdateHoopPassed(); });
        this.RegisterListener(EventID.OnContactHoop, (param) => UpdateBounce());
        this.RegisterListener(EventID.OnContactHoop, (param) => UpdateScore());
    }
    public void Reset()
    {
        passedHoop = 0;
        bounceCount = 0;
        if (ChallengeManager.Instance.type == 1 || ChallengeManager.Instance.type == 6)
            NewBallGameplay.Instance.Reset();
        if (ChallengeManager.Instance.type == 5)
            BounceGameplay.Instance.Reset();
        if (ChallengeManager.Instance.type == 4)
            ScoreGameplay.Instance.Reset();
    }
    public void UpdateHoopPassed()
    {
        passedHoop++;
        UI_Challenge.Instance.UpdateHoopPassed(passedHoop);
    }
    public void UpdateBounce()
    {
        if (ChallengeManager.Instance.type == 5)
        {
            bounceCount+= GameController.Instance.bounceCnt;
            BounceGameplay.Instance.UpdateBounce();
        }
    }
    public void UpdateScore()
    {
        if (ChallengeManager.Instance.type == 4)
            ScoreGameplay.Instance.UpdateScore();
    }
}
