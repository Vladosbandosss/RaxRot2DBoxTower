using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxScript : MonoBehaviour
{
    private float minX = -2.2f;
    private float maxX = 2.2f;
    private bool canMove;
    private float move_Speed = 2f;
    private Rigidbody2D rb;

    private bool gameOver;
    private bool ignoreCollision;
    private bool ignorTrigger;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;
    }
    void Start()
    {
        canMove = true;

        if (Random.Range(0, 2) > 0)
        {
            move_Speed *= -1f;//в другю сторону похуярит
        }
        GamePlayController.instanse.currentBox = this;
    }

    // Update is called once per frame
    void Update()
    {
        MoveBox();
    }

    void MoveBox()
    {
        if (canMove)
        {
            Vector3 temp = transform.position;
            temp.x += move_Speed * Time.deltaTime;

            if (temp.x > maxX)
            {
                move_Speed *= -1f;
            }
            else if (temp.x < minX)
            {
                move_Speed *= -1f;
            }

            transform.position = temp;
        }
    }
   public void DropBox()
    {
        canMove = false;
        rb.gravityScale = Random.Range(2, 4);
    }

    void Landed()
    {
        if (gameOver)
        {
            return;
        }

        ignoreCollision = true;
        ignorTrigger = true;
        GamePlayController.instanse.SpawnNewBox();
        GamePlayController.instanse.MoveCamera();
    }
    void RestartGame()
    {
        GamePlayController.instanse.RestartGame();
    }

    private void OnCollisionEnter2D(Collision2D target)
    {
        Debug.Log("вошел");
        if (ignoreCollision)
        {
            return;
        }

        if (target.gameObject.tag == "Platformer")
        {
            Debug.Log("на плотформе");
            Invoke(nameof(Landed), 2f);
            ignoreCollision = true;
        }

        if (target.gameObject.tag == "Box")
        {
            Debug.Log("на коробе");
            Invoke(nameof(Landed), 2f);
            ignoreCollision = true;
        }
    }
    private void OnTriggerEnter2D(Collider2D target)
    {
        if (ignorTrigger)
        {
            return;
        }
        if (target.tag == "GameOver")
        {
            CancelInvoke(nameof(Landed));
            gameOver = true;
            ignorTrigger = true;
            Invoke(nameof(RestartGame), 3f);
        }
    }
}
