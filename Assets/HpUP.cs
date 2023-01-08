using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class HpUP : MonoBehaviour
{
    PlaceableItem Getitematpos(Vector3 position)
    {
        foreach (PlaceableItem unit in GameObject.FindObjectsOfType<PlaceableItem>())
        {
            if (Vector3.Equals(unit.transform.position, position))
            {
                return unit;
            }

        }
        return null;
    }
    // Start is called before the first frame update
    void Start()
    {
        transform.position = GameObject.Find("Units").GetComponent<Tilemap>().layoutGrid.WorldToCell(transform.position) + new Vector3(0.5f, 0.5f, 0);
    }

    // Update is called once per frame
    void Update()
    {
     
       if (Getitematpos(transform.position) != null)
        {
            Health helping = Getitematpos(transform.position).GetComponent<Health>();

            helping.CurrentHP++;
            if (helping.CurrentHP > helping.MaxHp)
            {
                helping.MaxHp++;
            }
            Destroy(gameObject);
        }
    }
}
