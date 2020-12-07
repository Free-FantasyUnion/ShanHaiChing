using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wolf : EnemyBase
{
    [SerializeField] float warningRadius;
    [SerializeField] float attackRadius;
    [SerializeField] float attackAngle;
    [SerializeField] Player player;
    [SerializeField] float dashDistance = 0.5f;
    [SerializeField] float dashTime = 0.7f;
    [SerializeField] float dashSpeedRatio = 5.0f;
    private Transform JudgePoint;

    //temp
    float timer;
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
        this.transform.Translate(new Vector3(this.maxSpeed*dashSpeedRatio*Time.deltaTime*dashDirection,0,0));
        GameManager.AttackJudge(this.transform, this.attackRadius, this.attackAngle, LayerMask.NameToLayer("Enemy"), this.attackValue);
    }

    protected override void Action()
    {
        float distanceX = this.transform.position.x - player.transform.position.x;
        float absDistanceX = Mathf.Abs(distanceX);
        float distanceZ = this.transform.position.z - player.transform.position.z;
        float absDistanceZ = Mathf.Abs(distanceZ);
        Vector3 target0 = player.transform.position;
        Vector3 target1 = target0 + new Vector3(dashDistance*(0.66f)*(distanceX/absDistanceX),0,0);
        if (absDistanceX >= dashDistance && !isDashing)
        {
            this.MoveToward(target1);
        }
        else if (Mathf.Abs(distanceZ) >= 3.0f && !isDashing)
        {
            this.transform.Translate(0, 0, (-distanceZ / absDistanceZ) * this.maxSpeed * Time.deltaTime);
        }
        else if(!isDashing)
        {
            isDashing = true;
            timer = dashTime;
            if(distanceX/absDistanceX <= 0)
            {
                dashingRight = true;
            }
            else
            {
                dashingRight = false;
            }
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
    }

    // Update is called once per frame
    void Update()
    {
        Action();
        if (isDashing)
        {
            Dash();
            //print(timer);
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                timer = 0;
                isDashing = false;
                dashingRight = false;
            }
        }
    }
}