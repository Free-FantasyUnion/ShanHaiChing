using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wolf : EnemyBase
{
    [SerializeField] float warningRadius;
    [SerializeField] float attackRadius;
    [SerializeField] float attackAngle;
    [SerializeField] Player player;
    [SerializeField] float dashDistance = 20.0f;
    [SerializeField] float dashTime = 1.0f;
    [SerializeField] float dashSpeedRatio = 3.0f;
    private Transform JudgePoint;

    //temp
    float timer;
    bool isDashing = false;

    [SerializeField] private Animator anim;
    //Dash Attack
    private void Dash()
    {
        float distanceX = this.transform.position.x - player.transform.position.x;
        float absDistanceX = Mathf.Abs(distanceX);
        this.transform.Translate(new Vector3(this.maxSpeed*dashSpeedRatio*Time.deltaTime*(-distanceX/absDistanceX),0,0));
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
        if(absDistanceX >= dashDistance && !isDashing)
        {
            this.MoveToward(target1);
        }
        else if(Mathf.Abs(distanceZ) >= 3.0f && !isDashing) 
        {
            this.transform.Translate(0, 0, (-distanceZ / absDistanceZ) * maxSpeed * Time.deltaTime);
        }
        else
        {
            if (!isDashing)
            {
                isDashing = true;
                timer = dashTime;
            }
            else
            {
                Dash();
                timer -= Time.deltaTime;
                if(timer <= 0)
                {
                    timer = 0;
                    isDashing = false;
                }
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        anim = this.GetComponent<Animator>();
        this.InitAttributes();
    }

    // Update is called once per frame
    void Update()
    {
        Action();
    }
}
