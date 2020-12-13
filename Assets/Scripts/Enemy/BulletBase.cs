using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBase : MonoBehaviour
{
	Vector3 moveDirect;
	[SerializeField] private float moveSpeed;
	public float attackValue;
	[SerializeField] private float lifeSpan = 1.5f;
	public Player player;
	[SerializeField] int Type = 1;
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
		if (attackValue == 0)
			this.attackValue = 5.0f;
		this.GetComponent<SpriteRenderer>().flipX = moveDirect.x > 0;
	}
	private void Update()
	{
		if (Type == 1)
			moveStrait();
		else
			moveFollow();
		initTime += Time.deltaTime;
		if (initTime >= lifeSpan)
		{
			Destroy(this.gameObject);
		}
	}


	private void OnCollisionEnter(Collision other)
	{
		Debug.Log("子弹碰撞到了我的游戏主角");
		if (other.gameObject.tag == "Player")
		{
			Debug.Log(attackValue.ToString()+"Player!!!!!!!!");
			player.Hurt(attackValue);
		}
	}

}
