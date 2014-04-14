using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelLoader : MonoBehaviour {
    LevelLoadMode_t levelLoadMode = LevelLoadMode_t.ACCORDINGPLAYERPOS;
    /// <summary>
    /// level data parsed from level xml data, Hashtable has the gameobject params, like position, or saved script variables.
    /// </summary>
    Dictionary<string,Hashtable> levelData = new Dictionary<string, Hashtable>();

    bool isAvaliableLevelData = false;
    Vector3 lastObjPos = Vector3.zero;
    GameObject lastObj = null;

    void Start() {
        levelData = LevelDataWizard.LoadLevel( GlobalManager.instance.globalGameController.playerData.currentLevelName );
        isAvaliableLevelData = true;

        //StartCoroutine( LevelDataWizard.LoadLevel( GlobalManager.instance.globalGameController.playerData.currentLevelName, LevelDataReady ) );
    }

    void LevelDataReady( Dictionary<string, Hashtable> data ) {
        levelData = data;
        isAvaliableLevelData = true;
    }
    
    void Update() {
        if ( !isAvaliableLevelData ) { return; }
        if ( levelData.Count == 0 ) { return; }
        
        if( lastObj == null ){
            // parse the obj data

        } else {
            // check the distant to player

        }      

    }
}
