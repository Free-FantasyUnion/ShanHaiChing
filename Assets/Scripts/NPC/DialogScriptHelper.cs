using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.UI;

public class DialogScriptHelper
{
    /// <summary>
    /// 剧本
    /// </summary>
    private TextAsset script;
    /// <summary>
    /// 剧本名
    /// </summary>
    public string scriptName;
    /// <summary>
    /// 台词列表
    /// </summary>
    private List<string> textList;
    private int curIndex;


    /// <summary>
    /// 创建一个剧本帮助器
    /// </summary>
    /// <param name="_script">剧本.txt文件</param>
    /// <param name="_character">剧本名称</param>
    public DialogScriptHelper(TextAsset _script, string _character)
    {
        if (this.script == null)
        {
            this.script = _script;
            this.scriptName = _character;
            curIndex = 0;
            var temp = script.text.Split('\n');
            textList = new List<string>();
            foreach (var line in temp)
            {
                textList.Add(line);
            }
        }
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
