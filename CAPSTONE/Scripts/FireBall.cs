using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
   
    public GameObject iBall;
    public float iSpeed;

    private Material mMaterial;
    private float mTime;

    public Transform target;
    public float damage;

    public int critCheck;

    private float noiseSharpness;

    Vector4 noise;


    protected void Awake()
    {
        critCheck = Random.Range(1, 11);
        damage = Random.Range(25, 30);
        if (critCheck == 1)
        {
            damage *= 1.0f;
            damage = Mathf.Round(damage);
        }

        target = GameObject.Find("Character").transform;

    }
    // Use this for initialization
    void Start()
    {
        mMaterial = iBall.GetComponent<Renderer>().material;

        mTime = 1.0f;
        noiseSharpness = 4.5f;
        Invoke("DestroyProjectile", 6.0f);
    }

    // Update is called once per frame
    void Update()
    {
        noise += new Vector4(2.0f, 1.0f, 1.07f, 1.05f);
        mMaterial.SetVector("_Scale", noise * mTime);
        mMaterial.SetVector("_Offset", noise * mTime);

        mTime += Time.deltaTime * iSpeed;

        mMaterial.SetFloat("_TOffset", Mathf.Repeat(mTime, 1.0f));

       

    }

    protected void OnCollisionEnter(Collision col)
    {

        if (col.gameObject.tag == "Player")
        {
            target.gameObject.GetComponent<Dunga>().Damage(damage, critCheck);
            Destroy(gameObject);
        }

        if (col.gameObject.tag == "Enemy")
        {
            col.gameObject.GetComponent<Character>().Damage(damage, critCheck);
            Destroy(gameObject);                       
        }

        if (col.gameObject.tag == "Barrier")
        {
            if (col.gameObject.GetComponentInParent<Dunga>())
            {
                GetComponent<Rigidbody>().AddForce(transform.right * 500);
            }
            else
            {
                Destroy(gameObject);
            }
        }

    }

    protected virtual void DestroyProjectile()
    {
        // destroy projectile after x seconds
        Destroy(gameObject, 3.0f);
    }

    protected void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }
}