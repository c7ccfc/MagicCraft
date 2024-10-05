using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDown : MonoBehaviour
{
    public float speed = 10.4f;
    public GameObject stabPrefab;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       Vector3 pos = transform.position;
       if(Input.GetKey("w"))
       {
          pos.y+= speed*Time.deltaTime;
       }
       if(Input.GetKey("s"))
       {
          pos.y-= speed*Time.deltaTime;
       }
       if(Input.GetKey("d"))
       {
          pos.x+= speed*Time.deltaTime;
       }
       if(Input.GetKey("a"))
       {
          pos.x-= speed*Time.deltaTime;
       }
       transform.position = pos;
    }

    public void Stab( float damage)
    {
      //Search for closest enemy
      //Apply damage to closest enemy
      //Show spike + orient it towards enemy
      GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
      float minDist = 999f;
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
    public void Projectile( float damage)
    {
      //Search for closest enemy
      //Apply damage to closest enemy
      //Show spike + orient it towards enemy
      GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
      float minDist = 999f;
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
         
         GameObject projectile = Instantiate(stabPrefab, transform.position, Quaternion.identity);
         

         projectile.transform.eulerAngles =  new Vector3(0, 0, angle);
         //write code to let the ball to go along this angle with some speed
         /*
         stabPivot.SetActive(true);
         Vector3 direction = (closestEnemy.transform.position - transform.position).normalized;
         float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
         stabPivot.transform.eulerAngles = new Vector3(0, 0, angle);
         StartCoroutine(DeleteStab());*/
      }
    }
    public void AOE( float damage)
    {
      //Search for closest enemy
      //Apply damage to closest enemy
      //Show spike + orient it towards enemy
      GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
      float minDist = 999f;
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
         
         GameObject aoe = Instantiate(stabPrefab, transform.position, Quaternion.identity);
         aoe.transform.SetParent(transform);

         aoe.transform.eulerAngles =  new Vector3(0, 0, angle);

         /*
         stabPivot.SetActive(true);
         Vector3 direction = (closestEnemy.transform.position - transform.position).normalized;
         float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
         stabPivot.transform.eulerAngles = new Vector3(0, 0, angle);
         StartCoroutine(DeleteStab());*/
      }
    }
    /*
    IEnumerator DeleteStab()
    {
      yield return new WaitForSeconds(.5f);
      stabPivot.SetActive(false);
    }*/
}
