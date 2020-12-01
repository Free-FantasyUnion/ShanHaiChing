using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Sprite))]
public class NPC : MonoBehaviour
{


    [SerializeField] private Sprite NPC_Sprite;
    [SerializeField] private int scriptsIndex;

    private void Start()
    {
        this.NPC_Sprite = this.GetComponent<SpriteRenderer>().sprite;
    }

    private void Update()
    {
        if (Input.GetKeyDown((KeyCode)GameManager.Key.Dialog)&&Physics2D.OverlapCircle(this.transform.position, 10f) != null)
        //TODO : 设置判定范围
        {
            NPCManager.GetInstance().ReseBonseToNPC(scriptsIndex);
        }
    }
/*
    private void OnTriggerStay2D(Collider2D collision)
    {
        
        if (Input.GetKeyDown(KeyCode.P))
            NPCManager.GetInstance().ReseBonseToNPC(scriptsIndex);
    }
*/


}
