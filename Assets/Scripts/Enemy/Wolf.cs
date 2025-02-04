﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//TODO: 狼的脚本里面的 元气条需要检查一下
public class Wolf : EnemyBase
{
    [SerializeField] float warningRadius;
    [SerializeField] float attackRadius;
    [SerializeField] float attackAngle;

    [SerializeField] float dashDistance = 0.5f;
    [SerializeField] float dashTime = 0.7f;
    [SerializeField] float dashSpeedRatio = 5.0f;
    private Transform JudgePoint;
    private bool dashed = false;
    //temp
    float timer;
    float timer2;
    public bool isInterrupted;
    bool isDashing = false;
    bool dashingRight = false;
    private Image GenkiBar;

    [SerializeField] private Animator anim;
    //Dash Attack
    private void Dash()
    {
        int dashDirection = -1;
        if (dashingRight)
        {
            dashDirection = 1;
        }
        this.transform.Translate(new Vector3(this.maxSpeed * dashSpeedRatio * Time.deltaTime * dashDirection, 0, 0));
        if (Vector3.SqrMagnitude(player.transform.position - this.JudgePoint.transform.position) < 1f&&!dashed)
        {
            GameManager.AttackJudge(JudgePoint, this.attackRadius, this.attackAngle, LayerMask.NameToLayer("Player"), this.attackValue);
            dashed = true;
        }
    }

    protected override void Action()
    {

        float distanceX = this.transform.position.x - player.transform.position.x;
        float absDistanceX = Mathf.Abs(distanceX);
        float distanceZ = this.transform.position.z - player.transform.position.z;
        float absDistanceZ = Mathf.Abs(distanceZ);

        Vector3 target0 = player.transform.position;
        Vector3 target1 = target0 + new Vector3(dashDistance * ( 0.66f ) * ( distanceX / absDistanceX ), 0, 0);
        if (absDistanceX >= dashDistance && !isDashing)
        {
            this.MoveToward(target1);
            anim.SetInteger("state", 1);
        }
        else if (Mathf.Abs(distanceZ) >= 3.0f && !isDashing)
        {
            this.transform.Translate(0, 0, ( -distanceZ / absDistanceZ ) * this.maxSpeed * Time.deltaTime);
            anim.SetInteger("state", 1);
        }
        else if (!isDashing)
        {
            isDashing = true;
            timer = dashTime;
            if (distanceX / absDistanceX <= 0)
            {
                dashingRight = true;
            }
            else
            {
                dashingRight = false;
            }
            anim.SetInteger("state", 2);
        }

    }
    void Start()
    {
        isInterrupted = false;
        isDashing = false;
        this.basicSpeed = 3.0f;
        this.basicYuanQi = 75.0f;
        this.yuanQi = 75.0f;
        anim = this.GetComponent<Animator>();
        this.SetBasicFactors();
        this.dashDistance = 5.0f;
        this.dashTime = 1.5f;
        this.attackRadius = 1.5f;
        timer2 = 0;
        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        JudgePoint = transform.GetChild(0);

        GenkiBar = transform.Find("Canvas/Image").GetComponent<Image>();

    }

    void Update()//TODO: 狼的冲撞会因为dashed而判定不到
    {
        if (isAlive)
        {
            Action();
            if (isDashing)
            {
                if (isInterrupted )
                {
                    isInterrupted = false;
                    isDashing = false;
                    dashed = false;

                    //TODO:调一下动画
                    anim.SetInteger("state", 1);
                    timer2 = Time.timeSinceLevelLoad;
                }
                if (Time.timeSinceLevelLoad - timer2 >= 1.75f)
                {
                    Dash();
                    //print(timer);
                    timer -= Time.deltaTime;
                    if (timer <= 0)
                    {
                        timer = 0;
                        timer2 = Time.timeSinceLevelLoad;
                        isDashing = false;
                        dashingRight = false;
                        dashed = false;
                    }
                }
                else
                {
                    if (Vector3.SqrMagnitude(player.transform.position - this.JudgePoint.transform.position) < 1f && !dashed)
                    {
                        GameManager.AttackJudge(JudgePoint, this.attackRadius, this.attackAngle, LayerMask.NameToLayer("Player"), this.attackValue);
                        dashed = true;
                    }
                }

            }
            else
            {
                float distanceX = this.transform.position.x - player.transform.position.x;
                this.transform.localScale = new Vector3(distanceX >= 0 ? 1 : -1, 1, 1);
            }
        }


    }

    public override void Hurt(float value)
    {
        this.yuanQi -= value * defenceRatio;
        GenkiBar.fillAmount = yuanQi / basicYuanQi;
        if (yuanQi <= 0 && isAlive)
        {
            // Messenger.Broadcast(GameEvent.ENEMY_DEATH);
            isAlive = false;
            anim.SetTrigger("die");
            DestoryThis(3);
        }
        UpdateAtk();
    }
}