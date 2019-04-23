using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EncounterTrigger : MonoBehaviour
{
    private float clearTime = 0;
    private Game.State gameState;

    void Start()
    {
        gameState = MainSceneController.GetInstance().GetState();
    }

    void Update()
    {
        clearTime += Time.deltaTime;

        // Allow some time for encounter trigger to clear
        if (clearTime > 2.5f)
        {
            clearTime = 0;
            gameState = MainSceneController.GetInstance().GetState();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // See if collision is an encounter
        ChanceEncounter encounter = collision.gameObject.GetComponent<ChanceEncounter>();
        if (encounter != null && (gameState == Game.State.CATCHING && MainSceneController.GetInstance().GetState() == Game.State.CATCHING))
        {
            Debug.Log("Encounter :: " + collision.gameObject.name);
            MainSceneController.GetInstance().SetState(Game.State.ENCOUNTER_QUERY); // Notify controller to show encounter frame
        }
    }
}
