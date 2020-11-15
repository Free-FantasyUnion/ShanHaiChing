using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseEnemy : EnemyBase
{
    [SerializeField] float warningRadius;
    [SerializeField] float attackRadius;
    [SerializeField] float attackAngle;
    [SerializeField] GameObject player;
/*    [SerializeField] Sprite normal;
    [SerializeField] Sprite warning;
    [SerializeField] Sprite attacking;*/
    private GameObject weapon;

    [SerializeField] private Animator anim;


    private void Start()
    {
        anim = this.GetComponent<Animator>();

    }


    public override void AI()
    {
        float dist = Vector2.SqrMagnitude(player.transform.position - this.transform.position);
        if (dist > attackRadius)
        {
            //切换为攻击状态
            Attack();

        }
        else if (dist < warningRadius)
        {


            PathFinding();
            // this.GetComponent<SpriteRenderer>().sprite=;
            // 用动画控制图片

        }
        else
        {
            
        }
        
    }

    public void PathFinding()
    {
        //位移向player
        this.transform.Translate(Vector3.Lerp(this.transform.position, player.transform.position, 0.5f));
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
