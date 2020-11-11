using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class GameManager : MonoBehaviour
{
	private static GameManager instance;



    private void Awake()
    {
		instance = new GameManager();
	}
    private GameManager()
	{
	
	}
	// Start is called before the first frame update
	void Start()
	{
	}
	
	// Update is called once per frame
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.H))
		{
            AttackJudge();
		}
	}

    public void AttackJudge()
    {
		
    }

	float CaculateAngel(Vector3 playerFront, Vector3 enemyLine)
	{
		return Mathf.Acos(Vector3.Dot(playerFront.normalized, enemyLine.normalized)) * Mathf.Rad2Deg;
	}














	public static GameManager GetInstance()
	{
		return instance;
	}



	enum Key
	{
		Attack=KeyCode.J,
		Up = KeyCode.W,
		Down = KeyCode.S,
		Left=KeyCode.A,
		Right=KeyCode.D,
		// 快速移动未确定
		QuickMove = KeyCode.LeftShift,
	}

}


// 这里是全局的攻击判定函数
