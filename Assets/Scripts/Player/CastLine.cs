using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastLine : MonoBehaviour
{

    public Rigidbody2D player;
    public Sprite castingArrow;
    public GameObject baitPrefab;

    private GameObject baitInstance;

    private SpriteRenderer castingArrowSpriteRenderer;
    private bool released = false;

    private float clearTime = 0;
    private Game.State gameState;

    // Start is called before the first frame update
    void Start()
    {
        castingArrowSpriteRenderer = this.gameObject.AddComponent<SpriteRenderer>();
        castingArrowSpriteRenderer.sprite = castingArrow;
        castingArrowSpriteRenderer.enabled = false;

        gameState = MainSceneController.GetInstance().GetState();
    }

    // Update is called once per frame
    void Update()
    {
        clearTime += Time.deltaTime;

        // Check game state
        if(clearTime > 1) {
            clearTime = 0;
            gameState = MainSceneController.GetInstance().GetState();
        }
        
        // Check if primary mouse button is down
        if (Input.GetMouseButton(0) && gameState == Game.State.CATCHING)
        {
            castingArrowSpriteRenderer.enabled = true;

            // Clear old bait instances
            if (baitInstance != null)
            {
                DestroyImmediate(baitInstance);
                baitInstance = null;
            }

            // Update casting arrow position
            castingArrowSpriteRenderer.transform.position = player.position;
            transform.position = new Vector3(transform.position.x, transform.position.y, 0.5f);

            // Calculate world point
            Vector3 worldPoint = Input.mousePosition;
            worldPoint.Set(worldPoint.x, worldPoint.y, 2);
            worldPoint = Camera.main.ScreenToWorldPoint(worldPoint);
            Vector2 worldPoint2d = new Vector2(worldPoint.x, worldPoint.y);

            // Point casting arrow to calculated position
            Vector3 vectorToTarget = new Vector3(worldPoint2d.x, worldPoint2d.y) - new Vector3(player.position.x, player.position.y);
            float angle = (Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg) + 90;
            Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = q;
        } else
        {
            castingArrowSpriteRenderer.enabled = false;
        }

        // Check if primary mouse button has been clicked and then released
        if (Input.GetMouseButtonUp(0) && gameState == Game.State.CATCHING)
        {
            released = true;
        }   
    }

    void FixedUpdate()
    {
        if(released)
        {
            released = false;

            // Clear old bait instances
            if (baitInstance != null)
            {
                DestroyImmediate(baitInstance);
                baitInstance = null;
            }     

            // New instance of bait, flying towards casting arrown pointing direction
            baitInstance = Instantiate(baitPrefab, this.transform);

            // Get and set references
            Rigidbody2D rigidbody = baitInstance.GetComponent<Rigidbody2D>();
            baitInstance.transform.position = player.transform.position;
            baitInstance.GetComponent<Line>().player = player.gameObject;

            // Calculate direction bait should fly
            Vector3 worldPoint = Input.mousePosition;
            worldPoint.Set(worldPoint.x, worldPoint.y, 0);
            worldPoint = Camera.main.ScreenToWorldPoint(worldPoint);
            Vector2 force = new Vector2(player.transform.position.x - worldPoint.x, player.transform.position.y - worldPoint.y);
            rigidbody.AddForce(force * 100);
        }
    }
}
