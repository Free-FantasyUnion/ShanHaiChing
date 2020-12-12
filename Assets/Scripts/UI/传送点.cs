using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class 传送点 : MonoBehaviour
{
    string 下一个场景;
    string 当前场景;
    private void Update()
    {
        当前场景 = SceneManager.GetActiveScene().name;
        下一个场景 = "";
        switch (当前场景)
        {
            case "Part_1":
                下一个场景 = "Part_2";
                break;
            case "Part_2":
                下一个场景 = "Part_3";
                break;
        }
        
    }

    private void OnCollisionStay(Collision collision)
    {
        if (Input.GetKeyDown((KeyCode)GameManager.Key.Up)&&collision.gameObject.CompareTag("Player"))
            {
            PlayerPrefs.SetString("SceneTarget", 下一个场景);
            SceneManager.LoadSceneAsync("LoadPause");
        }
    }
}
