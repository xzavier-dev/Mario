
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelLoader : MonoBehaviour {
    LevelLoadMode_t levelLoadMode = LevelLoadMode_t.ACCORDINGPLAYERPOS;
    /// <summary>
    /// level data parsed from level xml data, Hashtable has the gameobject params, like position, or saved script variables.
    /// </summary>
    List<Hashtable> levelData = new List<Hashtable>();
    
    float minDistant = 10.0f;

    Transform player = null;

    bool isAvaliableLevelData = false;
    Vector3 lastObjPos = Vector3.zero;
    Hashtable lastObjData = null;

    void Start() {
        levelData = LevelDataWizard.LoadLevel( GlobalManager.instance.globalGameController.playerData.currentLevelName );
        isAvaliableLevelData = true;
        
        //StartCoroutine( LevelDataWizard.LoadLevel( GlobalManager.instance.globalGameController.playerData.currentLevelName, LevelDataReady ) );
    }

    void LevelDataReady( List<Hashtable> data ) {
        levelData = data;
        isAvaliableLevelData = true;
    }
    
    void Update() {
        if ( !isAvaliableLevelData ) { return; }
        if ( levelData.Count == 0 ) { return; }

        lastObjData = levelData[0];
        
        if( lastObjData == null ){
            // parse the obj data
            lastObjData = levelData[0];
            lastObjPos = (Vector3)lastObjData["Pos"];
        }

        if ( IsReadyToInstantiate(lastObjPos.x) ) {
            GameObject newObj = Instantiate( Resources.Load( "Elements/" + lastObjData["ObjName"] ) ) as GameObject;
            newObj.transform.position = lastObjPos;

            switch ( newObj.name ) {
                case "Player":
                    player = newObj.transform;
                    // -------- other params
                    break;
            }

            lastObjData = null;
            levelData.RemoveAt( 0 );

        } 
    }

    bool IsReadyToInstantiate(float x) {
        if ( player == null ) {
            if ( x < minDistant ) {
                return true;
            } else {
                return false;
            }
        } else {
            if ( x - player.position.x < minDistant ) {
                return true;
            } else {
                return false;
            }
        }
    }

}
