using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int maxHP = 3;
    private int currentHP;

    void Start()
    {
        currentHP = maxHP;
    }

    public void TakeDamage(int damage)
    {
        currentHP -= damage;
        Debug.Log("Player took damage! HP: " + currentHP);

        if (currentHP <= 0)
        {
            Die();
        }
    }


    void Die()
    {
        SceneManager.LoadScene("Play scene");
    }
}