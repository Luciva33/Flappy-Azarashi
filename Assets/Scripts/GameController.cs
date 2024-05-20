using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameController : MonoBehaviour
{
    //ゲームステート    enumは列挙定数
    enum State
    {
        Ready,
        Play,
        GameOver
    }

    State state;
    int score;

    public AzarashiController azarashi;
    public GameObject blocks;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI stateText;

    void Start()
    {
        //開始と同時にReadeyステートに移行
        Ready();
    }

    void LateUpdate()
    {
        //ゲームのステートごとにイベント監視
        switch (state)
        {
            case State.Ready:
                //タッチしたらゲームスタート
                if (Input.GetButtonDown("Fire1")) GameStart();
                break;
            case State.Play:
                //キャラクターが死亡したらゲームオーバー
                if (azarashi.IsDead()) GameOver();
                break;
            case State.GameOver:
                //タッチしたらシーンをリロード
                if (Input.GetButtonDown("Fire1")) Reload();
                break;
        }
    }

    void Ready()
    {
        state = State.Ready;

        //各オブジェクトを無効状態にする
        azarashi.SetSteerActive(false);
        blocks.SetActive(false);

        //ラベルこうしん
        scoreText.text = "Score : " + 0;

        stateText.gameObject.SetActive(true);
        stateText.text = "Ready";


    }

    void GameStart()
    {
        state = State.Play;

        //各オブジェクトを有効にする
        azarashi.SetSteerActive(true);
        blocks.SetActive(true);

        //最初の入力だけゲームコントローラーから渡す
        azarashi.Flap();

        stateText.gameObject.SetActive(false);
        stateText.text = "";

    }

    void GameOver()
    {
        state = State.GameOver;

        //シーン中のすべてのScrollObjectコンポーネントを探し出す
        ScrollObject[] scrollObjects = FindObjectsOfType<ScrollObject>();

        //全scrollobjectのスクロール処理を無効にする
        //拡張for文
        foreach (ScrollObject so in scrollObjects) so.enabled = false;

        stateText.gameObject.SetActive(true);
        stateText.text = "GameOver";


    }

    void Reload()
    {
        //現在読み込んでいるシーンを再読み込み
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }

    public void IncreaseScore()
    {
        score++;
        scoreText.text = "Score :" + score;
    }
}