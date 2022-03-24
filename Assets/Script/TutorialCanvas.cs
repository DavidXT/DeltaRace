using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialCanvas : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(coroutineDestruct());
    }
    // Update is called once per frame
    void Update()
    {
        if (Player.Instance)
        {
            Debug.Log(Player.Instance.transform.position.z);
            if(Player.Instance.transform.position.z > 80)
            {
                gameObject.SetActive(false);
            }
        }
    }

    public IEnumerator coroutineDestruct()
    {
        yield return new WaitForSeconds(6);
        gameObject.SetActive(false);
    }
}
