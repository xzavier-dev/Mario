using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    private bool isAnotherLevelReady = false;
    private string newLevelName = "";
    private bool isLoadingNewLevel = false;
	// Use this for initialization
	void Start () {
        GlobalManager.instance.globalGameController.gameOverEvent += GameOver;
	
	}

    void OnDestroy() {
        GlobalManager.instance.globalGameController.gameOverEvent -= GameOver;
    }


    /// <summary>
    /// when i am dead the below function will be called
    /// </summary>
    void GameOver() {
        Debug.Log( "i am " + transform.name + " Dead" );
        gameObject.SetActive( false );

    }
    

    /// <summary>
    /// when i be killed , i will tell the globalGameController, then ,it will broadcast game over event.
    /// </summary>
    void BeKilled() {
        if ( !GlobalManager.instance.globalGameController.IsGameOver() ) {
            GlobalManager.instance.globalGameController.SetGameOver();
        }
    }

    /// <summary>
    /// when player stand on the pipeline, and press down , the pileline will send me message to call the below function
    /// that tell me , i can go to new level
    /// </summary>
    /// <param name="levelName"></param>
    void SetNewLevelReady( string levelName ) {
        isAnotherLevelReady = true;
        newLevelName = levelName;
    }

    void SetCancelNewLevelReady() {
        isAnotherLevelReady = false;
    }


    void Update() {
        if ( isAnotherLevelReady && !isLoadingNewLevel && (Input.GetKeyDown( KeyCode.S ) || Input.GetKeyDown( KeyCode.DownArrow ) )){
            isLoadingNewLevel = true;
            StartCoroutine(GoingToNewLevel());
        }

    }


    /// <summary>
    /// when player stand on pipeline, and pressed down button, then , he can go to new level
    /// </summary>
    /// <returns></returns>
    IEnumerator GoingToNewLevel() {

        // wait a moment,play some animation.

        GlobalManager.instance.globalGameController.LoadNewLevel( newLevelName );
        yield return null;
    }


    
}
