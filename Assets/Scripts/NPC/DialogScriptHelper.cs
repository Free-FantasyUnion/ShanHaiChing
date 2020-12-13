using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogScriptHelper
{

    /// <summary>
    /// 台词列表
    /// </summary>
    private List<string> textList;
    private int curIndex;
    public string audioName;
    public bool haveAudio = false;



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
        haveAudio = false;
        audioName = "NO";
        if (curIndex != textList.Count)
        {
            var s = textList[curIndex++];
            if (s.Contains("READ"))
            {
                audioName = s.Split(' ')[1].Trim();
                haveAudio = true;
                s = textList[curIndex++];
            }
            if (s.Contains("GOTO"))
            {
                var sceneName = s.Split(' ')[1].Trim();
                UIManager.Instance.gotoScene(sceneName);
            }
            s.Replace("&", "\n");
            return s;
        }
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
