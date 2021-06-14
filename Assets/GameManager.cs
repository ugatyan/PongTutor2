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



    public static Vector2 botttomLeft;// ����
    public static Vector2 topRight;// �E��

    public int id;

    // Use this for initialization 
    //����������
    //void Start()
    public void Paddleinit()
    {
        //Convert screen's pixel coordinate into game's coordinate
        //��ʂ̃s�N�Z�����W���Q�[���̍��W�ɕϊ�
        botttomLeft = Camera.main.ScreenToWorldPoint(new Vector2(0, 0));
        topRight = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width,Screen.height));
  
        paddle= MonobitEngine.MonobitNetwork.Instantiate("Paddle", Vector3.zero, Quaternion.identity, 0, null, true, true, false);

    }

    public void ballinit() {
        ball= MonobitEngine.MonobitNetwork.Instantiate("Ball", Vector3.zero, Quaternion.identity, 0, null, true, true, false);

    }
}

