using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChanceEncounter : MonoBehaviour
{
    public Game.Size size = Game.Size.LARGE;

    // Start is called before the first frame update
    void Start()
    {
        // Start in random direction
        int modX = Random.Range(0, 2);
        int modY = Random.Range(0, 2);
        GetComponent<Rigidbody2D>().velocity = new Vector2((modX == 0 ? -1 : 1) * Random.Range(0, 20), (modX == 0 ? -1 : 1) * -Random.Range(0, 20));
    }

    
}
