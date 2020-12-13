﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBase : MonoBehaviour, ICharacter
{
	//basic Attributes
	[SerializeField] protected float basicYuanQi;
	[SerializeField] protected float basicYuanQiDrop;
	[SerializeField] protected float basicAttackValue;
	[SerializeField] protected float basicDefenceRatio;
	[SerializeField] protected float basicSpeed;
	[SerializeField] protected float velocityX;
	[SerializeField] protected float velocityY;
	protected bool isAlive = true;
	public Player player;
	[SerializeField] protected EnemyManager.AttackType attackType;
	[SerializeField] protected EnemyManager.AIType aiType;
	[SerializeField] protected Sprite enemyImage;
	//[SerializeField] protected Buff dropBuff;
	[SerializeField] public GameObject YQObject;

	[SerializeField] protected float closeAttackRange;
	//Attributes at time
	[SerializeField] protected Player target;// default: player
	protected List<Buff> buffList;
	protected float yuanQi;
	protected float YuanQiDrop;
	protected float attackValue;
	protected float defenceRatio;
	protected float maxSpeed;
	public float yuanQiDrop;
	protected float randomFactor;


	public void SetRandom()
	{
		randomFactor = Random.Range(-0.5f, 0.5f);
	}
	protected void SetBasicFactors()
	{
		defenceRatio = (float)( defenceRatio + randomFactor * 0.1 );
		yuanQi = 500 + 200 * randomFactor;

		attackValue = -3 / 550 * ( yuanQi - 600 ) + 3;
	}

	//protected void SetYuanqi()
	//{
	//	this.yuanQi = 1 * ( 1 + Random.Range(0, 0.5f) );
	//	this.yuanQiDrop = Mathf.Sqrt(this.yuanQi * this.yuanQi - 1);
	//}

	private void OnDestroy()
	{
		
	}

	//IsBuffable
	//public void GetBuff(Buff bf)
	//{
	//	this.buffList.Add(bf);
	//}

	//public void SetDefenceByRatio(float value)
	//{
	//	this.defenceRatio = this.basicDefenceRatio * value;
	//}

	//public void SetAtkByRatio(float value)
	//{
	//	this.attackValue = this.basicAttackValue * value;
	//}

	//public void SetSpeedByRatio(float value)
	//{
	//	this.maxSpeed = this.basicSpeed * value;
	//}
	//public void SetYuanqiDropByRatio(float value)
	//{
	//	this.yuanQiDrop = this.basicYuanQiDrop * value;
	//}
   


  

	//Move
	
	protected float MoveX()
	{
		return Time.deltaTime * this.velocityX;
	}
	protected float MoveY()
	{
		return Time.deltaTime * this.velocityY;
	}
	protected bool MoveToward(Vector3 targetPosition)
	{
		Vector3 temp = this.gameObject.transform.position - targetPosition;
		this.gameObject.transform.Translate(Vector3.ClampMagnitude(-temp, this.maxSpeed * Time.deltaTime));
		return temp.x > 0;
	}
	protected void MoveAway(Vector3 targetPosition)
	{
		Vector3 temp = this.gameObject.transform.position - targetPosition;
		this.gameObject.transform.Translate(Vector3.ClampMagnitude(temp, this.maxSpeed * Time.deltaTime));
	}
	protected void DoNothing()
	{
		;//really do nothing! really!
	}



	/// <summary>
	///不同敌人有不同的攻击方式
	/// </summary>
	protected abstract void Action();

    public void DropYuanQi(float value)
    {
		//YQObject = (GameObject) Resources.Load("Prefabs/元气.prefab");
		YQObject = Resources.Load<GameObject>("Prefabs/元气.prefab");
        YuanQiObject YQscript = YQObject.GetComponent<YuanQiObject>();
        YQscript = new YuanQiObject(value);
        Instantiate(YQObject,this.transform);
    }
    public virtual void Hurt(float value)
    {
        this.yuanQi -= value * defenceRatio;
        if (yuanQi <= 0)
        {
            DropYuanQi(15.0f);
            Destroy(this.gameObject,3);
        }
    }

	public void UpdateAtk()
	{
		attackValue = -3 / 550 * ( yuanQi - 600 ) + 3;
	}
	public void DestoryThis()
	{
		print("broad");
		Messenger.Broadcast(GameEvent.ENEMY_DEATH);
		Destroy(this.gameObject, .1f);
	}
}
