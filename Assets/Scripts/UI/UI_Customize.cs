using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Customize : MonoBehaviour
{
    [SerializeField] Image theme, ball;
    [SerializeField] Color gray,orange;
    private void Start()
    {
        ChooseBall();
    }
    public void ChooseBall()
    {
        ball.color = orange;
        theme.color = gray;
    }
    public void ChooseTheme()
    {
        ball.color = gray;
        theme.color = orange;
    }
}
