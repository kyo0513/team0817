using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]
[RequireComponent(typeof(Animator))] //アニメーションで追加

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator animator; //アニメーションで追加

    [SerializeField] private int moveSpeed;
    [SerializeField] private int jumpForce;

    private bool isMoving  = false;  //移動中判定
    private bool isJumping = false;  //ジャンプ中判定用
    private bool isFalling = false;  //落下中判定

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");     //移動量?

        isMoving  = horizontal != 0;                        //移動中
        isFalling = rb.velocity.y < -0.5f;                  //落下中

        //向き処理下がった場合はアニメーション反転
        if (isMoving)
        {
            Vector3 scale = gameObject.transform.localScale;
            //右向きから左向きになった時、その反対の時下記に一致する
            if(horizontal < 0 && scale.x > 0 || horizontal > 0 && scale.x < 0)
            {
                scale.x *= -1;
            }
            gameObject.transform.localScale = scale;
        }

        //スペースを押した＆落下中でなければジャンプ + ジャンプ中判定を追加
        //if (Input.GetKeyDown(KeyCode.Space) && !isJumping && !(rb.velocity.y < -0.5f))
        if (Input.GetKeyDown(KeyCode.Space) && !isJumping && !isFalling)
        {
            Jump();
        }

        //左右の歩行処理
        //rb.velocity = new Vector2(Input.GetAxis("Horizontal") * moveSpeed, rb.velocity.y);
        rb.velocity = new Vector2(horizontal * moveSpeed, rb.velocity.y); 

        //アニメーターパラメータ管理
        animator.SetBool("IsMoving",  isMoving );
        animator.SetBool("IsJumping", isJumping);
        animator.SetBool("IsFalling", isFalling);
        
    }

    void Jump()
    {
        isJumping = true;        //ジャンプ判定へ

        //ジャンプ力に力を加える
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    // 着地判定　他コライダー(stageのタグを持つものと)と接触したらジャンプ可になる
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Stage"))
        {
            isJumping = false;
        }
    }



}
