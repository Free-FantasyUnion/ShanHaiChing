using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class BulletBase : MonoBehaviour
{
    Sprite bulletImage;
    Vector3 moveDirect;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float attackValue;
    public Player player;
    //TODO:转向
    void AI()
    {
        ;
    }
    private void moveStrait()
    {
        this.transform.Translate(Vector3.ClampMagnitude(this.moveDirect, this.moveSpeed));
    }
    private void moveFollow()
    {
        this.moveDirect = player.transform.position - this.transform.position;
        this.transform.Translate(Vector3.ClampMagnitude(this.moveDirect, this.moveSpeed));
    }
    void BulletEffect()
    {
    
    }
    private void Start()
    {
        this.moveDirect = player.transform.position - this.transform.position;
    }
    private void Update()
    {
        moveStrait();
    }
}
