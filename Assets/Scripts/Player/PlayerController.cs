using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GlobalManager.instance.globalGameController.gameOverEvent += GameOver;
	
	}

    void OnDestroy() {
        GlobalManager.instance.globalGameController.gameOverEvent -= GameOver;
    }

    void GameOver() {
        Debug.Log( "i am " + transform.name + " Dead" );
        gameObject.SetActive( false );

    }

    void BeKilled() {
        if ( !GlobalManager.instance.globalGameController.IsGameOver() ) {
            GlobalManager.instance.globalGameController.SetGameOver();
        }
    }

    
}
