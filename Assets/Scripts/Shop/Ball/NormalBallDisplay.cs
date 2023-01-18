using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class NormalBallDisplay : BallDisplay
{

    [SerializeField] TextMeshProUGUI price;
    [SerializeField] Image priceTag;
    private void Start()
    {
        price.text = ball.price.ToString();
        if (int.Parse(price.text) > 150)
            priceTag.sprite = SpriteHolder.Instance.purpleTag;
        else
            priceTag.sprite = SpriteHolder.Instance.orangeTag;
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
                AudioManager.Instance.Play("ShopSelect");
                PlayerPrefs.SetInt("IdBallSelected", ball.id);
                this.PostEvent(EventID.OnChangeBall);
                //do sth with ball
                Logger.Log("chon ball ");
            }
        }
    }
}
