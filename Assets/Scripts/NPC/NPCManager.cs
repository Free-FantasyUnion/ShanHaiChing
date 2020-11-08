using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCManager : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private GameObject Dialog_Text_FrameWork;
    [SerializeField] private Text Dialog_Text;


    /// <summary>
    /// 剧本列表
    /// </summary>
    [SerializeField] private List<TextAsset> dialogScriptsList;
    public string curScriptName;
    public int index_Of_Scripts;//start from 0

    private DialogScriptHelper helper;

    //test
    public TextAsset test;
    public DialogScriptHelper dialog_Test;

    
    private void Start()
    {
        SetUp();


        //test
        dialog_Test = new DialogScriptHelper(test, "Test");
    }

    private void Update()
    {
        //按下P键显示对话
        if (Input.GetKeyDown(KeyCode.P))
        {
            print(curScriptName);

            ShowText();
           /* //test
            string tmp = dialog_Test.ReadLine();
            print(tmp);//catch a error and destory the class;*/
        }
    }

    private void SetUp()
    {

        //获取text文本的Text组件
        GameObject prefab = Instantiate(Dialog_Text_FrameWork);
        Dialog_Text = prefab.transform.Find("Text").GetComponent<Text>();
        dialogScriptsList = new List<TextAsset>();
        dialogScriptsList.Add(test);
        //
        index_Of_Scripts = 0;
        curScriptName = dialogScriptsList[0].name;
        //创建一个帮助器
        helper = new DialogScriptHelper(dialogScriptsList[index_Of_Scripts], curScriptName);
        


    }

    private void ShowText()
    {
        this.Dialog_Text.text = helper.ReadLine();
    }

}


