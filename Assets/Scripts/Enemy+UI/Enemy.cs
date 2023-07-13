//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public abstract class Enemy : MonoBehaviour
//{
//    public int health;
//    public int damage;
//    public float flashTime;
//    public GameObject bloodEffect;
//    public GameObject dropCoin;
//    public GameObject floatPoint;

//    private SpriteRenderer sr;
//    private Color originalColor;
//    private PlayerHealth playerHealth;

//    // Start is called before the first frame update
//    public void Start()
//    {
//        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
//        sr = GetComponent<SpriteRenderer>();
//        originalColor = sr.color;
//    }

//    // Update is called once per frame
//    public void Update()
//    {
//        if (health <= 0)
//        {
//            Instantiate(dropCoin, transform.position, Quaternion.identity);
//            Destroy(gameObject);
//        }
//    }

//    public void TakeDamage(int damage)
//    {
//        GameObject gb = Instantiate(floatPoint, transform.position, Quaternion.identity) as GameObject;
//        gb.transform.GetChild(0).GetComponent<TextMesh>().text = damage.ToString();
//        health -= damage;
//        FlashColor(flashTime);
//        Instantiate(bloodEffect, transform.position, Quaternion.identity);
//        GameController.camShake.Shake();
//    }

//    void FlashColor(float time)
//    {
//        sr.color = Color.red;
//        Invoke("ResetColor", time);
//    }

//    void ResetColor()
//    {
//        sr.color = originalColor;
//    }

//    void OnTriggerEnter2D(Collider2D other)
//    {
//        if (other.gameObject.CompareTag("Player") && other.GetType().ToString() == "UnityEngine.CapsuleCollider2D")
//        {
//            if (playerHealth != null)
//            {
//                playerHealth.DamagePlayer(damage);
//            }
//        }
//    }
//}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public int health;
    public int damage;

    public float flashTime;

    private SpriteRenderer sr;
    private Color originalColor;
    private PlayerHealth playerHealth;
    public GameObject floatPoint;


    public GameObject bloodEffect;

    public void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        originalColor = sr.color;
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
    }

    public void Update()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void TakeDamage(int dam)
    {
        GameObject gb = Instantiate(floatPoint, transform.position, Quaternion.identity) as GameObject;
        gb.transform.GetChild(0).GetComponent<TextMesh>().text = damage.ToString();
        health -= dam;
        FlashColor(flashTime);
        Instantiate(bloodEffect, transform.position, Quaternion.identity);
    }

        

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && other.GetType().ToString() == "UnityEngine.PolygonCollider2D")
        {
            if (playerHealth != null)
            {
                playerHealth.DamagePlayer(damage);
            }

        }
        //if (other.gameObject.CompareTag("Player") && other.GetType().ToString() == "UnityEngine.PolygonCollider2D")
        //{
        //    other.gameObject.GetComponent<PlayerScript>.OnHit(damage);
        ////    other.gameObject.GetComponent<PkayerNetwork>.OnHit(damage);
        //}


    }
    public void FlashColor(float time)
    {
        sr.color = Color.red;
        Invoke("ResetColor", time);
    }

    public void ResetColor()
    {
        sr.color = originalColor;
    }

}
