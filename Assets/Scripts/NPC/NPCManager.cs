using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Assertions.Must;

public class NPCManager : MonoBehaviour
{
    private static NPCManager instance = new NPCManager();
    [Header("UI")]
    [SerializeField] private GameObject Dialog_Text_FrameWork_Prefab;
    [SerializeField] private Text Dialog_Text;
    private GameObject Dialog_Text_FrameWork;

    [Space(10)]

    /// <summary>
    /// 剧本列表
    /// </summary>
    [SerializeField] private List<TextAsset> dialogScriptsList;
    public string curScriptName;
    public int index_Of_Scripts;//start from 0
    bool isDialogging = false;

    private DialogScriptHelper helper = new DialogScriptHelper();

    private NPCManager()
    {

    }

    public static NPCManager GetInstance()
    {
        return instance;
    }



    private void Start()
    {

        // load the scripts
        dialogScriptsList = new List<TextAsset>(Resources.LoadAll<TextAsset>(SceneManager.GetActiveScene().name));
        Dialog_Text_FrameWork_Prefab = Resources.Load<GameObject>("Prefabs/NPC/NPC_Dialog_UI");
        print("OVer");
    }


    private void ShowText()
    {
        //catch and destory the prefab
        this.Dialog_Text.text = helper.ReadLine();
    }


    public void ReseBonseToNPC(int index)
    {
        if (!isDialogging)// 第一次按下对话键
        {
            isDialogging = true;
            //生成prefab并获取text文本的Text组件
            Dialog_Text_FrameWork = Instantiate(Dialog_Text_FrameWork_Prefab);
            Dialog_Text = Dialog_Text_FrameWork.transform.Find("Text").GetComponent<Text>();
            helper.LoadScripts(dialogScriptsList[index]);
            ShowText();
        }
        else
        {
            try
            {
                ShowText();
            }
            catch
            {
                isDialogging = false;
                Destroy(Dialog_Text_FrameWork);
            }
        }

    }
}


