using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarFollow : MonoBehaviour
{
    public GameObject follow;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        Vector2 healthBarFollow = Camera.main.WorldToScreenPoint(follow.transform.position);
        healthBarFollow.y = healthBarFollow.y + 45;
        this.GetComponent<RectTransform>().position = healthBarFollow;
    }
}
