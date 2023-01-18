using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;
public class MissionBallDisplay : BallDisplay
{
    private void Start()
    {
        //progress of mission here
    }
   
    public void OnClick()
    {
        if (BallShopController.Instance.GetUnlockStatus(ball.id) == 0)
        {
            //gan vao mission popup
            this.PostEvent(EventID.OnShowPopup,ball);
        }
        else //da mo khoa
        {
            if (PlayerPrefs.GetInt("IdBallSelected") == ball.id) //chon dung theme cu
                UI_Customize.Instance.TurnOffCustomize();
            else //chon theme moi
            {
                AudioManager.Instance.Play("ShopSelect");
                PlayerPrefs.SetInt("IdBallSelected", ball.id);
                this.PostEvent(EventID.OnChangeBall);
                //do sth with ball
                Logger.Log("chon ball ");
            }
        }
    }
}
