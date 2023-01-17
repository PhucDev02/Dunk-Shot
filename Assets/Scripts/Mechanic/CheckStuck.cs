using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class CheckStuck : MonoBehaviour
{
    private float time = 5;
    private bool isRunning;
    [SerializeField] Image stuckBtn;
    private void Awake()
    {
        isRunning = false;
        time = 5;
        this.RegisterListener(EventID.OnShoot, (param) => isRunning = true);
        this.RegisterListener(EventID.OnContactHoop, (param) =>
        {
            isRunning = false;
            time = 5;
        });

    }
    private void Update()
    {
        if(isRunning)
        {
            time -= Time.deltaTime;
            if(time<0)
            {
                isRunning = false;
                stuckBtn.gameObject.SetActive(true);
                stuckBtn.transform.localScale = Vector3.zero;
                stuckBtn.transform.DOScale(1,0.5f).SetEase(Ease.OutBack);
            }
        }
    }
    private void Reset()
    {
        time = 5;
    }
}
