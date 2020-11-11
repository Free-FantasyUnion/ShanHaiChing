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

    private GameObject dialogFramework_Prefab;
    private GameObject dialogFrameWork;
    private Text Dialog_Text;


    [Space(10)]

    /// <summary>
    /// 剧本列表
    /// </summary>
    private List<TextAsset> dialogScriptsList;
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
        instance.dialogFramework_Prefab = Resources.Load<GameObject>("Prefabs/NPC/NPC_Dialog_UI");
    }


    private void ShowText()
    {
        //catch and destory the prefab
        instance.Dialog_Text.text = helper.ReadLine();
    }


    public void ReseBonseToNPC(int index)
    {
        if (!instance.isDialogging)// 第一次按下对话键
        {
            instance.isDialogging = true;
            //生成prefab并获取text文本的Text组件

            instance.dialogFrameWork = Instantiate(instance.dialogFramework_Prefab);
            instance.Dialog_Text = dialogFrameWork.transform.Find("Text").GetComponent<Text>();
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
                Destroy(instance.dialogFrameWork);
            }
        }

    }
}


