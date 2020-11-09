using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NPCManager : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private GameObject Dialog_Text_FrameWork;
    [SerializeField] private Text Dialog_Text;

    [Space(10)]

    /// <summary>
    /// 剧本列表
    /// </summary>
    [SerializeField] private List<TextAsset> dialogScriptsList;
    public string curScriptName;
    public int index_Of_Scripts;//start from 0

    private DialogScriptHelper helper;




    private void Start()
    {
        SetUp(); 
    }

    private void Update()
    {
        //按下P键显示对话
        if (Input.GetKeyDown(KeyCode.P))
        {
            print(curScriptName);
            ShowText();
        }
    }

    private void SetUp()
    {

        //获取text文本的Text组件
        GameObject prefab = Instantiate(Dialog_Text_FrameWork);
        Dialog_Text = prefab.transform.Find("Text").GetComponent<Text>();

        // load the scripts
        dialogScriptsList = new List<TextAsset>(Resources.LoadAll<TextAsset>(SceneManager.GetActiveScene().name));
        

        //创建一个帮助器
        helper = new DialogScriptHelper(dialogScriptsList[index_Of_Scripts], curScriptName);

        // set up
        index_Of_Scripts = 0;
        curScriptName = dialogScriptsList[0].name;
    }

    private void ShowText()
    {
        //catch and destory the prefab
        this.Dialog_Text.text = helper.ReadLine();
    }


    private void ReseBonseToNPC(int index,string npcName)
    {
        
    }
}


