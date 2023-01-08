using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallShopController : MonoBehaviour
{
    public static BallShopController Instance;
    [SerializeField] Transform normalBall, videoBall, missionBall, secretBall, challengeBall, fortuneBall;
    [SerializeField] GameObject normalBallPrf, videoBallPrf;
    [SerializeField] ContentSizeFitter fitter;
    private void Awake()
    {
        Instance = this;
        PlayerPrefs.SetInt("Ball_0", 1);

    }
    void Start()
    {
    }
    public void InitBallShop()
    {
        GameObject tmp = null;
        for (int i = 0; i < GameManager.Instance.balls.Length; i++)
        {
            if (GameManager.Instance.balls[i].type == BallType.Normal)
            {
                tmp = Instantiate(normalBallPrf, normalBall);
                tmp.GetComponent<NormalBallDisplay>().Init(GameManager.Instance.balls[i]);
            }
            if (GameManager.Instance.balls[i].type == BallType.Video)
            {
               // tmp = Instantiate(videoBallPrf, videoBall);
            }
        }
        fitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;

    }
    public int GetUnlockStatus(int id)
    {
        return PlayerPrefs.GetInt("Ball_" + id.ToString());
    }
}
