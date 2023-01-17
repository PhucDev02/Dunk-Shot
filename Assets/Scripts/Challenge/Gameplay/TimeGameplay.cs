using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class TimeGameplay : MonoBehaviour
{
    // Start is called before the first frame update
    public static TimeGameplay Instance;
    public bool isRunning;
    private void Awake()
    {
        Instance = this;
        cooldown = 0.05f;
        this.RegisterListener(EventID.OnShoot, (param) => isRunning = true);
    }
    [SerializeField] TextMeshProUGUI seconds, tictac;
    private float timeAmount, cooldown;
    public void Reset()
    {
        isRunning = false;
        timeAmount = ((TimeLevel)ChallengeManager.Instance.lastLevel).seconds;
        seconds.text = ((int)timeAmount).ToString("00");
        tictac.text = "00";
    }
    // Update is called once per frame
    void Update()
    {
        if (isRunning == true)
        {
            if (timeAmount < 0)
            {
                timeAmount = 0;
                tictac.text = "00";
                if (!BallController.isOnAir)
                    PopupManager.Instance.ShowFailedPopup(3);
            }
            else
                timeAmount -= Time.deltaTime;
            cooldown -= Time.deltaTime;
            if (cooldown < 0 && timeAmount > 0)
            {
                seconds.text = ((int)timeAmount).ToString("00");
                tictac.text = (((int)(timeAmount * 100)) % 100).ToString("00");
                cooldown = 0.05f;
            }
        }
    }
}
