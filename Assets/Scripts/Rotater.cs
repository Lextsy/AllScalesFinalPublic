using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotater : MonoBehaviour
{
    bool rotater;
    public Transform cube;
    public float speed1 = 1.0f;
    public float speed2 = 1.0f;
    public float speed3 = 1.0f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
                  

    }
    void Bitchtate()
    {cube.Rotate(speed1*Time.deltaTime,speed2,speed3);
       


    }

      void FixedUpdate()
    {
        Bitchtate();

    }
    
}
