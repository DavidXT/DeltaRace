using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectil : MonoBehaviour
{
    Vector3 destination;
    Vector3 direction;
    public float speed;
    public GameObject explosionToSpawn;
    bool alreadyExplode = false;

    // Start is called before the first frame update
    void Start()
    {
        if (Player.Instance)
        {
            destination = Player.Instance.transform.position;
            direction = destination - transform.position;
        }
        else
        {
            Destroy(gameObject);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += direction.normalized * (speed* Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.CompareTag("Player"))
        {
            if (!alreadyExplode)
            {
                alreadyExplode = true;
                Player.Instance.looseLife(1);
                Instantiate(explosionToSpawn, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
            
        }
        if (other.gameObject.CompareTag("Mur"))
        {
            if (!alreadyExplode)
            {
                alreadyExplode = true;
                Instantiate(explosionToSpawn, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
                
        }
    }
}
