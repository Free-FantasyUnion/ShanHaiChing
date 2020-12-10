using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Sprite))]
public class NPC : MonoBehaviour
{

    [SerializeField] private int scriptsIndex;
    [SerializeField] private float radius = 5f;

    private void Update()
    {
        if (Input.GetKeyDown((KeyCode)GameManager.Key.Dialog))
        //TODO : 设置判定范围//貌似已经设置好了
        {
            if (Vector3.Distance(PlayerManager.GetInstance().playerTF.position, this.transform.position) <= radius)
                NPCManager.GetInstance().ReseBonseToNPC(scriptsIndex);
        }
    }
}
