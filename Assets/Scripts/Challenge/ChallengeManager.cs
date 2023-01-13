using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ChallengeType
{
    NewBall,
    Collect,
    Time,
    Score,
    Bounce,
    NoAim
}
public class ChallengeManager : MonoBehaviour
{
    public static ChallengeManager Instance;
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        this.RegisterListener(EventID.OnCompleteChallenge, (param) => SetLevelComplete());

        for (int i = 0; i < newBallLevels.Length; i++)
            newBallLevels[i].id = i;
        for (int i = 0; i < collectLevels.Length; i++)
            collectLevels[i].id = i;
        for (int i = 0; i < timeLevels.Length; i++)
            timeLevels[i].id = i;
        for (int i = 0; i < scoreLevels.Length; i++)
            scoreLevels[i].id = i;
        for (int i = 0; i < bounceLevels.Length; i++)
            bounceLevels[i].id = i;
        for (int i = 0; i < noAimLevels.Length; i++)
            noAimLevels[i].id = i;
    }
    [Header("New Ball")]
    public NewBallLevel[] newBallLevels;
    [Header("Collect")]
    public CollectLevel[] collectLevels;
    [Header("Time")]
    public TimeLevel[] timeLevels;
    [Header("Score")]
    public ScoreLevel[] scoreLevels;
    [Header("Bounce")]
    public BounceLevel[] bounceLevels;
    [Header("No Aim")]
    public NoAimLevel[] noAimLevels;

    public string path;
    public int type;
    public void SetChallengeLevel(int type)
    {
        this.type = type;
        switch (type)
        {
            case 1:
                for (int i = 0; i < newBallLevels.Length; i++)
                {
                    if (PlayerPrefs.GetInt("NewBall" + i) == 0)
                    {
                        path = "Prefabs/Levels/NewBall/" + i;
                        break;
                    }
                }
                break;
            case 2:
                for (int i = 0; i < collectLevels.Length; i++)
                {
                    if (PlayerPrefs.GetInt("Collect" + i) == 0)
                    {
                        path = "Prefabs/Levels/Collect/" + i;
                        break;
                    }
                }
                break;
            case 3:
                for (int i = 0; i < timeLevels.Length; i++)
                {
                    if (PlayerPrefs.GetInt("Time" + i) == 0)
                    {
                        path = "Prefabs/Levels/Time/" + i;
                        break;
                    }
                }
                break;
            case 4:
                for (int i = 0; i < scoreLevels.Length; i++)
                {
                    if (PlayerPrefs.GetInt("Score" + i) == 0)
                    {
                        path = "Prefabs/Levels/Score/" + i;
                        break;
                    }
                }
                break;
            case 5:
                for (int i = 0; i < bounceLevels.Length; i++)
                {
                    if (PlayerPrefs.GetInt("Bounce" + i) == 0)
                    {
                        path = "Prefabs/Levels/Bounce/" + i;
                        break;
                    }
                }
                break;
            default:
                for (int i = 0; i < noAimLevels.Length; i++)
                {
                    if (PlayerPrefs.GetInt("NoAim" + i) == 0)
                    {
                        path = "Prefabs/Levels/NoAim/" + i;
                        break;
                    }
                }
                break;
        }
        Debug.Log(path);
    }
    public void SetLevelComplete()
    {
        string[] tmp = path.Split('/');
        PlayerPrefs.SetInt(tmp[tmp.Length - 2] + tmp[tmp.Length - 1], 1);
    }
    public void ExecuteCompleteChallenge()
    {
        //switch(type)
        //{
        //    case 1:
        //        new
        //}
    }
}
