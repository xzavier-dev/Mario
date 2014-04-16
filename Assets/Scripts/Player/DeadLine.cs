using UnityEngine;
using System.Collections;

public class DeadLine : MonoBehaviour {
    public float deadLine = -5f;
	
	
	// Update is called once per frame
	void Update () {
        if ( transform.position.y <= deadLine && !GlobalManager.instance.globalGameController.IsGameOver()) {
            GlobalManager.instance.globalGameController.SetGameOver();
        }
	
	}
}
