using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Ball ball;
    public Paddle paddle;

    public static Vector2 botttomLeft;
    public static Vector2 topRight;

    // Use this for initialization 
    void Start()
    {
        //Convert screen's pixel coordinate into game's coordinate
        botttomLeft = Camera.main.ScreenToWorldPoint(new Vector2(0, 0));
        topRight = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width,Screen.height));

        //Create ball
        Instantiate(ball);

        //Create two paddle
        Paddle paddle1 = Instantiate(paddle) as Paddle;
        Paddle paddle2 = Instantiate(paddle) as Paddle;
        paddle1.Init (true);//right paddle
        paddle2.Init(false);//left paddle
    }

}

