using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.UI;

public class DialogScriptHelper
{

    /// <summary>
    /// 台词列表
    /// </summary>
    private List<string> textList;
    private int curIndex;



    public void LoadScripts(TextAsset _script)
    {
        curIndex = 0;
        textList = new List<string>(_script.text.Split('\n'));
    }

    /// <summary>
    /// 读取一行台词,读到文档尾部继续读取会报错
    /// </summary>
    /// <returns></returns>
    public string ReadLine()
    {
        if (curIndex != textList.Count)
            return textList[curIndex++];
        else
            throw new System.Exception("已经读到文档结尾");
    }


    /// <summary>
    /// 测试函数
    /// </summary>
    public void PrintScripts()
    {
        int idx = 1;
        foreach (var item in textList)
        {
            Debug.Log("# "+(idx++).ToString()+" : "+item);
        }
    }
}
