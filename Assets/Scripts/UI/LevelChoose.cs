using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelChoose : MonoBehaviour
{

    private List<Button> startButtons;

    private void Start()
    {
        startButtons = new List<Button>();
        //foreach (Transform s in transform.Find("/Canvas/Panel"))
        //{
        //    startButtons.Add(s.GetComponent<Button>());
        //}
        startButtons.Add(transform.Find("/Canvas/Panel/South").GetComponent<Button>());
        startButtons[0].onClick.AddListener(goSouth);

        // transform.Find("/Canvas/Panel/开始游戏").GetComponent<Button>().onClick.AddListener(gotoScene);
    }

    void goSouth()
    {
        MusicManager.PlayMusic(MusicManager.chooseLevel);
        PlayerPrefs.SetString("SceneTarget", "Part_1");
        DontDestroyOnLoad(transform.Find("/MusicManager"));
        SceneManager.LoadSceneAsync("LoadPause");
    }

}
