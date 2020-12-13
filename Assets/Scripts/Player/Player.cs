using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour, ICharacter, IBuffable
{

    [Header("Basic Attributes")]
    private float basicAttackValue = 10.0f;
    private float basicYuanQi = 100.0f;
    private float basicYuanQiDrop = 0.375f;
    private float basicDefenceRatio = 0.1f;
    private float basicSpeed = 8.0f;
    private float basicColdTime = 0.35f;
    private float velocityX = 5.0f;
    private float velocityZ = 5.0f;
    private float buffTime = 5.0f;
    [Header("攻击")]
    private int faceDirection = 1;
    private float attackRadius = 10f;
    private int atkTimes = -1;
    private float 连续攻击间隔 = 0.5f;
    private Transform judgePoint = null;
    private float lastAtkTime;
    //Attributes at time
    float attackValue;
    public float yuanQi;
    float yuanQiDrop;
    float defenceRatio;
    float coldTime;
    //Move
    float maxSpeed;

    //Anime
    private Animator playerAnimator;

    //Attack
    [SerializeField] private TrailRenderer tail;
    List<Buff> buffList = null;
    private float attackRatio = 1;

    private void Awake()
    {
        Messenger.AddListener(GameEvent.ENEMY_DEATH, OnEnemyDeath);
    }

    private void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.ENEMY_DEATH, OnEnemyDeath);
    }

    private void Start()
    {
        playerAnimator = GetComponent<Animator>();
        buffList = new List<Buff>();
        lastAtkTime = 0;
        judgePoint = transform.Find("Player_Module/main/bone_1/bone_2/bone_3/bone_14/bone_15/bone_16/剑/JudgePoint");
        InitAttributes();
        yuanQi = basicYuanQi;
    }

    private void Update()
    {
        this.Hurt(yuanQiDrop*Time.deltaTime);
        foreach (var buff in this.buffList)//TODO:修
        {
            if ((Time.time - buff.startTime) >= buffTime) buffList.Remove(buff);
            buff.BuffEffect(this);
            buff.updateFill(1.0f - (Time.time - buff.startTime) / buffTime);
        }
        this.Move();
        if (Input.GetKeyDown((KeyCode)GameManager.Key.Attack))
        {
            AnimationStateChange();
        }
        this.coldTime -= Time.deltaTime;
        if (atkTimes != 0)
        {
            tail.enabled = true;
        }
        else
        {
            tail.enabled = false;
        }
        if (atkTimes != 0 && Time.timeSinceLevelLoad - lastAtkTime >= 0.75f)
        {
            attackRatio = 1;
            atkTimes = 0;
            playerAnimator.SetInteger("attack", 0);
        }
        GameManager.GetInstance().setPlayerPos(transform.position);
        UIManager.Instance.updateQiBar(this.yuanQi / this.basicYuanQi);
    }



    private void OnEnemyDeath()
    {
        int buff_max = 4;
        int rnd = Random.Range(1, buff_max);
        switch(rnd)
        {
            case 1:
                GetBuff(new Buff(Buff.BuffType.AtkUp, Time.time));
                break;
            case 2:
                GetBuff(new Buff(Buff.BuffType.SpeedUp, Time.time));
                break;
            case 3:
                GetBuff(new Buff(Buff.BuffType.SpeedDown, Time.time));
                break;
            case 4:
                GetBuff(new Buff(Buff.BuffType.YuanqiDropSlower, Time.time));
                break;
        }
    }




    #region 基本参数设置

    //IsBuffable
    public void GetBuff(Buff bf)
    {
        foreach (var e in buffList)
        {
            if (e.Equals(bf)) e.startTime = Time.time;
        }
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
        this.coldTime = 0;
    }

    #endregion

    public void Attack()
    {
        if (coldTime <= 0)
        {
            //这个print作为调试用
            //print(GameManager.AttackJudge(judgePoint, attackRadius, 60f, 8, YuanQi2Attack()));
            print(GameManager.AttackJudge(judgePoint, attackRadius, 60f,LayerMask.NameToLayer("Enemy"), YuanQi2Attack()));
            coldTime = basicColdTime;
            MusicManager.PlayMusic(MusicManager.atk[this.atkTimes-1]);
        }
    }

    private float YuanQi2Attack()
    {
        return (this.yuanQi + 1 - 1 / (yuanQi + 1)) * (attackRatio);
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
            //Debug.Log("Moving Up");
            tempZ = this.velocityZ;
        }
        else if (Input.GetKey((KeyCode)GameManager.Key.Down))
        {
            tempZ = -this.velocityZ;
        }
        if (Input.GetKey((KeyCode)GameManager.Key.Left))
        {
            //Debug.Log("Moving Left");
            if (faceDirection != -1)
            {
                this.transform.Rotate(0, 180, 0);
                faceDirection = -1;
            }
            tempX = -this.velocityX;

        }
        else if (Input.GetKey((KeyCode)GameManager.Key.Right))
        {
            //Debug.Log("Moving Right");
            if (faceDirection != 1)
            {
                this.transform.Rotate(0, 180, 0);
                faceDirection = 1;
            }
            tempX = this.velocityX;
        }
        Vector3 temp = new Vector3(tempX, 0, tempZ);
        Vector3 printTemp = Vector3.ClampMagnitude(temp, this.maxSpeed);
        printTemp.z = printTemp.z * 2.0f;
        playerAnimator.SetFloat("speed", Vector3.Magnitude(printTemp));
        printTemp = Vector3.ClampMagnitude(temp * faceDirection, this.maxSpeed * attackRatio * Time.deltaTime);
        printTemp.z = printTemp.z * 2.0f;
        //Debug.Log(this.maxSpeed);
        //每次移动都会new一个三维向量,考虑性能
        this.gameObject.transform.Translate(printTemp);
    }

    #endregion

    public void Hurt(float value)
    {
        this.yuanQi -= value * ( 1 - this.defenceRatio );
        if (this.yuanQi <= 0)
        {
            Messenger.Broadcast(GameEvent.PLAYER_DEATH);
        }
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

    public void GetYuanQi(float value)
    {
        this.yuanQi += value;
    }

}