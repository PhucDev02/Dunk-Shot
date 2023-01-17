using UnityEngine;
using UnityEngine.UI;

public class BallShopController : MonoBehaviour
{
    public static BallShopController Instance;
    [SerializeField] Transform normalBall, videoBall, missionBall, secretBall, challengeBall, fortuneBall;
    [SerializeField] GameObject normalBallPrf, videoBallPrf,missionBallPrf,challengeBallPrf,fortuneBallPrf,secretBallPrf;
    [SerializeField] GameObject panel;
    private void Awake()
    {
        Instance = this;
        PlayerPrefs.SetInt("Ball_0", 1);
    }
    public void InitBallShop()
    {
        panel.SetActive(true);
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
                tmp = Instantiate(videoBallPrf, videoBall);
                tmp.GetComponent<VideoBallDisplay>().Init(GameManager.Instance.balls[i]);
            }
            if (GameManager.Instance.balls[i].type == BallType.Mission)
            {
                tmp = Instantiate(missionBallPrf, missionBall);
                tmp.GetComponent<MissionBallDisplay>().Init(GameManager.Instance.balls[i]);
            }
            if (GameManager.Instance.balls[i].type == BallType.Challenge)
            {
                tmp = Instantiate(challengeBallPrf, challengeBall);
                tmp.GetComponent<ChallengeBallDisplay>().Init(GameManager.Instance.balls[i]);
            }
            if (GameManager.Instance.balls[i].type == BallType.Secret)
            {
                tmp = Instantiate(secretBallPrf, secretBall);
                tmp.GetComponent<SecretBallDisplay>().Init(GameManager.Instance.balls[i]);
            }
            if (GameManager.Instance.balls[i].type == BallType.Fortune)
            {
                tmp = Instantiate(fortuneBallPrf, fortuneBall);
                tmp.GetComponent<FortuneBallDisplay>().Init(GameManager.Instance.balls[i]);
            }
        }
        panel.SetActive(false);
    }
    public int GetUnlockStatus(int id)
    {
        return PlayerPrefs.GetInt("Ball_" + id.ToString());
    }
    public void UnlockBall(int id)
    {
        PlayerPrefs.SetInt("Ball_"+id, 1);
        for (int i = 0; i < missionBall.transform.childCount; i++)
        {
           missionBall.GetChild(i).GetComponent<MissionBallDisplay>().Init();
        }
    }
}
