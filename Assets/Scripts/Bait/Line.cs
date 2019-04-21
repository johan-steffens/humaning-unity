using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour
{
    public GameObject line;
    public GameObject player;

    private List<GameObject> lines;

    void Start()
    {
        lines = new List<GameObject>();

        GetComponent<Rigidbody2D>().AddForce(new Vector2(2, -2));
    }

    // Update is called once per frame
    void Update()
    {
        // Calculate amount of lines between us (bait) and player
        if(enabled)
        {
            float lineSize = line.GetComponent<SpriteRenderer>().size.x;
            float distance = Vector3.Distance(transform.position, player.transform.position);

            // Calculate amount of lines
            int amountOfLines = Mathf.CeilToInt(distance / lineSize) - 1;

            if (amountOfLines <= 1)
                return;

            // Offset player transform
            Vector3 playerPosition = player.transform.position;

            // Update lines
            for (int i = 0; i < Mathf.Max(lines.Count, amountOfLines); i++)
            {
                // If our array hasn't been filled up to here, add another item
                if(lines.Count < i + 1)
                {
                    GameObject newLine = Instantiate(line);
                    newLine.transform.parent = transform;
                    lines.Add(newLine);
                }

                // Set line position and rotation 
                if (i <= amountOfLines)
                {
                    // Position
                    lines[i].transform.position = Vector3.Lerp(playerPosition, transform.position, (float) i / (float) amountOfLines);
                    lines[i].transform.position = new Vector3(lines[i].transform.position.x, lines[i].transform.position.y, 0);

                    // Rotation
                    Vector3 vectorToTarget = new Vector3(playerPosition.x, playerPosition.y) - new Vector3(transform.position.x, transform.position.y);
                    float angle = (Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg);
                    Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
                    lines[i].transform.rotation = q;

                    // Enabled
                    lines[i].SetActive(true);
                } else
                {
                    lines[i].SetActive(false);
                }
            }
        }
    }
}
