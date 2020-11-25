using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;


public class Player : MonoBehaviour,ICharacter, IBuffable
{
    //Atack
    
    float attackRatio;
    float yuanQi;
    
    //Move
    float velocityX;
    float velocityY;
    float maxSpeed;

    //Anime
    private Animator playerAnimator;

    //Attack
    Vector2 attackPoint;
    float attackRadius;
    LayerMask LayerToAttack;

    //SetByRatio
    public void SetYuanqiByRatio(float value)
    {
        this.yuanQi = this.yuanQi * value;
    }

    public void GetBuff(Buff bf)
    {
        this.buffList.Add(bf);
    }
    public void SetAtkByRatio(float value)
    {
        this.attackRatio = this.attackRatio * value;
    }
    public void BurnHurt()
    {
        ;
    }
    public void Burn(float burnValue, int burnTime)
    {
        ThreadStart burnThreadRef = new ThreadStart(BurnHurt);
        Thread burnThread = new Thread(burnThreadRef);
        burnThread.Start();
    }
    List<Buff> buffList;


    private void Awake()
    {

    }
    private void Start()
    {
        playerAnimator = GetComponent<Animator>();
    }






   /* //Delegate
    void OverLapScope(float attackRange, int angle, LayerMask layer)
    {
        Collider2D[] enemyList = Physics2D.OverlapCircleAll(attackPoint, attackRadius,LayerToAttack);
*/
   /*        foreach(EnemyBase tmp in enemyList)
        {
            if(CaculateAngel(transform.right, tmp.transform.position - this.transform.position) < angle)
            {
                Attack(tmp);
            }
        }*//*
    }*/


    void AttackRange(Weapon weapon)
    {
        ;
    }

    void AttackEffect(EnemyBase enemy)
    {
        foreach(var bf in buffList){
            if (true)
            {
                ;
            }
        }
    }

    void Attack(EnemyBase enemy)
    {
        AttackEffect(enemy);
        enemy.Hurt(YuanQi2Attack(yuanQi));
    }




    private float YuanQi2Attack(float YuanQi)
    {
        float attackValue = 0;
        //TODO:
        return attackValue;
    }


    #region Move
    private float MoveX()
    {
        return Time.deltaTime * this.velocityX;
    }
    private float MoveY()
    {
        return Time.deltaTime * this.velocityY;
    }
    void Move()
    {
        playerAnimator.SetFloat("VelocityX", velocityX);
        playerAnimator.SetFloat("VelocityY", velocityY);
        float tempX, tempY;
        tempX = 0;
        tempY = 0;
        if (Input.GetKeyDown((KeyCode)GameManager.Key.Up))
        {
            tempY = this.velocityY;
        }
        else if (Input.GetKeyDown((KeyCode)GameManager.Key.Down))
        {
            tempY = -this.velocityY;
        }
        if (Input.GetKeyDown((KeyCode)GameManager.Key.Left))
        {
            tempX = -this.velocityX;
        }
        else if (Input.GetKeyDown((KeyCode)GameManager.Key.Right))
        {
            tempX = this.velocityX;
        }
        Vector3 temp = new Vector3(tempX, tempY, 0);
        //每次移动都会new一个三维向量,考虑性能
        this.transform.Translate(Vector3.ClampMagnitude(temp,10));
    }


    #endregion

    public void Hurt(float value)
    {

    }

    public void Attack(ICharacter enemy)
    {
        playerAnimator.SetBool("IsCloseAttack", true);
        //throw new System.NotImplementedException();
        playerAnimator.SetBool("IsCloseAttack", false);
    }
    // Update is called once per frame




}
