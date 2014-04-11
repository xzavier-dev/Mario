using UnityEngine;
using System.Collections;

public class TestRead : MonoBehaviour {

    string data;
	// Use this for initialization
	void Start () {

        StartCoroutine( DataCenter.LoadDataFromResources( "LevelData/level_001", true, DataLoadOK ) );

       //DataCenter.SaveDataToFile( "this is a test", Application.dataPath + "/Resources/LevelData/", "level_001.txt", true);

        
	
	}

    void DataLoadOK( string str ) {
        data = str;
        
    }

    void OnGUI() {
        GUILayout.Label( data );
    }
}
