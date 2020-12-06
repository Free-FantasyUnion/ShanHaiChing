using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;


public class Player : MonoBehaviour, ICharacter, IBuffable
{

    [Header("Basic Attributes")]
    [SerializeField] private float basicAttackValue = 10.0f;
    [SerializeField] private float basicYuanQi = 100.0f;
    [SerializeField] private float basicYuanQiDrop = 1.0f;
    [SerializeField] private float basicDefenceRatio = 0.1f;
    [SerializeField] private float basicSpeed = 1.0f;
    [SerializeField] private float velocityX = 5.0f;
    [SerializeField] private float velocityZ = 5.0f;
    [Header("攻击")]
    [SerializeField] private int faceDirection = 1;
    private float attackRadius = 1.71f;
    [SerializeField] private int atkTimes = -1;
    [SerializeField] private float 连续攻击间隔 = 0.5f;
    private Transform judgePoint = null;
    private float lastAtkTime;
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
    LayerMask LayerToAttack;
    List<Buff> buffList = null;


    private void Start()
    {
        playerAnimator = GetComponent<Animator>();
        buffList = new List<Buff>();
        lastAtkTime = 0;
        judgePoint = transform.Find("Player_Module/main/bone_1/bone_2/bone_3/bone_14/bone_15/bone_16/剑/JudgePoint");
        InitAttributes();
    }

    private void Update()
    {
        
        /*        foreach (var buff in this.buffList)
                {
                    buff.BuffEffect(this);
                }*/
        this.Move();
        if (Input.GetKeyDown((KeyCode)GameManager.Key.Attack))
        {
            AnimationStateChange();
        }
        if (atkTimes!=0&&Time.timeSinceLevelLoad - lastAtkTime >= 0.75f)
        {
            atkTimes = 0;
            playerAnimator.SetInteger("attack", 0);
        }
    }




    #region 基本参数设置

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

    #endregion

    public void Attack()
    {

        GameManager.AttackJudge(judgePoint, attackRadius, 60f, LayerMask.NameToLayer("Enemy"), YuanQi2Attack(yuanQi));

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
            if (faceDirection != -1)
            {
                this.transform.localScale = new Vector3(-1, 1, 1);
                faceDirection = -1;
            }
            tempX = -this.velocityX;

        }
        else if (Input.GetKey((KeyCode)GameManager.Key.Right))
        {
            Debug.Log("Moving Right");
            if (faceDirection != 1)
            {
                this.transform.localScale = new Vector3(1, 1, 1);
                faceDirection = 1;
            }
            tempX = this.velocityX;
        }
        Vector3 temp = new Vector3(tempX, 0, tempZ);
        Vector3 printTemp = Vector3.ClampMagnitude(temp, this.maxSpeed);
        playerAnimator.SetFloat("speed", Vector3.Magnitude(printTemp));
        //Debug.Log(this.maxSpeed);
        //每次移动都会new一个三维向量,考虑性能
        this.gameObject.transform.Translate(Vector3.ClampMagnitude(temp, this.maxSpeed * Time.deltaTime));
    }

    #endregion

    public void Hurt(float value)
    {
        this.yuanQi -= value * ( 1 - this.defenceRatio );
    }





    private void SetAttackFalse()
    {
        atkTimes = 0;
        playerAnimator.SetInteger("attack", 0);
    }

    private void AnimationStateChange()
    {
        if (Time.timeSinceLevelLoad - lastAtkTime >= 连续攻击间隔)
        {
            lastAtkTime = Time.timeSinceLevelLoad;
            atkTimes += atkTimes == 3 ? -2 : 1;
            // 0 non
            // 1 atk_1
            // 2 atk_2
            // 3 atk_3
            playerAnimator.SetInteger("attack", atkTimes);
        }

    }



}