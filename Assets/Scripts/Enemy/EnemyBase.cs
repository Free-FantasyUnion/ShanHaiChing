using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBase : MonoBehaviour,ICharacter
{
    [SerializeField] protected float yuanQi;
    [SerializeField] protected float yuanQiDrop;
    [SerializeField] protected Sprite enemyImage;
    [SerializeField] protected float defendRatio;

    private void SetYuanqi()
    {
        this.yuanQi = 1 * (1 + Random.Range(0,0.5f));
        this.yuanQiDrop = Mathf.Sqrt(this.yuanQi*this.yuanQi-1);
    }

    private float Defend(float value)
    {
        return value * defendRatio;
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
        this.yuanQi -= Defend(value);
    }
}
