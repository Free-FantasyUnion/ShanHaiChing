using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Fishman : EnemyBase
{
    [SerializeField] float warningRadius;
    [SerializeField] Player player;
    public Vector3 distance;
    private Transform JudgePoint;
    [SerializeField] private Animator anim;
    [SerializeField] private float shootingDistance;
    [SerializeField] private float rangeDistance;
    [SerializeField] private GameObject Bullet;
    [SerializeField] private float coldTime;
    [SerializeField] private GameObject Snake;
    [SerializeField] private GameObject Wolf;
    float coldTimeRemain = 0;
    float facingDir = 1;
    [SerializeField] private Image GenkiBar;

    protected void Shoot()
    {
        var tmp = Instantiate(Bullet, JudgePoint.position, JudgePoint.rotation, null).GetComponent<BulletBase>();
        tmp.player = this.player;
        tmp.attackValue = this.attackValue * 0.3f;
        //sleep
    }

    protected override void Action()
    {
        distance = player.transform.position - this.transform.position;
        float absDistance = distance.magnitude;
        if(absDistance >= rangeDistance)
        {
            Vector3 temp = JudgePoint.position - player.transform.GetChild(1).position;
            this.transform.Translate(Vector3.ClampMagnitude(-temp, this.maxSpeed * Time.deltaTime));
            if (Mathf.Abs(temp.x) > 3f)
                facingDir = temp.x > 0 ? 1 : -1;
            this.coldTimeRemain -= Time.deltaTime;
        }
        else if (absDistance <= shootingDistance)
        {
            Vector3 temp = JudgePoint.position - player.transform.GetChild(1).position;
            this.transform.Translate(Vector3.ClampMagnitude(temp, this.maxSpeed * Time.deltaTime));
            if (Mathf.Abs(temp.x) > 3f)
                facingDir = temp.x > 0 ? 1 : -1;
            this.coldTimeRemain -= Time.deltaTime;
        }
        else if (absDistance > shootingDistance && this.coldTimeRemain <= 0)
        {
            this.Shoot();
            facingDir = distance.x > 0 ? -1 : 1;
            this.coldTimeRemain = this.coldTime;
        }
        else
        {
            this.coldTimeRemain -= Time.deltaTime;
            if(Random.Range(0,7.0f) >= 6.0f && this.coldTimeRemain <= 0)
            {
                GameObject summon;
                this.coldTimeRemain = this.coldTime;
                if (Random.Range(0,2.0f) >= 1.0f)
                {
                    summon = this.Snake;
                }
                else
                {
                    summon = this.Wolf;
                }
                Instantiate(summon, JudgePoint.transform);
            }
        }
        this.transform.localScale = new Vector3(facingDir, 1, 1);
    }
    // Start is called before the first frame update
    void Start()
    {
        //anim = this.GetComponent<Animator>();
        this.coldTime = 2.0f;
        this.basicSpeed = 3.0f;
        this.shootingDistance = 5.0f;
        this.rangeDistance = 30.0f;
        SetBasicFactors();
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        }
        JudgePoint = transform.GetChild(0);
        if (Bullet == null)
            Bullet = Resources.Load<GameObject>("Prefabs/Bullet2");
        //TODO:血条
        //GenkiBar = transform.Find("Canvas/Image").GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        GenkiBar.fillAmount = yuanQi / basicYuanQi;
        if (isAlive)
        Action();
    }

    public override void Hurt(float value)
    {
        this.yuanQi -= value * defenceRatio;
        GenkiBar.fillAmount = yuanQi / basicYuanQi;
        if (yuanQi <= 0 && isAlive)
        {
            Messenger.Broadcast(GameEvent.ENEMY_DEATH);
            isAlive = false;
            print("here");
            Destroy(this.gameObject);
        }
        UpdateAtk();
    }
}
