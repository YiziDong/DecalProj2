using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tonge : MonoBehaviour
{

    #region Serialized variables
    [SerializeField]
    private float m_maxExpandY;

    [SerializeField]
    private float m_expandSpeed;

    [SerializeField]
    private GameObject wall;

    #endregion

    #region private updates
    private float ColliderOffset;
    private float ColliderSize;
    private BoxCollider2D m_Collider;
    private SpriteRenderer m_Sprite;
    private bool duringAttack = false;
    #endregion

    #region fixed functions
    public void StartAttack()
    {
        StartCoroutine(Attack());
    }

    public IEnumerator Attack() {
        duringAttack = true;
        Color changeTrans = m_Sprite.color;
        changeTrans.a = 255;
        m_Sprite.color = changeTrans;
        Vector2 offsetChange = new Vector2(0, - 0.3f);
        while (m_Collider.offset.y < 0.5) {
            m_Collider.offset = offsetChange;
                offsetChange.y += Time.deltaTime * m_expandSpeed;
            yield return null;
        }
        duringAttack = false;
        m_Collider.offset = new Vector2(0, -0.3f);
        changeTrans.a = 0;
        m_Sprite.color = changeTrans;
    }

    #endregion

    #region updates

    private void Awake()
    {
        m_Collider = gameObject.GetComponent<BoxCollider2D>();
        m_Sprite = gameObject.GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        Physics2D.IgnoreCollision(wall.GetComponent<BoxCollider2D>(), GetComponent<BoxCollider2D>());
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (duringAttack && collision.gameObject.tag == "enemy") {
            Vector2 repel = Vector2.zero;
            if (collision.transform.position.x - gameObject.transform.position.x < 0.1)
            {
                repel.x -= 5f;
                repel.y += 3f;
            }
            else if (collision.transform.position.x - gameObject.transform.position.x > 0.1)
            {
                repel.x += 5f;
                repel.y += 3f;
            }
            else
            {
                repel.y += 6f;
            }
            collision.gameObject.GetComponent<EnemyInfo>().getDamaged(this.GetComponentInParent<playerEntity>().m_Damage, repel);
        }
    }

    

    #endregion
}
