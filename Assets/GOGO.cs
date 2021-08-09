using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GOGO : MonoBehaviour
{
    public String sceneName;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("oh you in");
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("oh player in");
            SceneManager.LoadScene(sceneName);
        }
    }
}
