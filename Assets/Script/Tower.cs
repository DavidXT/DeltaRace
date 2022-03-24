using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class Tower : MonoBehaviour
{
    public float range;
    public GameObject spawnPoint;

    public float delayBeforeShot;
    float currentDelay;

    public GameObject projectil;

    private void Start()
    {
        currentDelay = delayBeforeShot;
    }

    // Update is called once per frame
    void Update()
    {
        if(currentDelay >= 0)
        {
            currentDelay -= Time.deltaTime;
        }

        Collider[] player = Physics.OverlapSphere(transform.position, range)
            .Where(x => x.gameObject.CompareTag("Player"))
            .ToArray();

        if(player.Length > 0)
        {
            transform.LookAt(player[0].transform.position);

            if(currentDelay <= 0)
            {
                Instantiate(projectil, spawnPoint.transform.position, Quaternion.identity);
                currentDelay = delayBeforeShot;
            }
        }
    }
}
