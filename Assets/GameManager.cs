using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MonobitEngine;

public class GameManager : MonobitEngine.MonoBehaviour
{
    //public Ball ball;
    //public Paddle paddle;

    GameObject paddle;
    GameObject ball;



    public static Vector2 botttomLeft;// 下左
    public static Vector2 topRight;// 右上

    public int id;

    // Use this for initialization 
    //初期化処理
    //void Start()
    public void Paddleinit()
    {
        //Convert screen's pixel coordinate into game's coordinate
        //画面のピクセル座標をゲームの座標に変換
        botttomLeft = Camera.main.ScreenToWorldPoint(new Vector2(0, 0));
        topRight = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width,Screen.height));
  
        paddle= MonobitEngine.MonobitNetwork.Instantiate("Paddle", Vector3.zero, Quaternion.identity, 0, null, true, true, false);

    }

    public void ballinit() {
        ball= MonobitEngine.MonobitNetwork.Instantiate("Ball", Vector3.zero, Quaternion.identity, 0, null, true, true, false);

    }
}

