using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public List<Image> BuffImages;
    public Image GenkiBar;
    public GameObject pausePannel;
    public GameObject deathPannel;
    public GameObject statusPannel;

    void Start()
    {
        //pauseCanvas = transform.Find("Undefined")；
        Instance = this;
        BuffImages.Add(transform.Find("/PlayerInfo/StatusPanel/Buffs/AtkUpL").GetComponent<Image>());
        BuffImages.Add(transform.Find("/PlayerInfo/StatusPanel/Buffs/SpeedUpL").GetComponent<Image>());
        BuffImages.Add(transform.Find("/PlayerInfo/StatusPanel/Buffs/SlowGenkiL").GetComponent<Image>());
        BuffImages.Add(transform.Find("/PlayerInfo/StatusPanel/Buffs/HurtReduceL").GetComponent<Image>());
        GenkiBar = transform.Find("/PlayerInfo/GenkiBar").GetComponent<Image>();
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

    bool pausing = false;
    public void Pause()
    {
        if (pausing)
        {
            pausing = false;
            pausePannel.SetActive(false);
            Time.timeScale = 1;
            statusPannel.SetActive(true);
        }
        else
        { 
            pausing = true;
            pausePannel.SetActive(true);
            Time.timeScale = 0;
            statusPannel.SetActive(false);
        }
    }
    public void Awake()
    {
        Messenger.AddListener(GameEvent.PLAYER_DEATH, OnPlayerDeath);
    }

    public void OnPlayerDeath()
    {
        Time.timeScale = 0;
        statusPannel.SetActive(false);
        deathPannel.SetActive(true);
    }
    public void Update()
    {
        //GenkiBar.fillAmount = GameManager.GetInstance().player.GetCurrentYuanQiRatio();
        if (Input.GetKeyDown((KeyCode)GameManager.Key.Pause))
        {
            Pause();
            //otherwise Instantiate(pausePannel, this.Transform);
        }
    }
    public void updateQiBar(float ratio)
    {
        GenkiBar.fillAmount = ratio;
    }

    public void gotoScene(string name)
    {
        Time.timeScale = 1.0f;
        PlayerPrefs.SetString("SceneTarget", "Part_1");
        SceneManager.LoadSceneAsync("LoadPause");
    }

}
