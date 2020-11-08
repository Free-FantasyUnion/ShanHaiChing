using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBase : MonoBehaviour
{
    [SerializeField]float yuanQi;
    [SerializeField]float yuanQiDrop;
    [SerializeField]Sprite enemyImage;
    [SerializeField] float detectRadius;
    [SerializeField] float attackRadius;
    [SerializeField] float defendRatio;

    private void SetYuanqi()
    {
        this.yuanQi = 1 * (1 + Random.Range(0,0.5f));
        this.yuanQiDrop = Mathf.Sqrt(this.yuanQi*this.yuanQi-1);
    }

    private float Defend(float value)
    {
        return value * defendRatio;
    }
    public void Hurt(float value)
    {
        this.yuanQi -= Defend(value);
    }

    /*    smjb????
     *    
     *    union{//差不多这个意思
            int attackValue;
        BUllet bulletType;
        }
    */

    /// <summary>
    /// 近战远战的敌人的寻路方式不同
    /// </summary>
    
    abstract public void AI();


    /// <summary>
    ///不同敌人有不同的攻击方式
    /// </summary>
    abstract public void Attack();
}
