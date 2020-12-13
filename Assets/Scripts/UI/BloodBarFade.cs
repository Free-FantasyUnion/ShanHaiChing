using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class BloodBarFade : MonoBehaviour
{
    Image barimage;

    private void Start()
    {
        barimage = transform.Find("Background").GetComponent<Image>();
    }

    public void showImage()
    {
        barimage.DOFade(255, 0.5f);
    }

    public void hideImage()
    {
        barimage.DOFade(0, 0.5f);
    }

}
