using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    //隙間の高さの範囲
    public float minHeight;
    public float maxHeight;
    //Rootオブジェクト
    public GameObject root;

    void Start()
    {
        //開始時の隙間の高さを変更 隙間の初期化
        ChangeHeight();
    }

    // Update is called once per frame
    void ChangeHeight()
    {
        //ランダムな高さを生成して設定
        float height = Random.Range(minHeight, maxHeight);
        root.transform.localPosition = new Vector3(0.0f, height, 0.0f);
    }

    //ScrollObjectスクリプトからのメッセージを受け取って高さを変更
    //スクロール完了時の隙間の再設定
    void OnScrollEnd()
    {
        ChangeHeight();
    }

}
