using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubyController : MonoBehaviour
{
    public float speed = 3.0f;
    public int maxHealth = 5;
    public float timeInvincible = 2.0f;
    public  int health {get { return currentHealth; }}
    int currentHealth;

    bool isInvincible;
    float invincibleTimer;

    private Rigidbody2D rigidbody2d;
    float horizontal;

    float vertical;

     Animator animator;

    Vector2 lookDirectionVector2 = new Vector2(1, 0);

    // Start is called before the first frame update
    void Start()
    {
        // QualitySettings.vSyncCount = 0;
        // Application.targetFrameRate = 10;
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        currentHealth = maxHealth; //获取默认生命值
        currentHealth = maxHealth; //获取默认生命值
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        Vector2 move = new Vector2(horizontal, vertical);
        //检查x，y的值是否是零，Mathf.Approximately的精度更高
        if (!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f))
        {
            lookDirectionVector2.Set(move.x, move.y);
            lookDirectionVector2.Normalize();


        }
        animator.SetFloat("Look X", lookDirectionVector2.x);
        animator.SetFloat("Look Y", lookDirectionVector2.y);
        animator.SetFloat("Speed", move.magnitude);
    

        if (isInvincible)
        {
            invincibleTimer -= Time.deltaTime;
            if (invincibleTimer < 0)
                isInvincible = false;
        }

    }

    void FixedUpdate()
    {
        Vector2 position = rigidbody2d.position;
        // Debug.Log(horizontal);
        position.x += (3f * horizontal * Time.deltaTime);
        position.y += (3f * vertical * Time.deltaTime);
        rigidbody2d.MovePosition(position);

    }

    public void ChangeHealth(int amount)
    {
        if (amount < 0)
        {
            if (isInvincible)
                return;

            isInvincible = true;
            invincibleTimer = timeInvincible;
        }

        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        Debug.Log(currentHealth + "/" + maxHealth);
    }
}