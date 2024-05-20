using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollObject : MonoBehaviour
{
    public float speed = 1.0f;
    public float startPosition;
    public float endPosition;

    void Update()
    {
        //毎フレームxポジションを少しずつ移動
        transform.Translate(-1 * speed * Time.deltaTime, 0, 0);

        //スクロールが目標ポイントまで到達したかをチェック
        if (transform.position.x <= endPosition) ScrollEnd();
    }

    void ScrollEnd()
    {
        //通り過ぎた分を加味してポジションを再設定
        float diff = transform.position.x - endPosition;
        Vector3 restartPositon = transform.position;
        restartPositon.x = startPosition + diff;
        //diffを入れないと、Update1秒間６０回の間、きっちりposition.xを超えるタイミングが同じではない
        transform.position = restartPositon;

        //同じゲームオブジェクトにアタッチされているコンポーネントにメッセージを送る
        //同一スクリプトのほかのコンポーネントにイベント通知して呼び出す
        //下はセットで使う
        SendMessage("OnScrollEnd", SendMessageOptions.DontRequireReceiver);
    }

}




