using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Bouncer : MonoBehaviour
{

    private void OnCollisionEnter2D(Collision2D collision)
    {
        transform.DOScale(0.8f, 0.05f).SetEase(Ease.OutCubic).OnComplete(()=> {
            transform.DOScale(1f, 0.05f).SetEase(Ease.OutBack);
        });
    }
}
