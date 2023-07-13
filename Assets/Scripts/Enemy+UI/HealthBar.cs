using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Text healthText;
    public static int HealthCurrent;
    public static int HealthMax;
    //private PlayerHealth playerHealth;

    private Image healthBar;

    // Start is called before the first frame update
    public void Start()
    {
        //playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        //HealthMax = playerHealth.health;
        healthBar = GetComponent<Image>();
        HealthCurrent = HealthMax;
    }

    // Update is called once per frame
    public void Update()
    {
        healthBar.fillAmount = (float)HealthCurrent / (float)HealthMax;
        healthText.text = HealthCurrent.ToString() + "/" + HealthMax.ToString();
    }
}
