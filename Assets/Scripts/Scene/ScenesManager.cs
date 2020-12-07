using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenesManager : MonoBehaviour
{
    public static ScenesManager _Instance;
    [SerializeField] private float borderLineSceneTwo, borderLineSceneThree;
    private bool isEmitSceneTwo = false, isEmitSceneThree = false;
    [SerializeField]
    private List<EnemyRushPosition> Scene2RushPosition,
        Scene3RushPosition;
    void Awake()
    {
        _Instance = this;
    }

    void Update()
    {
        
        if (GameManager.GetInstance().playerPos.x > borderLineSceneTwo)
        {
            if (!isEmitSceneTwo)
            {
                isEmitSceneTwo = true;
                invokeSceneTwo();
            }
        }
        if (GameManager.GetInstance().playerPos.x > borderLineSceneThree)
        {
            if (!isEmitSceneThree)
            {
                isEmitSceneThree = true;
                invokeSceneThree();
            }
        }
    } 

    void invokeSceneTwo()
    {
        foreach (var pos in Scene2RushPosition)
        {
            pos.makeEnemy();
        }
    }

    void invokeSceneThree()
    {

    }
}
