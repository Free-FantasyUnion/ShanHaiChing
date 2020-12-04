using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private static PlayerManager instance = new PlayerManager();
    [HideInInspector]public Transform playerTF;
    private PlayerManager()
    {

    }

    void Start()
    {
        instance.playerTF = GameObject.FindGameObjectWithTag("Player").transform;
    }


    void Update()
    {

    }
    public static PlayerManager GetInstance()
    {
        return instance;
    }



}
