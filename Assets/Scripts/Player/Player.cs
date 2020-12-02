using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;


public class Player : MonoBehaviour, ICharacter, IBuffable
{

    //Basic Attributes
    [SerializeField] private float basicAttackValue = 10.0f;
    [SerializeField] private float basicYuanQi = 100.0f;
    [SerializeField] private float basicYuanQiDrop = 1.0f;
    [SerializeField] private float basicDefenceRatio = 0.1f;
    [SerializeField] private float basicSpeed = 1.0f;
    [SerializeField] private float velocityX = 5.0f;
    [SerializeField] private float velocityZ = 5.0f;
    //Attributes at time
    float attackValue;
    float yuanQi;
    float yuanQiDrop;
    float defenceRatio;
    //Move
    float maxSpeed;

    //Anime
    private Animator playerAnimator;

    //Attack
    Vector2 attackPoint;
    float attackRadius;
    LayerMask LayerToAttack;
    List<Buff> buffList;
    
    //IsBuffable
    public void GetBuff(Buff bf)
    {
        this.buffList.Add(bf);
    }
    public void SetDefenceByRatio(float value)
    {
        this.defenceRatio = this.basicDefenceRatio * value;
    }

    public void SetAtkByRatio(float value)
    {
        this.attackValue = this.basicAttackValue * value;
    }

    public void SetSpeedByRatio(float value)
    {
        this.maxSpeed = this.basicSpeed * value;
    }
    public void SetYuanqiDropByRatio(float value)
    {
        this.yuanQiDrop = this.basicYuanQiDrop * value;
    }
    public void InitAttributes()
    {
        SetAtkByRatio(1.0f);
        SetDefenceByRatio(1.0f);
        SetSpeedByRatio(1.0f);
        SetYuanqiDropByRatio(1.0f);
    }




    /* //Delegate
     void OverLapScope(float attackRange, int angle, LayerMask layer)
     {
         Collider2D[] enemyList = Physics2D.OverlapCircleAll(attackPoint, attackRadius,LayerToAttack);
 */
    /*        foreach(EnemyBase tmp in enemyList)
         {
             if(CaculateAngel(transform.right, tmp.transform.position - this.transform.position) < angle)
             {
                 Attack(tmp);
             }
         }*//*
     }*/


    void AttackRange(Weapon weapon)
    {
        ;
    }
    public void Attack()
    {
        ;
    }
    void AttackEnemy(EnemyBase enemy)
    {
        playerAnimator.SetBool("IsCloseAttack", true);
        //throw new System.NotImplementedException();
        playerAnimator.SetBool("IsCloseAttack", false);
        enemy.Hurt(YuanQi2Attack(yuanQi));
    }




    private float YuanQi2Attack(float YuanQi)
    {
        float attackValue = 0;
        //TODO:
        return attackValue;
    }


    #region Move
    public void Move()
    {
/*        playerAnimator.SetFloat("VelocityX", velocityX);
        playerAnimator.SetFloat("VelocityZ", velocityZ);*/

        float tempX, tempZ;
        tempX = 0;
        tempZ = 0;
        if (Input.GetKey((KeyCode)GameManager.Key.Up))
        {
            Debug.Log("Moving Up");
            tempZ = this.velocityZ;
        }
        else if (Input.GetKey((KeyCode)GameManager.Key.Down))
        {
            Debug.Log("Moving Down");
            tempZ = -this.velocityZ;
        }
        if (Input.GetKey((KeyCode)GameManager.Key.Left))
        {
            Debug.Log("Moving Left");
            tempX = -this.velocityX;
        }
        else if (Input.GetKey((KeyCode)GameManager.Key.Right))
        {
            Debug.Log("Moving Right");
            tempX = this.velocityX;
        }
        Vector3 temp = new Vector3(tempX, 0, tempZ);
        Vector3 printTemp = Vector3.ClampMagnitude(temp, this.maxSpeed);
        playerAnimator.SetFloat("speed", Vector3.Magnitude(printTemp));
        //Debug.Log(this.maxSpeed);
        //每次移动都会new一个三维向量,考虑性能
        this.gameObject.transform.Translate(Vector3.ClampMagnitude(temp,this.maxSpeed*Time.deltaTime));
    }
   
    #endregion

    public void Hurt(float value)
        {
            this.yuanQi -= value * (1-this.defenceRatio);
        }
    

    // Update is called once per frame
    private void Awake()
    {

    }
    private void Start()
    {
        playerAnimator = GetComponent<Animator>();
        buffList = new List<Buff>();
    }
    private void Update()
    {
        InitAttributes();
        foreach(var buff in this.buffList)
        {
            buff.BuffEffect(this);
        }
        this.Move();
    }



}
