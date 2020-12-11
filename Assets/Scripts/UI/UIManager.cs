using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public List<Image> BuffImages;
    public Image GenkiBar;

    void Start()
    {                                       
        Instance = this;
        BuffImages.Add(transform.Find("/PlayerInfo/Panel/Buffs/AtkUpL").GetComponent<Image>());
        BuffImages.Add(transform.Find("/PlayerInfo/Panel/Buffs/SpeedUpL").GetComponent<Image>());
        BuffImages.Add(transform.Find("/PlayerInfo/Panel/Buffs/SlowGenkiL").GetComponent<Image>());
        BuffImages.Add(transform.Find("/PlayerInfo/Panel/Buffs/HurtReduceL").GetComponent<Image>());
        GenkiBar = transform.Find("/PlayerInfo/Panel/GenkiBar").GetComponent<Image>();
        foreach (var img in BuffImages)
        {
            img.GetComponent<Image>().fillAmount = 0.0f;
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

    public void updateQiBar(float ratio)
    {
        GenkiBar.fillAmount = ratio;
    }
}
