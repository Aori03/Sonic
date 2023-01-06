using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SonicContoller : MonoBehaviour
{
    public float speed;
    public float jumpForce;
    public int score;
    public Text scoreText;
    public GameObject spawn;

    // private bool spacePress;
    private bool isGrounded;
    private Rigidbody2D rigidbody2d;
    private SpriteRenderer spriteRenderer;
    private Animator animator;


    private void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        scoreText.text = score.ToString();

        spawn = GameObject.FindWithTag("Respawn");
        transform.position = spawn.transform.position;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            isGrounded = false;
            //spacePress = false;
            Jump();
        }
    }

    private void FixedUpdate()
    {
        // Сохраняем координаты игрока
        Vector3 position = transform.position;

        //Добавляем к сохранённым координатам ввод с клавиатуры
        position.x += Input.GetAxis(axisName: "Horizontal") * speed;


        //Присваеваем игроку новую позицию
        transform.position = position;
        if (Input.GetAxis(axisName: "Horizontal") != 0)
        {
            if (Input.GetAxis(axisName: "Horizontal") > 0)
            {
                spriteRenderer.flipX = false;
            }
            else if (Input.GetAxis(axisName: "Horizontal") < 0)
            {
                spriteRenderer.flipX = true;
            }
            animator.SetInteger(name: "State", value: 1);
        }
        else
        {
            animator.SetInteger(name: "State", value: 0);
        }
    }

    private void Jump()
    {
        rigidbody2d.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
    }

    public void AddCoin(int count)
    {
        score += count;

        scoreText.text = score.ToString();
    }


    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Grounded"))
        {
            isGrounded = true;
            // spacePress = false;
        }
    }
}