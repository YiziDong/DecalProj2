using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerEntity : MonoBehaviour
{
#region Public variables
    public int m_Damage;
    public int m_Health;
#endregion

#region Editable variables
    [SerializeField]
    private float m_JumpX;
    public float playerJumpX() {
        return m_JumpX;
    }

    [SerializeField]
    private float m_JumpY;
    public float playerJumpY()
    {
        return m_JumpY;
    }

    [SerializeField]
    private float m_poisonDefenseCoolDown;

    [SerializeField]
    private float m_poisonDefenseDurationCountDown;

    [SerializeField]
    private float m_attackCooldown;

    [SerializeField]
    private GameObject m_tonge;

    [SerializeField]
    private GameObject Verticalwall;
    #endregion

    #region Fixed variables
    private Rigidbody2D m_rb;
    private Collider2D m_collider;
    private bool canMove = false;
    private float PoisionDefenseCoolDown = 0.0f;
    private float PoisionDefenseDurationCountDown = 0.0f;
    private float attackCoolDown = 0f;
#endregion

#region Fixed functions

    private void MoveLeft()
    {
            m_rb.AddForce(new Vector2(-m_JumpX, m_JumpY), ForceMode2D.Impulse);
    }

    private void MoveRight()
    {
        m_rb.AddForce(new Vector2(m_JumpX, m_JumpY), ForceMode2D.Impulse);
    }

    private void MoveUp() {
        m_rb.AddForce(new Vector2(0, m_JumpY * 2f), ForceMode2D.Impulse);
    }

    private void PoisonDefense() {
        PoisionDefenseCoolDown = m_poisonDefenseCoolDown;
        PoisionDefenseDurationCountDown = m_poisonDefenseDurationCountDown;
    }

    void OnCollisionEnter2D(Collision2D collider)
    {
        
        if (collider.gameObject.tag == "floor")
        {
            canMove = true;
            if (Verticalwall)
            {
                Verticalwall.GetComponent<VerticalPlatform>().changeBackRotation();
            }
            if (gameObject.GetComponentInChildren<feet>().Landing == true) {
                m_rb.velocity = new Vector2(0, m_rb.velocity.y);
                gameObject.GetComponentInChildren<feet>().Landing = false;
            }
        }
        if (collider.gameObject.tag == "enemy" && PoisionDefenseDurationCountDown > 0.0f) {
            Vector2 repel = m_rb.velocity;
            if (collider.transform.position.x - gameObject.transform.position.x < 0.1)
            {
                repel.x -= 5f;
                repel.y += 3f;
            }
            else if (collider.transform.position.x - gameObject.transform.position.x > 0.1)
            {
                repel.x += 5f;
                repel.y += 3f;
            }
            else
            {
                repel.y += 6f;
            }
            collider.gameObject.GetComponent<EnemyInfo>().getDamaged(m_Damage, repel);
        }
        if (collider.gameObject.tag == "enemy" && m_collider.IsTouching(collider.gameObject.GetComponent<Collider2D>()) && PoisionDefenseDurationCountDown <= 0.0f)
        {
            Vector2 repel = m_rb.velocity;
            if (collider.transform.position.x - gameObject.transform.position.x < 0.1)
            {
                repel.x += 5f;
            }
            else if (collider.transform.position.x - gameObject.transform.position.x > 0.1)
            {
                repel.x -= 5f;
            }
            m_rb.AddForce(repel, ForceMode2D.Impulse);
            m_Health -= 1;
        }
    }
    void OnCollisionExit2D(Collision2D collider)
    {
        if (collider.gameObject.tag == "floor")
        {
            canMove = false;
        }
        
    }

    #endregion

    #region in-game functions

    void Awake()
    {
        //get the tonge object by placing it at the first under player
        m_rb = this.GetComponent<Rigidbody2D>();
        m_collider = this.GetComponent<Collider2D>();
    }
    void Update()
    {
        if (m_Health <= 0 && Input.GetKeyDown(KeyCode.Escape)) {
            Debug.Log("quiting");
            Application.Quit();
        }
        if (PoisionDefenseCoolDown >= 0.0f) {
            PoisionDefenseCoolDown -= Time.deltaTime;
        }
        if (PoisionDefenseDurationCountDown > 0.0f) {
            PoisionDefenseDurationCountDown -= Time.deltaTime;
            if (PoisionDefenseDurationCountDown <= 0.0f) {
                gameObject.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255);
            }
        }
        if (attackCoolDown > 0f) {
            attackCoolDown -= Time.deltaTime;
        }
        if (Input.GetKeyDown("a"))
        {
            if (canMove)
            {
                MoveLeft();
                canMove = false;
            }
        }
        if (Input.GetKeyDown("d") )
        {
            if (canMove)
            {
                MoveRight();
                canMove = false;
            }
        }
        if (Input.GetKeyDown("w")) {
            if (canMove) {
                MoveUp();
                canMove = false;
            }
        }
        if (Input.GetKeyDown("s") && canMove && Verticalwall) {
            Verticalwall.GetComponent<VerticalPlatform>().changeRotate();
        }
        if (Input.GetKeyDown("left shift"))
        {
            if (PoisionDefenseCoolDown <= 0.0f)
            {
                PoisonDefense();
                gameObject.GetComponent<SpriteRenderer>().color = new Color(207,0, 255);
            }
        }
        if (Input.GetKeyDown("space") && attackCoolDown <= 0.0f) {
            m_tonge.GetComponent<Tonge>().StartAttack();
            attackCoolDown += m_attackCooldown;
        }
    }   
#endregion


}
