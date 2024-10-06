using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDown : MonoBehaviour
{
    public float speed = 150f;
    public static GameObject stabPrefab;
    public static GameObject AOEPrefab;
    public static GameObject ProjectilePrefab;
    private Animator animator;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;
        bool isMoving = false;

        // Movement Input
        if (Input.GetKey("w"))
        {
            pos.y += speed * Time.deltaTime;
            isMoving = true;
        }
        if (Input.GetKey("s"))
        {
            pos.y -= speed * Time.deltaTime;
            isMoving = true;
        }
        if (Input.GetKey("d"))
        {
            pos.x += speed * Time.deltaTime;
            isMoving = true;
            // Flip character to face right
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        if (Input.GetKey("a"))
        {
            pos.x -= speed * Time.deltaTime;
            isMoving = true;
            // Flip character to face left
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }

        // Apply the new position
        transform.position = pos;

        // Update Animator
        animator.SetBool("isWalking", isMoving);
    }
      public static void Projectile(float damage){
         //  float projectileSpeed = 10f;
         //     float MaxLifeTime = 10.0f;
         //     float lifetime;
         GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
         float minDist = 999f;
         GameObject closestEnemy = null;
         foreach (GameObject enemy in enemies){
            if (Vector3.Distance(gameObject.transform.position, enemy.transform.position) < minDist){
               closestEnemy = enemy;
            }
         }
         GameObject proj = GameObject.Instantiate(ProjectilePrefab, transform.position, transform.rotation);
         proj.GetComponentInChildren<ProjectileMovement>().direction = closestEnemy.transform.position-transform.position;
      }

    public static void Stab(float damage)
    {
      //Search for closest enemy
      //Apply damage to closest enemy
      //Show spike + orient it towards enemy
      GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
      float minDist = 9f;
      GameObject closestEnemy = null;
      foreach (GameObject enemy in enemies){
         if (Vector3.Distance(gameObject.transform.position, enemy.transform.position) < minDist){
            closestEnemy = enemy;
         }
      }
      // if (closestEnemy){
      //    closestEnemy.GetComponent<Enemy>().TakeDamage(damage);
      // }
      //Rotate stabPivot to enemy, enable stabPivot
      if (closestEnemy){
         Vector3 direction = (closestEnemy.transform.position - transform.position).normalized;
         float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
         
         GameObject stab = Instantiate(stabPrefab, transform.position, Quaternion.identity);
         stab.transform.SetParent(transform);

         stab.transform.eulerAngles =  new Vector3(0, 0, angle);

         /*
         stabPivot.SetActive(true);
         Vector3 direction = (closestEnemy.transform.position - transform.position).normalized;
         float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
         stabPivot.transform.eulerAngles = new Vector3(0, 0, angle);
         StartCoroutine(DeleteStab());*/
      }
    }


   //  public void Projectile(float damage)
   //  {
   //    private float projectileSpeed = 10f;
   //    private float MaxLifeTime = 10.0f;
   //    private float lifetime;
   //    GameObject.Instantiate(ProjectilePrefab, tranform.position, transform.rotation);
   //  }
      
   //    GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
   //    float minDist = 999f;
   //    GameObject closestEnemy = null;
   //    foreach (GameObject enemy in enemies){
   //       float distance = Vector3.Distance(gameObject.transform.position, enemy.transform.position);
   //       if (distance<minDist)
   //       {
   //          minDist = distance;
   //          closestEnemy = enemy;
   //       }
   //    }
      
      
   //       if (closestEnemy )
   //       {
   //          Vector3 direction = (closestEnemy.transform.position - transform.position).normalized;
   //          float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            
   //          //GameObject spawnPoint = transform.Find("ProjectileSpawnPoint").gameObject;
   //          GameObject projectile = Instantiate(ProjectilePrefab, transform.position, Quaternion.identity);
            
   //          projectile.transform.eulerAngles =  new Vector3(0, 0, angle);
            
   //          Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();   
         
   //          if (rb!=null)
   //          {
   //             //void start()
   //             //{
   //             //   lifetime=0;   
   //             //}
   //             // void FixedUpdate()
   //             //{
   //             //  lifetime+= Time.fixedDeltaTime;
   //             //   transform.position += transform.forward*projectileSpeed;
   //             //   if(lifetime>MaxLifeTime)
   //             //   {
   //             //      Destroy(gameObject);
   //             //   }
   //             //}
   //             // rb.velocity = direction*projectileSpeed;
   //          }
   //       }
        
   // }

   public static void AOE(float damage)
   {

      GameObject aoe = Instantiate(AOEPrefab, transform.position, Quaternion.identity);
      aoe.transform.SetParent(transform);

   }
    /*
    IEnumerator DeleteStab()
    {
      yield return new WaitForSeconds(.5f);
      stabPivot.SetActive(false);
    }*/

    public static void Cast(Magic magic)
    {
        string type = magic.magicType;
        if (type == "stab")
        {
            Stab(magic.magicDamage);
        }else if (type == "projectile")
        {
            Projectile(magic.magicDamage);
        }else if (type == "AOE")
        {
            Projectile(magic.magicDamage);
        }
    }
}
