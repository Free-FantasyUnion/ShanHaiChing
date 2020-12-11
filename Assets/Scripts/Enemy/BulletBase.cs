using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBase : MonoBehaviour
{
    Vector3 moveDirect;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float attackValue;
    [SerializeField] private float lifeSpan = 1.5f;
    public Player player;
    private float initTime = 0;
    void AI()
    {
        ;
    }
    private void moveStrait()
    {
        this.transform.Translate(Vector3.ClampMagnitude(this.moveDirect, this.moveSpeed));
    }
    private void moveFollow()//这里也不要乱动,糊的
    {
        this.moveDirect = player.transform.GetChild(1).position - this.transform.position;
        this.transform.Translate(Vector3.ClampMagnitude(this.moveDirect, this.moveSpeed));
    }
    void BulletEffect()
    {

    }
    private void Start()
    {
        this.moveDirect = player.transform.GetChild(1).position - this.transform.position;
        this.attackValue = 5.0f;
        this.GetComponent<SpriteRenderer>().flipX = moveDirect.x > 0;
    }
    private void Update()
    {
        moveStrait();
        initTime += Time.deltaTime;
        if (initTime >= lifeSpan)
        {
            Destroy(this.gameObject);
        }
    }


    private void OnCollisionStay(Collision other)
    {
        print(other.gameObject.tag);
        if (other.gameObject.tag == "Player")
        {
            player.Hurt(attackValue);
        }
    }

}
