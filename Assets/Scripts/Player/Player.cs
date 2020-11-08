using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //Atack
    float attackRatio;
    float yuanQi;
    
    //Move
    float velocityX;
    float velocityY;
    float maxSpeed;


    //Attack
    Vector2 attackPoint;
    float attackRadius;
    LayerMask LayerToAttack;

    
    List<Buff> buffList;


    private void Awake()
    {
        
    }



    float CaculateAngel(Vector3 playerFront, Vector3 enemyLine)
    {
        return Mathf.Acos(Vector3.Dot(playerFront.normalized, enemyLine.normalized)) * Mathf.Rad2Deg;
    }


    //Delegate
    void OverLapScope(float attackRange, int angle, LayerMask layer)
    {
        Collider2D[] enemyList = Physics2D.OverlapCircleAll(attackPoint, attackRadius,LayerToAttack);
/*        foreach(EnemyBase tmp in enemyList)
        {
            if(CaculateAngel(transform.right, tmp.transform.position - this.transform.position) < angle)
            {
                Attack(tmp);
            }
        }*/
    }


    void AttackRange(Weapon weapon)
    {
        
    }


    void Attack(EnemyBase enemy)
    {
        enemy.Hurt(YuanQi2Attack(yuanQi));
    }




    private float YuanQi2Attack(float YuanQi)
    {
        float attackValue = 0;
        //TODO:
        return attackValue;
    }


    #region Move
    float MoveX()
    {
        return 1f;
    }
    float MoveY()
    {
        return 1f ;
    }
    void Move()
    {
        Vector3 temp = new Vector3(MoveX(), MoveY(), 0);
        //每次移动都会new一个三维向量,考虑性能
        this.transform.Translate(Vector3.ClampMagnitude(temp,10));
    }
    // Update is called once per frame
    #endregion





}
