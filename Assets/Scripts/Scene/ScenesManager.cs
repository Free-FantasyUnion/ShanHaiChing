﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenesManager : MonoBehaviour
{
    private enum StatusType
    {
        Event, AmountElement, BOSS
    }

    // 单例
    public static ScenesManager _Instance;
    
    // 控制分界线的位置
    [SerializeField] 
    private float[] borderLineScene = new float[3];
    private bool isEmitSceneOne = false, isEmitSceneTwo = false, isEmitSceneThree = false;
    
    // 刷怪点
    [SerializeField]
    private List<EnemyRushPosition>[] SceneRushPosition = new List<EnemyRushPosition>[3];

    // 相机位置的控制
    private CameraManager camera;
    private Vector3 cameraMaxPosCurrent;
    private Vector3 cameraMinPosCurrent;
    [SerializeField]
    private float[] cameraBorderXPosS = new float[3];

    // 场景切换触发
    private int deathEnemyCount;
    // 触发类型
    [SerializeField] 
    private StatusType[] type = new StatusType[3];
    // 场景最大敌人数
    [SerializeField] 
    private int[] maxEnemyAmount = new int[3];
    // 当前所处状态
    private int currentStatus = 0;
    [SerializeField]
    private GameObject[] border = new GameObject[2];
    [SerializeField]
    private GameObject endDialog;

    void Awake()
    {
        _Instance = this;
        Messenger.AddListener(GameEvent.ENEMY_DEATH, OnEnemyDeath);
        Messenger.AddListener(GameEvent.BOSS_DEATH, OnBossDeath);
    }

    private void Start()
    {
        camera = transform.Find("/Camera").GetComponent<CameraManager>();
        camera.setMaxMoveX(cameraBorderXPosS[0]);
        SceneRushPosition[0] = new List<EnemyRushPosition>();
        SceneRushPosition[0].Add(transform.Find("/EnemyPosCH1").GetComponent<EnemyRushPosition>());
        SceneRushPosition[1] = new List<EnemyRushPosition>();
        SceneRushPosition[1].Add(transform.Find("/EnemyPosCH21").GetComponent<EnemyRushPosition>());
        SceneRushPosition[1].Add(transform.Find("/EnemyPosCH22").GetComponent<EnemyRushPosition>());
        SceneRushPosition[1].Add(transform.Find("/EnemyPosCH23").GetComponent<EnemyRushPosition>());
        SceneRushPosition[2] = new List<EnemyRushPosition>();
        SceneRushPosition[2].Add(transform.Find("/EnemyPosCH31").GetComponent<EnemyRushPosition>());
        SceneRushPosition[2].Add(transform.Find("/EnemyPosCH32").GetComponent<EnemyRushPosition>());
        SceneRushPosition[2].Add(transform.Find("/EnemyPosCH33").GetComponent<EnemyRushPosition>());
        /*foreach (var i in SceneRushPosition[1]) print(i.name);*/
    }

    private void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.ENEMY_DEATH, OnEnemyDeath);
        Messenger.RemoveListener(GameEvent.BOSS_DEATH, OnBossDeath);
    }

    void Update()
    {

        if (GameManager.GetInstance().playerPos.x > borderLineScene[0])
        {
            if (!isEmitSceneOne)
            {
                isEmitSceneOne = true;
                invokeStatus(0);
            }
        }


        if (GameManager.GetInstance().playerPos.x > borderLineScene[1])
        {
            if (!isEmitSceneTwo)
            {
                isEmitSceneTwo = true;
                invokeStatus(1);
            }
        }
        if (GameManager.GetInstance().playerPos.x > borderLineScene[2])
        {
            if (!isEmitSceneThree)
            {
                isEmitSceneThree = true;
                invokeStatus(2);
            }
        }
    } 

    void invokeStatus(int id)
    {
        print(id);
        foreach (var i in SceneRushPosition[1]) print(i.name);
        foreach (var pos in SceneRushPosition[id])
        {
            pos.makeEnemy();
        }
    }

    void OnEnemyDeath()
    {
        ++deathEnemyCount;
        if (type[currentStatus] == StatusType.AmountElement)
        {
            if (deathEnemyCount >= maxEnemyAmount[currentStatus])
            {
                gotoNextStatus();
            }
        }

    }

    public void gotoNextStatus()
    {
        print("next" + currentStatus);
        if (currentStatus == 2)
        {
            endDialog.SetActive(true);
        }
        border[currentStatus].SetActive(false);
        ++currentStatus;
        deathEnemyCount = 0;
        print("gotoNext");
        camera.setMaxMoveX(cameraBorderXPosS[currentStatus]);
    }

    public void OnBossDeath()
    {
        if (type[currentStatus] == StatusType.BOSS)
        {
            gotoNextStatus();
        }
    }


}
