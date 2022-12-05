using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
public class UI_Controller : MonoBehaviour
{
    
    public void Reload()
    {
        DOTween.KillAll();
        SceneManager.LoadScene(0);
    }
}
