using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float health = 150f;
    public GameManager manager;

    public void TakeDamage(float damage){
        health -= damage;
        Debug.Log(damage + "damage taken; health at " + health);
        if(health <= 0){
            GameOver();
        }
    }

    void GameOver(){
        Debug.Log("GameOver");
        manager.GameOver();
    }
}
