using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseEnemy : EnemyBase
{
    [SerializeField] float warningRadius;
    [SerializeField] float attackRadius;
    [SerializeField] float attackAngle;
    [SerializeField] GameObject player;
    [SerializeField] Sprite normal;
    [SerializeField] Sprite warning;
    [SerializeField] Sprite attacking;
    private GameObject weapon;


    public override void AI()
    {
        float dist = Vector2.SqrMagnitude(player.transform.position - this.transform.position);
        if (dist < attackRadius)
        {
            //切换为攻击状态
            PathFinding();
            Attack(yuanQi);
        }

        else if (dist< warningRadius)
        {
           // this.GetComponent<SpriteRenderer>().sprite=;
            // 用动画控制图片

        }
        
    }

    public void PathFinding()
    {
        
    }



    private float YuanQi2Attack()
    {
        return this.yuanQi;
    }
    public override void Attack()
    {
        float attackValue = this.YuanQi2Attack();
        GameManager.GetInstance().AttackJudge(this.weapon.transform, attackRadius, attackAngle, LayerMask.NameToLayer("Player"), attackValue);
    }


}
