using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class CheckStuck : MonoBehaviour
{
    public float time = 7;
    private bool isRunning;
    [SerializeField] Image stuckBtn;
    private void Awake()
    {
        isRunning = false;
        time = 7;
        this.RegisterListener(EventID.OnShoot, (param) => isRunning = true);
        this.RegisterListener(EventID.OnContactHoop, (param) => Reset());
        this.RegisterListener(EventID.OnGameOver, (param) => Reset());

    }
    private void Update()
    {
        if (isRunning)
        {
            time -= Time.deltaTime;
            if (time < 0)
            {
                Reset();
                stuckBtn.gameObject.SetActive(true);
                stuckBtn.transform.localScale = Vector3.zero;
                stuckBtn.transform.DOScale(1, 0.5f).SetEase(Ease.OutBack);
            }
        }
    }
    private void Reset()
    {
        isRunning = false;
        time = 7;
    }
}
