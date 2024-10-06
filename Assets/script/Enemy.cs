using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //public static event Action<Enemy> OnEnemyKilled;
    [SerializeField] float Health, MaxHealth=5f;
    [SerializeField] private float moveSpeed = 2f; // Set default speed or assign in Inspector

    // Start is called before the first frame update
    Rigidbody2D rb;
    Transform target;
    Vector2 moveDirection;
  
    //[SerializeField] FloatingHealthBar healthBar;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        //healthBar = GetComponentInChildren<FloatingHealthBar>();
    }
    
    private void Start()
    {
        Health=MaxHealth;
        //healthBar.UpdateHealthBar(healthBar,maxHealth);
        target = GameObject.Find("Hexagon Flat-Top").transform;
    }

    // Update is called once per frame
    private void Update()
    {
        if(target)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            rb.rotation = angle; //change this line to use transform rotation
            //transform.eulerAngles = new Vector3(0, 0, angle);
            moveDirection = direction;
        }
    }
    
    private void FixedUpdate()
    {
        if(target)
        {
            //Vector3 pos = transform.position;
            rb.velocity = new Vector2(moveDirection.x, moveDirection.y)*moveSpeed;
            //transform.position = new Vector3(pos.x + velocity.x*Time.deltaTime, pos.y + velocity.y*Time.deltaTime, pos.z);
        }
    }

    public void TakeDamage(float damageAmount)
    {
        Health-= damageAmount;
        //healthBar.UpdateHealthBar(healthBar,maxHealth);
        if(Health<=0)
        {
            Destroy(gameObject);
            // OnEnemyKilled?.Invoke(this);
        }
    }
}
