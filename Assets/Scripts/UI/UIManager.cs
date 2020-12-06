using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public List<Image> BuffImages;

    void Start()
    {
        Instance = this;
        foreach (var img in BuffImages)
        {
            img.GetComponent<Image>().fillAmount = 1.0f;
        }
    }

    public void setUIRatio(Buff.BuffType buff, float amount)
    {
        switch(buff)
        {
            case Buff.BuffType.AtkUp:
                BuffImages[0].GetComponent<Image>().fillAmount = amount;
                break;
            case Buff.BuffType.SpeedDown:
                BuffImages[1].GetComponent<Image>().fillAmount = amount;
                break;
            case Buff.BuffType.SpeedUp:
                BuffImages[2].GetComponent<Image>().fillAmount = amount;
                break;
            case Buff.BuffType.YuanqiDropSlower:
                BuffImages[3].GetComponent<Image>().fillAmount = amount;
                break;
        }
    }

    
}
