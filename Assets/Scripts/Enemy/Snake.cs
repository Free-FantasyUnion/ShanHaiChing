﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Snake : EnemyBase
{
    [SerializeField] float warningRadius;
    [SerializeField]  float attackRadius=1.5f;
    [SerializeField] float attackAngle;
    public Vector3 distance;
    private Transform JudgePoint;
    [SerializeField] private Animator anim;
    [SerializeField] private float shootingDistance;
    [SerializeField] private float bitingDistance;
    [SerializeField] private GameObject Bullet;
    [SerializeField] private float coldTime;
    float coldTimeRemain = 0;
    float facingDir = 1;
    private Image GenkiBar;

    protected void Shoot()
    {
        var tmp = Instantiate(Bullet, JudgePoint.position, JudgePoint.rotation, null).GetComponent<BulletBase>();
        tmp.player = this.player;
        tmp.attackValue = this.attackValue * 0.5f;
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
        distance = player.transform.GetChild(1).position - JudgePoint.position;

        float absDistance = distance.magnitude;
        if (absDistance >= shootingDistance)
        {
            anim.SetFloat("speed", 2);
            Vector3 temp = JudgePoint.position - player.transform.GetChild(1).position;
            /*if (temp.sqrMagnitude > 2.25f)*/
                this.transform.Translate(Vector3.ClampMagnitude(-temp, this.maxSpeed * Time.deltaTime));
            if (Mathf.Abs(temp.x) > 3f)
                facingDir = temp.x > 0 ? 1 : -1;

            this.coldTimeRemain -= Time.deltaTime;
        }
        else if (absDistance >= bitingDistance && this.coldTimeRemain <= 0)
        {
            this.Shoot();
            facingDir = distance.x > 0 ? -1 : 1;
            this.coldTimeRemain = this.coldTime;
            anim.SetFloat("speed", 0);
            anim.SetInteger("attack", 2);
        }
        else if (this.coldTimeRemain <= 0)
        {
            if (absDistance <= this.attackRadius)
            {
                anim.SetInteger("attack", 1);
                anim.SetFloat("speed", 0);
                this.coldTimeRemain = this.coldTime;
            }
            else
            {
                anim.SetFloat("speed", 2);
                this.coldTimeRemain -= Time.deltaTime;
                Vector3 temp = JudgePoint.position - player.transform.GetChild(1).position;
/*                if(temp.sqrMagnitude>2.25f)*/
                this.transform.Translate(Vector3.ClampMagnitude(-temp, this.maxSpeed * Time.deltaTime));
                if (Mathf.Abs(temp.x) > 3f)
                    facingDir = temp.x > 0.1f ? 1 : -1;
            }
        }
        else
        {
            this.coldTimeRemain -= Time.deltaTime;
        }
        this.transform.localScale = new Vector3(facingDir, 1, 1);
    }

    void Start()
    {
        anim = this.GetComponent<Animator>();
        this.coldTime = 1.0f;
        this.basicSpeed = 4.0f;
        this.shootingDistance = 15.0f;
        this.bitingDistance = 10.0f;
        SetBasicFactors();
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        }
        JudgePoint = transform.GetChild(0);
        if (Bullet == null)
            Bullet = Resources.Load<GameObject>("Prefabs/Bullet");
        GenkiBar = transform.Find("Canvas/Image").GetComponent<Image>();
    }


    void Update()
    {
        if(isAlive)
        Action();
    }

    public void SetAttackFalse()
    {
        anim.SetInteger("attack", 0);
    }



    public override void Hurt(float value)
    {
        this.yuanQi -= value * defenceRatio;
        GenkiBar.fillAmount = yuanQi / basicYuanQi;
        if (yuanQi <= 0&&isAlive)
        {
            isAlive = false;
            anim.SetTrigger("die");
        }
        UpdateAtk();
    }

}
