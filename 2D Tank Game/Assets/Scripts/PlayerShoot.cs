using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerShoot : NetworkBehaviour {

    public float damage = 1.0f;
    public int maxAmmo = 10;
    public int ammo = 10;

    public GameObject bulletPrefab;
    public Transform bulletSpawn;

    [SerializeField]
    private LayerMask mask;

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetButtonDown("Shoot")) {
            
            if (ammo > 0)
                CmdShoot();
            Debug.LogWarning("Ammo:" + ammo);
            ammo--;
        }
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("Pickup Collision");
        if (col.gameObject.tag == "ammo" && ammo < maxAmmo)
        {
            ammo = maxAmmo;
            Debug.LogWarning("Ammo Restored:" + ammo);
            Destroy(col.gameObject);
        }
    }

    [Command]
    void CmdShoot()
    {
        var bullet = (GameObject)Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);

        NetworkServer.Spawn(bullet);

        bullet.GetComponent<Rigidbody2D>().velocity = bullet.transform.up * -6;

        Destroy(bullet, 1.5f);
    }



}
