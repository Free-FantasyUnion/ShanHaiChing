using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBase : MonoBehaviour,ICharacter,IBuffable
{
    //basic Attributes
    [SerializeField] protected float basicYuanQi;
    [SerializeField] protected float basicYuanQiDrop;
    [SerializeField] protected float basicAttackValue;
    [SerializeField] protected float basicDefenceRatio;
    [SerializeField] protected float basicSpeed;
    [SerializeField] protected float velocityX;
    [SerializeField] protected float velocityY;

    [SerializeField] protected EnemyManager.AttackType attackType;
    [SerializeField] protected EnemyManager.AIType aiType;
    [SerializeField] protected Sprite enemyImage;
    [SerializeField] protected Buff dropBuff;
    //Attributes at time
    [SerializeField] protected Player target;// default: player
    protected List<Buff> buffList;
    protected float yuanQi;
    protected float YuanQiDrop;
    protected float attackValue;
    protected float defenceRatio;
    protected float maxSpeed;
    public float yuanQiDrop;

    //IsBuffable
    public void GetBuff(Buff bf)
    {
        this.buffList.Add(bf);
    }
    public void SetDefenceByRatio(float value)
    {
        this.defenceRatio = this.basicDefenceRatio * value;
    }

    public void SetAtkByRatio(float value)
    {
        this.attackValue = this.basicAttackValue * value;
    }

    public void SetSpeedByRatio(float value)
    {
        this.maxSpeed = this.basicSpeed * value;
    }
    public void SetYuanqiDropByRatio(float value)
    {
        this.yuanQiDrop = this.basicYuanQiDrop * value;
    }
    

    
    protected void SetYuanqi()
    {
        this.yuanQi = 1 * (1 + Random.Range(0,0.5f));
        this.yuanQiDrop = Mathf.Sqrt(this.yuanQi*this.yuanQi-1);
    }

    //Move
    protected float MoveX()
    {
        return Time.deltaTime * this.velocityX;
    }
    protected float MoveY()
    {
        return Time.deltaTime * this.velocityY;
    }
    protected void AggressiveMove()
    {
        Vector3 temp = this.target.gameObject.transform.position - this.gameObject.transform.position; 
        this.gameObject.transform.Translate(Vector3.ClampMagnitude(temp, this.maxSpeed * Time.deltaTime));
    }
    protected void DoNothing()
    {
        ;//really do nothing! really!
    }
    protected void GuardMove()
    {
        ;//undefined
    }
    protected void ShootingMove()
    {
        ;
    }
    protected void AIMove()
    {
        switch (this.aiType)
        {
            case EnemyManager.AIType.Aggressive:
                AggressiveMove();
                break;
            case EnemyManager.AIType.Stand:
                DoNothing();
                break;
            case EnemyManager.AIType.Guard:
                GuardMove();
                break;
            case EnemyManager.AIType.Shooting:
                ;
                break;
            default:
                break;
        }
    }


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
        this.yuanQi -= value*defenceRatio;
    }
    public void Burn(float burnValue, int burnTime)
    {
        ;
    }
}
