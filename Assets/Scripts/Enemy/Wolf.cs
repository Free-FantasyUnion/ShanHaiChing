using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wolf : EnemyBase
{
    [SerializeField] float warningRadius;
    [SerializeField] float attackRadius;
    [SerializeField] float attackAngle;
    [SerializeField] Player player;//TODO: 变量未赋值
    [SerializeField] float dashDistance = 0.5f;
    [SerializeField] float dashTime = 0.7f;
    [SerializeField] float dashSpeedRatio = 5.0f;
    private Transform JudgePoint;

    //temp
    float timer;
    float timer2;
    bool isDashing = false;
    bool dashingRight = false;

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
        GameManager.AttackJudge(this.transform, this.attackRadius, this.attackAngle, LayerMask.NameToLayer("Player"), this.attackValue);
    }

    protected override void Action()
    {
        float distanceX = this.transform.position.x - player.transform.position.x;
        /*        this.transform.localScale = new Vector3(distanceX >= 0 ? 1 : -1, 1, 1);*/
        // TODO: 狼的移动需要添加左右翻转的代码 用 localScale
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
    // Start is called before the first frame update
    void Start()
    {
        isDashing = false;
        this.basicSpeed = 3.0f;
        anim = this.GetComponent<Animator>();
        this.InitAttributes();
        this.dashDistance = 5.0f;
        this.dashTime = 1.5f;
        timer2 = 0;
        if(player==null)
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        Action();
        if (isDashing)
        {
            if (Time.timeSinceLevelLoad - timer2 >= 1f)
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
                }
            }
            else
            {
                
            }

        }
        else
        {
            float distanceX = this.transform.position.x - player.transform.position.x;
            this.transform.localScale = new Vector3(distanceX >= 0 ? 1 : -1, 1, 1);
        }

    }
}