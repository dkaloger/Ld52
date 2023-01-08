using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int MaxHp;
    public int CurrentHP;
    public Vector3[] vulnerablepoints;
    public bool VulnerablePoints;
    public bool releasechildrenOnDeath;
    private void Start()
    {
        CurrentHP = MaxHp;
    }
    void DoDamage(int damage)
    {
        CurrentHP -= 1;
        print(gameObject.name);
        if (CurrentHP == 0)
        {
            //print("here22");
            //also destroy tile
            if (gameObject.GetComponent<PlaceableItem>() != null)
            {
                gameObject.GetComponent<PlaceableItem>().Remove();
            }
            if (releasechildrenOnDeath)
            {
                Destroy(transform.GetChild(0).gameObject);
                for (int i = 0; i < transform.childCount; i++)
                {
                    transform.GetChild(i).gameObject.SetActive(true);
                }
                
          transform.DetachChildren();

            }
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
       
        Vector3 direction = -1 *(transform.position - collision.transform.position);
        direction = Vector3.Normalize(direction);
        if (collision.gameObject.GetComponent<Projectile>() != null   &&!VulnerablePoints)
        {
            
            if (collision.gameObject.GetComponent<Projectile>().dontkill != gameObject)
            {
                
                DoDamage(collision.gameObject.GetComponent<Projectile>().HitDamage);
            }
        }
        else
        {

            foreach (var vuldir in vulnerablepoints)
            {
                print(direction);
                if (vuldir == direction)
                {
                    DoDamage(collision.gameObject.GetComponent<Projectile>().HitDamage);

                }
            }
        }

        if (collision.gameObject.GetComponent<Projectile>().dontkill != gameObject)
        {
            Destroy(collision.gameObject);
        }

    }
}
