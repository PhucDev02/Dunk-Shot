using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class MissionPopup : Popup
{
    [SerializeField] Image fill;
    public override void AssignPopup(Ball ball)
    {
        base.AssignPopup(ball);
        //fill
        preview.sprite = ball.spriteBall;
    }
}
