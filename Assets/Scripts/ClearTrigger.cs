using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearTrigger : MonoBehaviour
{

    GameObject gameController;

    void Start()
    {
        //ゲーム開始時にGameControllerをFindにしておく
        gameController = GameObject.FindWithTag("GameController");

    }


    //トリガーからExitしたらクリアとみなす
    void OnTriggerExit2D(Collider2D other)
    {
        gameController.SendMessage("IncreaseScore");
    }

}
