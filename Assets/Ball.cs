using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MonobitEngine;

public class Ball : MonobitEngine.MonoBehaviour
{
    [SerializeField]
    float speed;

    float radius;
    Vector2 direction;

    // MonobitView コンポーネント
    MonobitEngine.MonobitView m_MonobitView = null;

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

    // Start is called before the first frame update
    // 最初のフレーム更新の前に Start が呼び出されます
    void Start() {
        direction = Vector2.one.normalized;// direction is (1,1) normlized
        radius = transform.localScale.x / 2;// half the width 
    }

    // Update is called once per frame
    // Update はフレームごとに 1 回呼び出されます
    void Update() {

        // オブジェクト所有権を所持しなければ実行しない
        if (!m_MonobitView.isMine) {
            return;
        }

        transform.Translate(direction * speed * Time.deltaTime);

        // Bounce 
        // 弾む
        if (transform.position.y < GameManager.botttomLeft.y + radius && direction.y < 0) {
            direction.y = -direction.y;
        }
        if (transform.position.y > GameManager.topRight.y - radius && direction.y > 0) {
            direction.y = -direction.y;
        }

        //Game over
        if (transform.position.x < GameManager.botttomLeft.x + radius && direction.x < 0) {
            Debug.Log("Right player wins!!");

            // For now, just freeze time
            // とりあえずフリーズタイム
            Time.timeScale = 0;
            enabled = false;// Stop updating script
                            // スクリプトの更新を停止
        }
        if (transform.position.x > GameManager.topRight.x - radius && direction.x > 0) {
            Debug.Log("Left player wins!!");

            // For now, just freeze time
            // とりあえずフリーズタイム
            Time.timeScale = 0;
            enabled = false;// Stop updating script
                            // スクリプトの更新を停止
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Paddle") {
            Debug.Log("当たった");
            bool isRight = other.GetComponent<Paddle>().m_MonobitView.isMine;
            Debug.Log("isRight="+isRight);

            // If hitting right paddle and moving right, flip direction
            // 右のパドルを押して右に移動すると、方向が反転します
            if (isRight == true && direction.x > 0) {
                direction.x = -direction.x;
            }
            // If hitting left paddle and moving right, flip direction
            // 左のパドルを押して右に移動すると、方向が反転します
            if (isRight == false && direction.x < 0) {
                direction.x = -direction.x;
            }



        }    
    }
}
