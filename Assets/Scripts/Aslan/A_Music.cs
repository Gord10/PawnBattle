using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class A_Music : MonoBehaviour
{
    static A_Music instance;

    private void Awake()
    {
        if(instance)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
