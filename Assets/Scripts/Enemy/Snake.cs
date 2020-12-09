using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : EnemyBase
{
    [SerializeField] float warningRadius;
    [SerializeField] float attackRadius;
    [SerializeField] float attackAngle;

    private Transform JudgePoint;
    [SerializeField] private Animator anim;
    [SerializeField] private float shootingDistance;
    [SerializeField] private float bitingDistance;
    [SerializeField] private GameObject Bullet;
    [SerializeField] private float coldTime;
    float coldTimeRemain = 0;
    float facingDir = 1;
    protected void Shoot()
    {
        var tmp = Instantiate(Bullet, JudgePoint.position, JudgePoint.rotation, null).GetComponent<BulletBase>();
        tmp.player = this.player;
        //tmp.speed
        //sleep
    }
    protected void Bite()
    {
        GameManager.AttackJudge(JudgePoint, this.attackRadius, this.attackAngle, LayerMask.NameToLayer("Player"), this.attackValue);

        //sleep
    }
    protected override void Action()// 这段代码不要乱动
    {
        //Vector3 distance = player.transform.GetChild(1).position - this.transform.position;
        Vector3 distance = player.transform.GetChild(1).position - JudgePoint.position;
        //TODO: 近战蛇的人间大迷惑, 位移出问题,动画没问题.
        float absDistance = distance.magnitude;
        if (absDistance >= shootingDistance)
        {
            anim.SetFloat("speed", 2);
            facingDir = this.MoveToward(player.transform.GetChild(1).position) ? 1 : -1;
            this.coldTimeRemain -= Time.deltaTime;
        }
        else if (absDistance >= bitingDistance && this.coldTimeRemain <= 0)
        {
            this.Shoot();
            this.coldTimeRemain = this.coldTime;
            anim.SetFloat("speed", 0);
        }
        else if (this.coldTimeRemain <= 0)
        {
            if (absDistance <= this.attackRadius)
            {
                anim.SetBool("attack", true);
                anim.SetFloat("speed", 0);
                this.coldTimeRemain = this.coldTime;
            }
            else
            {
                anim.SetFloat("speed", 2);
                this.coldTimeRemain -= Time.deltaTime;
                facingDir = this.MoveToward(player.transform.GetChild(1).position) ? 1 : -1;
            }
        }
        else
        {
            this.coldTimeRemain -= Time.deltaTime;
        }
        this.transform.localScale = new Vector3(facingDir, 1, 1);
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
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        }
        JudgePoint = transform.GetChild(0);
        if (Bullet == null)
            Bullet = Resources.Load<GameObject>("Prefabs/Bullet");
    }

    // Update is called once per frame
    void Update()
    {
        Action();
    }

    public void SetAttackFalse()
    {
        this.anim.SetBool("attack", false);
    }

}
