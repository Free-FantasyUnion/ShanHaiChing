using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    //ObjectPool<EnemyBase> enemys;
    //ObjectPool<BulletBase> bullets;
    //
    //
    public enum AttackType
    {
        Far,
        Close
    };
    public enum AIType
    {
        Aggressive,
        Stand,
        Guard
    }

    private static EnemyManager instance = new EnemyManager();

    private EnemyManager(){}

    List<EnemyBase> enemyPool;
    public int PoolSize;

    private void Start()
    {
        instance.enemyPool = new List<EnemyBase>();
    }

    private void Update()
    {
        
    }
    





}
