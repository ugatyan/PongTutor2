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

    // Start is called before the first frame update
    // �ŏ��̃t���[���X�V�̑O�� Start ���Ăяo����܂�
    void Start() {
        direction = Vector2.one.normalized;// direction is (1,1) normlized
        radius = transform.localScale.x / 2;// half the width 
    }

    // Update is called once per frame
    // Update �̓t���[�����Ƃ� 1 ��Ăяo����܂�
    void Update() {
        transform.Translate(direction * speed * Time.deltaTime);

        // Bounce 
        // �e��
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
            // �Ƃ肠�����t���[�Y�^�C��
            Time.timeScale = 0;
            enabled = false;// Stop updating script
                            // �X�N���v�g�̍X�V���~
        }
        if (transform.position.x > GameManager.topRight.x - radius && direction.x > 0) {
            Debug.Log("Left player wins!!");

            // For now, just freeze time
            // �Ƃ肠�����t���[�Y�^�C��
            Time.timeScale = 0;
            enabled = false;// Stop updating script
                            // �X�N���v�g�̍X�V���~
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Paddle") {
            bool isRight = other.GetComponent<Paddle>().isRight;

            // If hitting right paddle and moving right, flip direction
            // �E�̃p�h���������ĉE�Ɉړ�����ƁA���������]���܂�
            if (isRight == true && direction.x > 0) {
                direction.x = -direction.x;
            }
            // If hitting left paddle and moving right, flip direction
            // ���̃p�h���������ĉE�Ɉړ�����ƁA���������]���܂�
            if (isRight == false && direction.x < 0) {
                direction.x = -direction.x;
            }
        }    
    }
}
