using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Assertions.Must;

public class NPCManager : MonoBehaviour
{
    private static NPCManager instance;

    [Header("UI")]
    private GameObject dialogFrameWork;
    private Text Dialog_Text;


    [Space(10)]

    /// <summary>
    /// 剧本列表
    /// </summary>
    private List<TextAsset> dialogScriptsList;
    private AudioSource audio;
    private List<AudioClip> audioClips;
    bool isDialogging = false;

    private DialogScriptHelper helper;

    private NPCManager()
    {

    }

    public static NPCManager GetInstance()
    {
        return instance;
    }

    private void Awake()
    {
        instance = new NPCManager();
        instance.helper = new DialogScriptHelper();
    }

    private void Start()
    {
        // load the scripts
        instance.dialogScriptsList = new List<TextAsset>(Resources.LoadAll<TextAsset>(SceneManager.GetActiveScene().name));
        //instance.dialogFramework_Prefab = Resources.Load<GameObject>("Prefabs/NPC/NPC_Dialog_UI");
        instance.dialogFrameWork = transform.GetChild(0).gameObject;
        instance.audio = GetComponent<AudioSource>();
        instance.audioClips = new List<AudioClip>(Resources.LoadAll<AudioClip>("Audio"));
    }


    private void ShowText()
    {
        //catch and destory the prefab
        instance.Dialog_Text.text = instance.helper.ReadLine();
        if (instance.helper.haveAudio)
        { 
            
            for (int i = 0; i < instance.audioClips.Count; ++i)
            {
                if (string.Equals(instance.audioClips[i].name.Trim(), instance.helper.audioName))
                {
                    instance.audio.clip = instance.audioClips[i];
                    instance.audio.Play();
                    break;
                }
            }
        }
    }


    public void ReseBonseToNPC(int index)
    {
        if (!instance.isDialogging)// 第一次按下对话键
        {
            instance.isDialogging = true;
            //生成prefab并获取text文本的Text组件

            //instance.dialogFrameWork = Instantiate(instance.dialogFramework_Prefab);
            instance.dialogFrameWork.SetActive(true);

            instance.Dialog_Text = dialogFrameWork.transform.GetChild(0).GetChild(0).GetComponent<Text>();
            instance.helper.LoadScripts(instance.dialogScriptsList[index]);
            instance.ShowText();
        }
        else
        {
            try
            {
                instance.ShowText();
            }
            catch
            {
                instance.isDialogging = false;
                instance.dialogFrameWork.SetActive(false);
            }
        }

    }

    void readDialog(string name)
    {

    }


}


