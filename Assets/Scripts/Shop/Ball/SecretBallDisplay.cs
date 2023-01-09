using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class SecretBallDisplay : BallDisplay
{
    [SerializeField] public TextMeshProUGUI index;
    private void Start()
    {
        //progress of mission here
        index.text = (ball.id - 16).ToString();
    }
    public void OnClick()
    {
        if (BallShopController.Instance.GetUnlockStatus(ball.id) == 0)
        {
            //gan vao mission popup
            this.PostEvent(EventID.OnShowPopup, ball);
        }
        else //da mo khoa
        {
            if (PlayerPrefs.GetInt("IdBallSelected") == ball.id) //chon dung theme cu
                UI_Customize.Instance.TurnOffCustomize();
            else //chon theme moi
            {
                PlayerPrefs.SetInt("IdBallSelected", ball.id);
                this.PostEvent(EventID.OnChangeBall);
                //do sth with ball
                Logger.Log("chon ball ");
            }
        }
    }
}
