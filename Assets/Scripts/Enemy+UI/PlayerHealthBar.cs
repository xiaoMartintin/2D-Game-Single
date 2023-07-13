using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : HealthBar
{
    private PlayerHealth playerHealth;
    //public GameObject follow;

    // Start is called before the first frame update
    void Start()
    {
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        HealthMax = playerHealth.health;
        
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
        //Vector2 healthBarFollow = Camera.main.WorldToScreenPoint(follow.transform.position);
        //this.GetComponent<RectTransform>().position =  healthBarFollow;
    }
}
