using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class TokenObject : MonoBehaviour
{
    private void Start()
    {
        transform.DOScale(1.1f, 0.4f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine);
        
    }
}
