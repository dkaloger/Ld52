using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int MaxHp;
    public int CurrentHP;
    private void Start()
    {
        CurrentHP = MaxHp;
    }
    void DoDamage(int damage)
    {
        CurrentHP -= damage;
        if(CurrentHP == 0)
        {
            //also destroy tile
            if(gameObject.GetComponent<PlaceableItem>() != null)
            {
                gameObject.GetComponent<PlaceableItem>().Remove();
            }
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.GetComponent<Projectile>() != null)
        {
        DoDamage(collision.gameObject.GetComponent<Projectile>().HitDamage);
        Destroy(collision.gameObject);
        }
        else
        {
            print("unwanted Collision");
        }
       
    }
}
