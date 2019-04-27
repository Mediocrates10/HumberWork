using UnityEngine;
using UnityEngine.Networking;

public class Bullet : NetworkBehaviour
{

    public float velocity = 5f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            CmdPlayerShot(collision.gameObject.name, 1f);
        }

        Destroy(this.gameObject);
    }

    [Command]
    public void CmdPlayerShot(string ID, float damage)
    {
        Debug.Log(ID + " has been shot");

        PlayerManager player = GameManager.GetPlayer(ID);
        player.TakeDamage(damage);
    }
}