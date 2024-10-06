using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float Health;
    public float MaxHealth;
    public Image HealthBar;

    public float Health_Recover;
    // Start is called before the first frame update
    void Start()
    {
        MaxHealth=Health;    
    }

    // Update is called once per frame
    void Update()
    {
        HealthBar.fillAmount = Mathf.Clamp(Health/MaxHealth,0,1);
        if(Health <=0)
        {
            Destroy(gameObject);
        }
    }
    public void Heal()
    {
        if(Health<=MaxHealth-Health_Recover)
        {
            Health+=Health_Recover;
        }
        else if(MaxHealth>MaxHealth-Health_Recover)
        {
            Health = MaxHealth;
        }
        
        
    }
}
