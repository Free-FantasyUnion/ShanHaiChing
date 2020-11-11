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
        if (Input.GetKeyDown(KeyCode.P))
            NPCManager.GetInstance().ReseBonseToNPC(scriptsIndex);
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetKeyDown(KeyCode.P))
            NPCManager.GetInstance().ReseBonseToNPC(scriptsIndex);
    }



}
