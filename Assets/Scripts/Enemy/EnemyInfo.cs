using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyInfo : MonoBehaviour
{
    #region Editor Variables

    //[SerializeField]
    //[Tooltip("The name of this enemy")]
    //private string m_Name;
    //public string EnemyName {
    //    get {
    //        return m_Name;
    //    }
    //}

    //[SerializeField]
    //[Tooltip("The prefab of this enemy that will spawned")]
    //private Sprite m_EnemySprite;
    //public Sprite EnemySprite {
    //    get {
    //        return m_EnemySprite;
    //    }
    //}

    [SerializeField]
    [Tooltip("The Damage that the enmey will be having")]
    private int m_Damge;
    public int Damage {
        get {
            return m_Damge;
        }
    }

    [SerializeField]
    [Tooltip("The Health of this enemy")]
    private int m_Health;
    public int EnemyHealth {
        get {
            return m_Health;
        }
    }

    [SerializeField]
    [Tooltip("The speed of the enemy")]
    private float m_duration;
    public float EnemyDuration {
        get {
            return m_duration;
        }
    }


    [SerializeField]
    [Tooltip("The left Endpoint of the enemy")]
    private Vector2 m_leftEndPoint;
    public Vector2 EnemyLeftEndPoint {
        get {
            return m_leftEndPoint;
        }
    }

    [SerializeField]
    [Tooltip("The right Endpoint of the enemy")]
    private Vector2 m_rightEndPoint;
    public Vector2 EnemyRightEndPoint
    {
        get
        {
            return m_rightEndPoint;
        }
    }

    [SerializeField]
    [Tooltip("The health player could heal after dead")]
    private int m_heal;
    public int PlayerHeal
    {
        get
        {
            return m_heal;
        }
    }


    #endregion

    #region cached variables
    private float elaspsedTime;
    private bool isLeft = true;
    private float timeThatDoNotMoveCountDown = 0.0f;
    private Vector2 originialPos = new Vector2();
    private float afterHitLerp = 0f;
    private bool finishedHitEffect = true;
    #endregion


    private void Awake()
    {
        originialPos = transform.position;
    }

    #region updates


    private void Update()
    {
        if (timeThatDoNotMoveCountDown > 0) {
            timeThatDoNotMoveCountDown -= Time.deltaTime;
        }
        if (elaspsedTime / m_duration >= 1) {
            elaspsedTime = 0;
            isLeft = !isLeft;
        }
        else if (isLeft && finishedHitEffect)
        {
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2();
            elaspsedTime += Time.deltaTime;
            float percentageComplete = elaspsedTime / m_duration;
            transform.position = Vector2.Lerp(m_rightEndPoint, m_leftEndPoint, percentageComplete);
            originialPos = transform.position;
        }
        else if (!isLeft && finishedHitEffect)
        {
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2();
            elaspsedTime += Time.deltaTime;
            float percentageComplete = elaspsedTime / m_duration;
            transform.position = Vector2.Lerp(m_leftEndPoint, m_rightEndPoint, percentageComplete);
            originialPos = transform.position;
        }
    }

    #endregion

    #region functions
    public void EnemyDead() {
        gameObject.SetActive(false);
    }

    public void getDamaged(int damage, Vector2 force) {
        m_Health -= damage;
        StartCoroutine(getHit(force));
    }

    public IEnumerator getHit(Vector2 force) {
        finishedHitEffect = false;
        timeThatDoNotMoveCountDown = 0.4f;
        gameObject.GetComponent<Rigidbody2D>().AddForce(force, ForceMode2D.Impulse);
        while (timeThatDoNotMoveCountDown >= 0.0f)
        {
            yield return null;
        }
        transform.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        Vector2 afterForcePos = transform.position;
        if (m_Health <= 0)
        {
            EnemyDead();
        }
        while (Mathf.Abs(transform.position.x - originialPos.x) > 0.1 && Mathf.Abs(transform.position.y - originialPos.y) > 0.1)
        {
            transform.position = Vector2.Lerp(afterForcePos, originialPos, afterHitLerp);
            afterHitLerp += Time.deltaTime;
            yield return null;
        }
        afterHitLerp = 0.0f;
        finishedHitEffect = true;
        transform.position = originialPos;
    }
    
    #endregion
}
