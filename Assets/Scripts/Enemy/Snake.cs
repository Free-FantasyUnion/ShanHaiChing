using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : EnemyBase
{
    [SerializeField] float warningRadius;
    [SerializeField] float attackRadius;
    [SerializeField] float attackAngle;
    [SerializeField] Player player;
    private Transform JudgePoint;

    [SerializeField] private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    protected override void Action()
    {

    }
}
