using UnityEngine;
using System.Collections;

public class ThresholdEvent : MonoBehaviour {

    public ParticleSystem parSys;
    public int pointBoost;
    bool entered = false;

	void OnTriggerEnter2D(Collider2D col)
    {
        if (!entered && !col.CompareTag("Untagged"))
        {
            //if (col.rigidbody2D.velocity == Vector2.zero && col.rigidbody2D.gravityScale == 1)
            //{
                Debug.Log("Entered threshold.");
                col.GetComponent<ScoreScript>().AddScore(pointBoost);
                entered = true;
            //}
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        //nothing.
    }
}
