using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class VictoryHoop : MonoBehaviour
{
    [SerializeField] Transform anchor, powerRing;
    [SerializeField] GameObject blikas;
    [SerializeField] SpriteRenderer top, down,net;
    [SerializeField] new GameObject light;
    public int id;
    bool isContacted = false;
    GameObject ball;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        ball = collision.gameObject;
        effect();
        isContacted = true;
        StartCoroutine(waitToExecute());
    }

    IEnumerator waitToExecute()
    {
        yield return new WaitForSeconds(0.5f);
        ChallengeManager.Instance.ExecuteCompleteChallenge();
    }
    private void Update()
    {
        if (isContacted)
        {
            ball.transform.position = anchor.position;
        }
    }
    private void effect()
    {
        top.sprite = GameManager.Instance.themes[0].topHoopDisable;
        down.sprite = GameManager.Instance.themes[0].downHoopDisable;
        net.sprite = GameManager.Instance.net;
        blikas.SetActive(false);
        powerRing.gameObject.SetActive(true);
        if (GameController.Instance.isPerfect)
        {
            powerRing.transform.DOScale(powerRing.transform.localScale * 2.6f, 0.5f).SetEase(Ease.OutCubic);
            powerRing.GetComponent<SpriteRenderer>().DOFade(0, 0.5f).OnComplete(() =>
            {
                powerRing.gameObject.SetActive(false);
            });
        }
        else
        {
            powerRing.transform.DOScale(powerRing.transform.localScale * 2.0f, 0.5f);
            powerRing.GetComponent<SpriteRenderer>().DOFade(0, 0.5f).OnComplete(() =>
            {
                powerRing.gameObject.SetActive(false);
            });
        }

        //
        light.SetActive(true);
        light.transform.transform.DORotate(Vector3.forward * 360, 4f, RotateMode.FastBeyond360).SetLoops(-1, LoopType.Restart).SetEase(Ease.Linear);
    }
}
