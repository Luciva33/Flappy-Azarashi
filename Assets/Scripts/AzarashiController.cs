using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AzarashiController : MonoBehaviour
{
    Rigidbody2D rd2d;
    Animator animator;
    float angle;
    bool isDead;

    public float maxHeight;
    public float flapVelocity;
    public float relativeVelocityX; //相対的な
    public GameObject sprite;

    public bool IsDead()
    {
        return isDead;
    }

    //Awake関数はオブジェクトが生成された瞬間に呼ばれる
    //Startはオブジェクトが生成されてから初めてのフレーム前で呼ばれる
    //コンポーネントの取得をすべてのオブジェクトのStart関数より早い段階で行う
    void Awake()
    {
        rd2d = GetComponent<Rigidbody2D>();
        animator = sprite.GetComponent<Animator>();
    }

    void Update()
    {
        //最高高度に到達していない場合に限りタップ入力を受け付ける
        if (Input.GetButtonDown("Fire1") && transform.position.y < maxHeight)
        {
            Flap();
        }

        //角度を反映
        ApplyAngle();

        //angleが水平以上だったら、アニメーターのflapフラグをtrueにする
        animator.SetBool("flap", angle >= 0.0f && !isDead);
    }
    public void Flap()
    {
        //死んだら羽ばたけない
        if (isDead) return;

        //velocityを直接書き換えて上方向に加速
        rd2d.velocity = new Vector2(0.0f, flapVelocity);
        //重力が利いていないときは操作しない
        if (rd2d.isKinematic) return;

    }

    void ApplyAngle()
    {
        float targetAngle;

        //死んだらひっくり返る
        if (isDead)
        {
            targetAngle = 180.0f;
        }
        else
        {
            //現在の速度、相対速度から進んでいる角度を求める
            //Atanとはarctanjent　逆タンジェント　二辺の比から角度を教えてくれる
            //今どの向きをむけばいいか計算している
            targetAngle =
            Mathf.Atan2(rd2d.velocity.y, relativeVelocityX) * Mathf.Rad2Deg;

        }


        //回転アニメをスムージング
        //すぐにむきをかえるのではなくラープをかけてスムーズに回転
        angle = Mathf.Lerp(angle, targetAngle, Time.deltaTime * 10.0f);

        //Rotation の反映
        sprite.transform.localRotation = Quaternion.Euler(0.0f, 0.0f, angle);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (isDead) return;

        //クラッシュエフェクト
        Camera.main.SendMessage("Clash");


        //何かにぶつかったら死亡フラグ
        isDead = true;
        Debug.Log(isDead);
    }
    public void SetSteerActive(bool active)
    {
        //Rigidbodyのon of切り替え
        rd2d.isKinematic = !active;
    }
}
