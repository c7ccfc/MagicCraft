using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtsEnemies : MonoBehaviour
{
    float damageAmount;
    public Collider2D hitbox;
    public float dps;//damage per seconds
    public float duration;

    public int maxTargets;

    // Start is called before the first frame update
    void Start()
    {
        hitbox = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        duration -= Time.deltaTime;
        if (duration < 0)
        {
            Destroy(transform.parent.gameObject);
        }

        Collider2D[] results = new Collider2D[maxTargets];
        hitbox.OverlapCollider(new ContactFilter2D(), results);

        foreach (Collider2D target in results)
        {
            if (target == null) break;

            Enemy enemy = target.GetComponent<Enemy>();
            if (enemy != null){
                enemy.TakeDamage(dps * Time.deltaTime);
            }
        }
    }
}
