using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Spawner : MonoBehaviour {
    static private List<string> cacheObjectNames = new List<string>();
    static private List<GameObject> cache = new List<GameObject>();

    static public GameObject GetObjectByName( string objName ) {
        int objIndex = GetIndexOfObjectByName( objName );
        if ( objIndex != -1 ) {
            return cache[objIndex];
        }

        GameObject newObj = Resources.Load( objName ) as GameObject;
        if ( newObj != null ) {
            cacheObjectNames.Add( objName );
            cache.Add( newObj );
            return newObj;
        } else {
            Debug.LogError( "Load GameObject Faild!!!, Please check the object name." );
            return null;
        }
    }

    static private int GetIndexOfObjectByName(string objName){
        for (int i = 0; i < cacheObjectNames.Count; ++i){
            if( cacheObjectNames[i] == objName ){
                return i;
            }
        }
        return -1;
    }
    
}
