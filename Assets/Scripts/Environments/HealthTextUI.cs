using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HealthTextUI : MonoBehaviour
{

    public TextMeshProUGUI m_texts;

    public GameObject player;

    private int playerHealth;

    private void Start()
    {
        playerHealth = player.GetComponent<playerEntity>().m_Health;
    }



    private void Update()
    {
        playerHealth = player.GetComponent<playerEntity>().m_Health;
        if (playerHealth == 1)
        {
            m_texts.SetText("1");
        }
        else if (playerHealth == 2) {
            m_texts.SetText("2");
        }
        else if (playerHealth == 3)
        {
            m_texts.SetText("3");
        }
    }
}
