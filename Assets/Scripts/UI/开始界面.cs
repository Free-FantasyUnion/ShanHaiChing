using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 开始界面 : MonoBehaviour
{
    public GameObject 制作人员;
    public GameObject 游戏说明;


    public void Quit()
    {
        Application.Quit();
    }
    public void 打开制作人员()
    {
        制作人员.SetActive(true);
    }
    public void 关闭制作人员()
    {
        制作人员.SetActive(false);
    }
    public void 打开游戏说明()
    {
        游戏说明.SetActive(true);
    }
    public void 关闭游戏说明()
    {
        游戏说明.SetActive(false);
    }

}
