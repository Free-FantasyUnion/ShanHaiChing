using System.Collections;
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
    private float borderLineSceneTwo, borderLineSceneThree;
    private bool isEmitSceneTwo = false, isEmitSceneThree = false;
    
    // 刷怪点
    [SerializeField]
    private List<EnemyRushPosition> Scene2RushPosition,
        Scene3RushPosition;

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
    }

    private void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.ENEMY_DEATH, OnEnemyDeath);
        Messenger.RemoveListener(GameEvent.BOSS_DEATH, OnBossDeath);
    }

    void Update()
    {
        
        if (GameManager.GetInstance().playerPos.x > borderLineSceneTwo)
        {
            if (!isEmitSceneTwo)
            {
                isEmitSceneTwo = true;
                invokeStatusTwo();
            }
        }
        if (GameManager.GetInstance().playerPos.x > borderLineSceneThree)
        {
            if (!isEmitSceneThree)
            {
                isEmitSceneThree = true;
                invokeStatusThree();
            }
        }
    } 

    void invokeStatusTwo()
    {
        foreach (var pos in Scene2RushPosition)
        {
            pos.makeEnemy();
        }
    }

    void invokeStatusThree()
    {

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
        if (currentStatus == 2)
        {

        }
        ++currentStatus;
        deathEnemyCount = 0;
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
