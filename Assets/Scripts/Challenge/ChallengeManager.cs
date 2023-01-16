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
    public NewBallLevel[] newBallLevels;
    public CollectLevel[] collectLevels;
    public TimeLevel[] timeLevels;
    public ScoreLevel[] scoreLevels;
    public BounceLevel[] bounceLevels;
    public NoAimLevel[] noAimLevels;

    public string path;
    public int type;
    public Level lastLevel;
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
                        lastLevel = newBallLevels[i];
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
                        lastLevel = collectLevels[i];
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
                        lastLevel = timeLevels[i];
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
                        lastLevel = scoreLevels[i];
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
                        lastLevel = bounceLevels[i];
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
                        lastLevel = noAimLevels[i];
                        break;
                    }
                }
                break;
        }
    }

    public void SetLevelComplete()
    {
        string[] tmp = path.Split('/');
        PlayerPrefs.SetInt(tmp[tmp.Length - 2] + tmp[tmp.Length - 1], 1);
    }
    public int ExecuteCompleteChallenge()
    {
        switch (type)
        {
            case 1:
                for (int i = 0; i < GameManager.Instance.balls.Length; i++)
                {
                    if (GameManager.Instance.balls[i].type == BallType.Challenge)
                    {
                        if (BallShopController.Instance.GetUnlockStatus(GameManager.Instance.balls[i].id) == 0)
                        {
                            BallShopController.Instance.UnlockBall(GameManager.Instance.balls[i].id);
                            PopupManager.Instance.ShowWinBallPopup(GameManager.Instance.balls[i]);
                            SetLevelComplete();
                            return 1;
                        }
                    }
                }
                break;
            //case 2:
            //    break;
            //case 3: //Time
            //    return 1;
            default:
                PopupManager.Instance.ShowWinTokenPopup(type);
                UI_Controller.Instance.UpdateCurrency(PlayerPrefs.GetInt("Stars"), PlayerPrefs.GetInt("Tokens") + 20);
                SetLevelComplete();
                break;
        }
        return 0;
    }
}
