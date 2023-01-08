using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class NormalBallDisplay : BallDisplay
{

    [SerializeField] TextMeshProUGUI price;

    private void Start()
    {
        price.text = ball.price.ToString();
    }
    public void OnClick()
    {
        if (BallShopController.Instance.GetUnlockStatus(ball.id) == 0)
        {
            if (PlayerPrefs.GetInt("Stars") > ball.price)
            {
                Logger.Log("mua");
                PlayerPrefs.SetInt("Stars", PlayerPrefs.GetInt("Stars") - ball.price);
                unlock();
            }
            else Logger.Log("ko du tien");
        }
        else //da mo khoa
        {
            if (PlayerPrefs.GetInt("IdBallSelected") == ball.id) //chon dung theme cu
                UI_Customize.Instance.TurnOffCustomize();
            else //chon theme moi
            {
                PlayerPrefs.SetInt("IdBallSelected",ball.id);
                this.PostEvent(EventID.OnChangeBall);
                //do sth with ball
                Logger.Log("chon ball ");
            }
        }
    }
    private void unlock()
    {
        this.PostEvent(EventID.OnPurchaseItem);
        PlayerPrefs.SetInt("Ball_" + ball.id, 1);
        lockStatus.SetActive(false);
    }
}
