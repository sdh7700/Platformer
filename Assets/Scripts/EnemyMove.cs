using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
  public int nextMove; // 행동지표를 결정하기

  Rigidbody2D rigid;
  Animator anim;
  SpriteRenderer spriteRenderer;


  void Awake()
  {
    rigid = GetComponent<Rigidbody2D>();
    anim = GetComponent<Animator>();
    spriteRenderer = GetComponent<SpriteRenderer>();
    Invoke("Think", 5);
  }

  void Update()
  {

  }

  void FixedUpdate()
  {
    // Move
    rigid.velocity = new Vector2(nextMove, rigid.velocity.y);

    // Platform Check
    Vector2 frontVec = new Vector2(rigid.position.x + nextMove * 0.4f, rigid.position.y);
    Debug.DrawRay(frontVec, Vector3.down, new Color(0, 1, 0));
    RaycastHit2D rayHit = Physics2D.Raycast(frontVec, Vector3.down, 1, LayerMask.GetMask("Platform"));
    if (rayHit.collider == null)
      Turn();
  }

  void Think() // nextMove를 바꿔주는 로직
  {
    // Set Next Active
    nextMove = Random.Range(-1, 2);
    // Sprite Animation
    anim.SetInteger("walkSpeed", nextMove);
    // Flip Sprite
    if (nextMove != 0)
      spriteRenderer.flipX = nextMove == 1;
    // Recursive
    float nextThinkTime = Random.Range(2f, 5f);
    Invoke("Think", nextThinkTime);
  }

  void Turn()
  {
    nextMove *= -1;
    spriteRenderer.flipX = nextMove == 1;
    CancelInvoke();
    Invoke("Think", 2);
  }
}
