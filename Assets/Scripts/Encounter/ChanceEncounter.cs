using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChanceEncounter : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(0, 20), -Random.Range(0, 20));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
