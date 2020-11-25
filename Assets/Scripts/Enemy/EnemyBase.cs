using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBase : MonoBehaviour,ICharacter
{
    [SerializeField] protected float yuanQi;
    [SerializeField] protected float yuanQiDrop;
    [SerializeField] protected Sprite enemyImage;
    [SerializeField] protected float defendRatio;
    [SerializeField] protected float velocity;
    [SerializeField] protected EnemyManager.AttackType attackType;
    [SerializeField] protected EnemyManager.AIType aiType;

    private void SetYuanqi()
    {
        this.yuanQi = 1 * (1 + Random.Range(0,0.5f));
        this.yuanQiDrop = Mathf.Sqrt(this.yuanQi*this.yuanQi-1);
    }

    private float MoveX()
    {
        return Time.deltaTime * this.velocity;
    }
    private float MoveY()
    {
        return Time.deltaTime * this.velocity;
    }
    private void AIMove()
    {
        switch (this.aiType)
        {
            case EnemyManager.AIType.Aggressive:
                ;
                break;
            case EnemyManager.AIType.Stand:
                ;
                break;
            case EnemyManager.AIType.Guard:
                ;
                break;
            default:
                break;
        }
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
    public abstract void Attack();

    public void Hurt(float value)
    {
        this.yuanQi -= value*defendRatio;
    }
    public void Burn(float burnValue, int burnTime)
    {
        ;
    }
}
