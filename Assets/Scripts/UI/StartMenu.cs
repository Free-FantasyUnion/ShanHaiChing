using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMenu : MonoBehaviour
{

    private List<Button> startButtons;

    private void Start()
    {
        startButtons = new List<Button>();
        foreach(Transform s in transform.Find("/Canvas/Panel"))
        {
            startButtons.Add(s.GetComponent<Button>());
        }
        startButtons[0].onClick.AddListener(gotoScene);

        // transform.Find("/Canvas/Panel/开始游戏").GetComponent<Button>().onClick.AddListener(gotoScene);
    }

    
    public void gotoScene()
    {
        PlayerPrefs.SetString("SceneTarget", "LevelChoose");
        SceneManager.LoadSceneAsync("LoadPause");
    }
}
