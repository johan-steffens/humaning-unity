using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastLine : MonoBehaviour
{

    public Rigidbody2D player;
    public Sprite castingArrow;

    private SpriteRenderer castingArrowSpriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        castingArrowSpriteRenderer = this.gameObject.AddComponent<SpriteRenderer>();
        castingArrowSpriteRenderer.sprite = castingArrow;
        castingArrowSpriteRenderer.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Check if primary mouse button is down
        castingArrowSpriteRenderer.enabled = Input.GetMouseButton(0);

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
    }
}
