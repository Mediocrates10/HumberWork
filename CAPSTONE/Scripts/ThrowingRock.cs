using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ThrowingRock : MonoBehaviour
{

    Rigidbody arrowBody;
    private bool canDamage;
    private Collider p_collider;
    protected Dunga dungaScript;
    protected float timer;
    protected float timePassed = -1f;
    protected bool hitShield = false;
    protected bool hitCheck = false;
    protected Material arrowMaterial;
    protected Renderer aRend;

    public GameObject brokenStonePrefab;
    public Material arrowDis;
    public Transform target;
    public float launchAngle;
    public float g;
    public float xSqr, zSqr;
    public float damage;

    public int critCheck;


    protected void Awake()
    {
        dungaScript = GameObject.Find("Character").GetComponent<Dunga>();
        canDamage = true;
        critCheck = Random.Range(1, 11);
        damage = Random.Range(9, 12);
        if (critCheck == 1)
        {
            damage *= 1.5f;
            damage = Mathf.Round(damage);
        }

        // set target for rock
        if (dungaScript.target != null)
        {
            target = dungaScript.target.transform;
        }

        // get object's rb component
        arrowBody = GetComponent<Rigidbody>();
        arrowMaterial = GetComponent<Material>();
        aRend = GetComponent<Renderer>();
        // set gravity
        g = Physics.gravity.y;
        // set arrow's velocity to 0
        arrowBody.velocity = Vector3.zero;

        xSqr = (target.transform.position.x - transform.position.x) * (target.transform.position.x - transform.position.x);
        zSqr = (target.transform.position.z - transform.position.z) * (target.transform.position.z - transform.position.z);

    }

    // Use this for initialization
    void Start()
    {

        p_collider = GetComponent<Collider>();
        // align arrow with trajectory
        transform.rotation = Quaternion.LookRotation(arrowBody.velocity);

        // calculate total distance to target using pythagorean theorem
        float targetDist = Mathf.Sqrt(xSqr + zSqr);
        targetDist += Random.Range(-1f, 1f);
        // calculate height differential between enemy and target
        float h = target.transform.position.y - transform.position.y;
        // rotate projectile to target
        transform.LookAt(target.transform.position);

        // calculate x and y component of velocity
        float tanAlpha = Mathf.Tan(launchAngle * Mathf.Deg2Rad);
        float Vx = Mathf.Sqrt(g * targetDist * targetDist / (2.0f * (h - targetDist * tanAlpha)));
        float Vy = tanAlpha * Vx;

        // velocity components into vector
        Vector3 localVelocity = new Vector3(0f, Vy, Vx);
        // convert local space coordinates into world coordinates for the projectile's velocity
        Vector3 globalVelocity = transform.TransformDirection(localVelocity);

        // apply the velocity to the arrow's rigidbody
        arrowBody.velocity = globalVelocity;
    }

    // Update is called once per frame
    void Update()
    {
        // align arrow with trajectory
        if (hitCheck == false)
            transform.rotation = Quaternion.LookRotation(arrowBody.velocity);

        if (hitShield == true)
        {
            timePassed += 2 * Time.deltaTime;
            Destroy(gameObject, 2.0f);
            gameObject.GetComponent<Renderer>().sharedMaterial.SetFloat("_DissolveAmount", timePassed);
            aRend.material = arrowDis;
        }
    }

    protected void OnCollisionEnter(Collision col)
    {
        // make projectile "stick" into object and then destroy projectile it after x seconds
        hitCheck = true;
        Destroy(gameObject);

        if (col.gameObject.tag == "Barrier" && canDamage == true)
        {
            Destroy(gameObject);
            GameObject stoneBreak = Instantiate(brokenStonePrefab, transform.position, Quaternion.identity);
            Destroy(stoneBreak, 2.0f);
            canDamage = false;
        }

        if (col.gameObject.tag == "Enemy" && canDamage == true)
        {
            col.gameObject.GetComponent<Character>().Damage(damage, critCheck);
            Destroy(gameObject);
            GameObject stoneBreak = Instantiate(brokenStonePrefab, transform.position, Quaternion.identity);
            Destroy(stoneBreak, 2.0f);
            canDamage = false;
        }

    }
    /*
    protected void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 13)
        {
            arrowBody.velocity /= 10;
            arrowBody.useGravity = false;
            hitShield = true;
        }
    }*/

    protected virtual void DestroyProjectile()
    {
        // destroy projectile after x seconds
        Destroy(gameObject, 2.0f);
    }

    protected void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }
}
