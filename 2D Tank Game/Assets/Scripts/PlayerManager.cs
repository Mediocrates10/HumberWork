using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class PlayerManager : NetworkBehaviour
{

    [SerializeField]
    private float maxHealth = 5.0f;

    [SyncVar]
    private float currHealth;

    private void Awake()
    {
        SetDefaults();
    }

    public void TakeDamage(float dmg)
    {
        currHealth -= dmg;
        Debug.Log("Health: " + currHealth);
        if (currHealth <= 0)
        {
            death();
            Invoke("EndGame", 2f);
        }
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("Pickup Collision");
        if (col.gameObject.tag == "health" && currHealth < maxHealth)
        {
            currHealth = maxHealth;
            Debug.Log("Health Restored: " + currHealth);
            Destroy(col.gameObject);
        }
    }

    void death()
    {
        Destroy(gameObject);
    }

    void SetDefaults()
    {
        currHealth = maxHealth;
    }

    void EndGame()
    { 
        SceneManager.LoadScene("SampleScene");
    }
}