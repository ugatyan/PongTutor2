using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MonobitEngine;

public class GameManager : MonobitEngine.MonoBehaviour
{
    public Ball ball;
    public Paddle paddle;

    public static Vector2 botttomLeft;// ����
    public static Vector2 topRight;// �E��

    // Use this for initialization 
    //����������
    //void Start()
    public void Gameinit()
    {
        //Convert screen's pixel coordinate into game's coordinate
        //��ʂ̃s�N�Z�����W���Q�[���̍��W�ɕϊ�
        botttomLeft = Camera.main.ScreenToWorldPoint(new Vector2(0, 0));
        topRight = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width,Screen.height));
        /*
                //Create ball
                //�{�[�����쐬
                Instantiate(ball);

                //Create two paddle
                // 2 �̃p�h�����쐬
                Paddle paddle1 = Instantiate(paddle) as Paddle;
                Paddle paddle2 = Instantiate(paddle) as Paddle;
                paddle1.Init (true);//right paddle �E�p�h��
                paddle2.Init(false);//left paddle ���p�h��*/

        if (MonobitNetwork.isHost) {
            // ����(���N���C�A���g��ɂ�)����
            Instantiate(ball);
            Paddle paddle1 = Instantiate(paddle) as Paddle;
            paddle1.Init(true);//right paddle �E�p�h��
        }
        else {
            Paddle paddle2 = Instantiate(paddle) as Paddle;
            paddle2.Init(false);//left paddle ���p�h��*/
        }
    }
}

