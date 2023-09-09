


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int health;
    public int blinks;
    public float time;
    public float dieTime;
    //private SpriteRenderer sr;
    //private Color originalColor;
    //public GameObject bloodEffect;
    //public float flashTime;
    private Renderer myRender;
    private Animator anim;

    public void Start()
    {
        myRender = GetComponent<Renderer>();
        anim = GetComponent<Animator>();
        //sr = GetComponent<SpriteRenderer>();
        //originalColor = sr.color;
    }

    public void Update()
    {
        if(health <= 0)
        {
        
        }
    }

    public void DamagePlayer(int dam)
    {
        health -= dam;
        //FlashColor(flashTime);
        //Instantiate(bloodEffect, transform.position, Quaternion.identity);
        if (health < 0)
        {
            health = 0;
        }
        HealthBar.HealthCurrent = health;
        if (health <= 0)
        {
            anim.SetTrigger("Die");
            Invoke("KillPlayer", dieTime);
        }
        BlinkPlayer(blinks, time);
    }

    //public void FlashColor(float time)
    //{
    //    sr.color = Color.red;
    //    Invoke("ResetColor", time);
    //}

    //public void ResetColor()
    //{
    //    sr.color = originalColor;
    //}

    void BlinkPlayer(int numBlinks, float seconds)
    {
        StartCoroutine(DoBlinks(numBlinks, seconds));
    }

    IEnumerator DoBlinks(int numBlinks, float seconds)
    {
        for (int i = 0; i < numBlinks * 2; i++)
        {
            myRender.enabled = !myRender.enabled;
            yield return new WaitForSeconds(seconds);
        }
        myRender.enabled = true;
    }


    void KillPlayer()
    {
        Destroy(gameObject);
    }
}