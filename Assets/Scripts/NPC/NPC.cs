using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Sprite))]
public class NPC : MonoBehaviour
{


    [SerializeField] private Sprite NPC_Sprite;
    [SerializeField] private int scriptsIndex;
    [SerializeField] private float radius=5f;

    private void Start()
    {
        this.NPC_Sprite = this.GetComponent<SpriteRenderer>().sprite;
    }

    private void Update()
    {
        if (Input.GetKeyDown((KeyCode)GameManager.Key.Dialog))
        //TODO : 设置判定范围
        {
            if(Vector3.Distance(PlayerManager.GetInstance().playerTF.position,this.transform.position)<=radius)
            NPCManager.GetInstance().ReseBonseToNPC(scriptsIndex);
        }
    }

/*    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.transform.position, radius);
            
    }   */
    /*
        private void OnTriggerStay2D(Collider2D collision)
        {

            if (Input.GetKeyDown(KeyCode.P))
                NPCManager.GetInstance().ReseBonseToNPC(scriptsIndex);
        }
    */


}
