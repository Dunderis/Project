using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class Switchscene : MonoBehaviour
{
    public string SceneName;

    public bool random;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SceneChange()
    {
        if (random)
        {
            int num = Random.Range(0, 2);
            if (num == 0)
            {
                SceneManager.LoadScene("Map 1");
            }
            else
            {
                SceneManager.LoadScene("Map 2");
            }
        }
        else
        {
            SceneManager.LoadScene(SceneName);
        }
    }
}
