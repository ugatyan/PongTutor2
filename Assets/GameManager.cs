using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MonobitEngine;

public class GameManager : MonobitEngine.MonoBehaviour
{
    public Ball ball;
    public Paddle paddle;

    public static Vector2 botttomLeft;// 下左
    public static Vector2 topRight;// 右上

    // Use this for initialization 
    //初期化処理
    //void Start()
    public void Gameinit()
    {
        //Convert screen's pixel coordinate into game's coordinate
        //画面のピクセル座標をゲームの座標に変換
        botttomLeft = Camera.main.ScreenToWorldPoint(new Vector2(0, 0));
        topRight = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width,Screen.height));
        /*
                //Create ball
                //ボールを作成
                Instantiate(ball);

                //Create two paddle
                // 2 つのパドルを作成
                Paddle paddle1 = Instantiate(paddle) as Paddle;
                Paddle paddle2 = Instantiate(paddle) as Paddle;
                paddle1.Init (true);//right paddle 右パドル
                paddle2.Init(false);//left paddle 左パドル*/

        if (MonobitNetwork.isHost) {
            // 球を(他クライアント上にも)生成
            Instantiate(ball);
            Paddle paddle1 = Instantiate(paddle) as Paddle;
            paddle1.Init(true);//right paddle 右パドル
        }
        else {
            Paddle paddle2 = Instantiate(paddle) as Paddle;
            paddle2.Init(false);//left paddle 左パドル*/
        }
    }
}

