using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EncounterTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // See if collision is an encounter
        ChanceEncounter encounter = collision.gameObject.GetComponent<ChanceEncounter>();
        if (encounter != null)
        {
            Debug.Log("Encounter :: " + collision.gameObject.name);
            MainSceneController.GetInstance().SetState(Game.State.ENCOUNTER_QUERY); // Notify controller to show encounter frame
        }
    }
}
