using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : EnemyBase
{
    [SerializeField] float warningRadius;
    [SerializeField] float attackRadius;
    [SerializeField] float attackAngle;
    [SerializeField] Player player;
    private Transform JudgePoint;
    [SerializeField] private Animator anim;
    [SerializeField] private float shootingDistance;
    [SerializeField] private float bitingDistance;
    [SerializeField] private BulletBase Bullet;
    [SerializeField] private float coldTime;
    float coldTimeRemain = 0;
    protected void shoot()
    {
        Instantiate(Bullet, this.transform);
        //sleep
    }
    protected void bite()
    {
        GameManager.AttackJudge(this.transform, this.attackRadius, this.attackAngle, LayerMask.NameToLayer("enemy"), this.attackValue);
        //sleep
    }
    protected override void Action()
    {
        Vector3 distance = player.transform.position - this.transform.position;
        float absDistance = distance.magnitude;
        if (absDistance >= shootingDistance)
        {
            this.MoveToward(player.transform.position);
            this.coldTimeRemain -= Time.deltaTime;
        }
        else if (absDistance >= bitingDistance && this.coldTimeRemain <=0)
        {
            this.shoot();
            this.coldTimeRemain = this.coldTime;
        }
        else if(this.coldTimeRemain <= 0)
        {
            if(absDistance <= this.attackRadius)
            {
                print("Biting!");
                this.bite();
                this.coldTimeRemain = this.coldTime;
            }
            else
            {
                this.coldTimeRemain -= Time.deltaTime;
                this.MoveToward(player.transform.position);
            }
        }
        else
        {
            this.coldTimeRemain -= Time.deltaTime;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        anim = this.GetComponent<Animator>();
        this.coldTime = 1.0f;
        this.basicSpeed = 4.0f;
        this.shootingDistance = 15.0f;
        this.bitingDistance = 10.0f;
        InitAttributes();
    }

    // Update is called once per frame
    void Update()
    {
        Action();
    }

}
