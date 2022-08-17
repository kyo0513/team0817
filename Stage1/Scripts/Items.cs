using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))] 
public class Items : MonoBehaviour
{
    Rigidbody2D rb;
    Animator animator;
    int jumpForce  = 7;   //やり方がわからなかったので直に指定してます現在のプレイヤーキャラのジャンプ力を入れています
    
    // Start is called before the first frame update
    void Start()
    {
        animator=GetComponent<Animator>();

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("ハンバーガー踏まれる");
        //触れてきた対象（つまりネズミ）のRigidbody・animatorを取得します
        Rigidbody2D player_rb       = other.gameObject.GetComponent<Rigidbody2D>();
        Animator    player_animator = other.gameObject.GetComponent<Animator>();

        //ハンバーガーのアニメーション用のトリガーを付与します
        animator.SetTrigger("IsAction");

        //プレイヤーのジャンプと同じ処理ですが1.2倍?飛ぶように設定しています
        player_rb.AddForce(Vector2.up * jumpForce * 1.2f, ForceMode2D.Impulse);

        //プレイヤーの見た目の変化のさせ方が不明；以下だとダメでした； ※新しいトリガーを追加することで解決
        //player_animator.SetBool("IsJumping", true);
        //player_animator.SetBool("IsFalling", false);

        player_animator.SetTrigger("IsBownd");            
        
    }

}
