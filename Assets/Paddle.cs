using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MonobitEngine;

public class Paddle : MonobitEngine.MonoBehaviour
{
    // MonobitView コンポーネント
    public MonobitEngine.MonobitView m_MonobitView = null;

    [SerializeField]
    float speed;

    float height;

    string input;
    public bool isRight;

    void Awake() {
        // すべての親オブジェクトに対して MonobitView コンポーネントを検索する
        if (GetComponentInParent<MonobitEngine.MonobitView>() != null) {
            m_MonobitView = GetComponentInParent<MonobitEngine.MonobitView>();
        }
        // 親オブジェクトに存在しない場合、すべての子オブジェクトに対して MonobitView コンポーネントを検索する
        else if (GetComponentInChildren<MonobitEngine.MonobitView>() != null) {
            m_MonobitView = GetComponentInChildren<MonobitEngine.MonobitView>();
        }
        // 親子オブジェクトに存在しない場合、自身のオブジェクトに対して MonobitView コンポーネントを検索して設定する
        else {
            m_MonobitView = GetComponent<MonobitEngine.MonobitView>();
        }
    }


    // Use this for initialization
    //初期化処理
    void Start()
    {
        height = transform.localScale.y;

        Vector2 pos = Vector2.zero;

        if (MonobitNetwork.isHost) {
            pos = new Vector2(GameManager.topRight.x, 0);
            pos -= Vector2.right * transform.localScale.x;// Move a bit to the leht

            isRight = true;

            input = "PaddleRight";
        }
        else {
            pos = new Vector2(GameManager.botttomLeft.x, 0);
            pos += Vector2.right * transform.localScale.x;// Move a bit to the right

            isRight = false;

            input = "PaddleLeft";
        }

        transform.position = pos;

        transform.name = input;
    }

    /*public void Init(bool isRightPaddle, int ID)
    {
        isRight = isRightPaddle;

        Vector2 pos = Vector2.zero;

        if (isRightPaddle)
        {
            // Place paddle on the right of screen
            // 画面右側にパドルを配置
            pos = new Vector2(GameManager.topRight.x, 0);
            pos -= Vector2.right * transform.localScale.x;// Move a bit to the leht

            input = "PaddleRight";
        }
        else
        {

            monobitView.TransferOwnership(ID);
            // Place paddle on the left of screen
            // 画面左側にパドルを配置
            pos = new Vector2(GameManager.botttomLeft.x, 0);
            pos += Vector2.right * transform.localScale.x;// Move a bit to the right

            input = "PaddleLeft";
        }

        //Update this paddle's position
        // このパドルの位置を更新
        transform.position = pos;

        transform.name = input;
    }*/

    // Update is called once per frame
    // Update はフレームごとに 1 回呼び出されます
    void Update() {

        // オブジェクト所有権を所持しなければ実行しない
        if (!m_MonobitView.isMine) {
            return;
        }

        // Now let's move the paddle!
        // パドルを動かす!

        //GetAxis is a number between -1 to 1(-1 for down, 1 for up)
        //GetAxis は -1 から 1 までの数値です (下は -1、上は 1)
        float move = Input.GetAxis(input) * Time.deltaTime * speed;

            // Restrict paddle movement
            // パドルの動きを制限する

            // If paddle is too low and user is continuing to move down, stop
            // パドルが低すぎてユーザーが下に移動し続けている場合、停止します
            if (transform.position.y < GameManager.botttomLeft.y + height / 2 && move < 0) {
                move = 0;
            }
            // If paddle is too high and user is continuing to move up, stop
            // パドルが高すぎてユーザーが上に移動し続けている場合、停止します
            if (transform.position.y > GameManager.topRight.y - height / 2 && move > 0) {
                move = 0;
            }

            transform.Translate(move * Vector2.up);
       
    }
}

