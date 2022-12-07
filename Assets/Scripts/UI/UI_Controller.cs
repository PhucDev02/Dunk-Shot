using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;
public class UI_Controller : MonoBehaviour
{

    [SerializeField] Transform flashTransition;
    [SerializeField] float a, timeFade;
    private void Awake()
    {
    }
    private void Start()
    {
        flashTransition.GetComponent<Image>().DOFade(a, 0);
        flashTransition.GetComponent<Image>().DOFade(0, timeFade);
    }
    public void Reload()
    {
        flashTransition.GetComponent<Image>().DOFade(a, timeFade).OnComplete(() =>
        {
            DOTween.KillAll();
            SceneManager.LoadScene(0);
        });
    }
}
