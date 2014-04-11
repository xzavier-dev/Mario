using UnityEngine;
using System.Collections;
using UnityEditor;

public class LevelEditor : EditorWindow{
    [MenuItem( "Game Tool/Level Editor" )]
    static void ActiveEditor() {
        EditorWindow.GetWindow<LevelEditor>( false, "Level Editor", true );
    }

    static public string levelName = "";
    static public string levelSaveAsName = "";
    static private bool isEdit = false;

    void OnGUI() {

        if ( !isEdit ) {
            DrawMenuPanel();
        } else {
            DrawEditPanel();
        }
    }

    void DrawMenuPanel() {
        GUILayout.Label( "" );
        GUILayout.BeginVertical();

        levelName = GUILayout.TextField( levelName ,GUILayout.Height(30));
        if ( GUILayout.Button( "Open Level",GUILayout.Height(30)) ) {

            if ( levelName == "" ) {
                Debug.LogError( "Level name is null" ); 
                return;
            }

            isEdit = true;

        }
        GUILayout.EndVertical();
    }

    void DrawEditPanel() {

        GUILayout.Label( "" );        

        if ( GUILayout.Button( "Save Level", GUILayout.Height( 30 ) ) ) {
            SaveLevel( false );            
        }

        GUILayout.Label("");

        GUILayout.BeginHorizontal();
        levelSaveAsName = GUILayout.TextField( levelSaveAsName,GUILayout.Height(30) );
        if ( GUILayout.Button( "Save As", GUILayout.Width( 100 ),GUILayout.Height(30) ) ) {
            SaveLevel( true );
        }
        if ( GUILayout.Button( "Clean", GUILayout.Width( 100 ),GUILayout.Height(30) ) ) {
            Clean();
        }
        GUILayout.EndHorizontal();

    }
    
    
    void Clean() {
        isEdit = false;
        levelName = "";
        levelSaveAsName = "";
    }


    // =======================================================  Follow is data processing  =======================================================

    void SaveLevel( bool isSaveAs ) {

        if ( isSaveAs ) {
            if ( levelSaveAsName == "" ) { return; }



        } else {

        }

    }

    void LoadLevel() {

    }


    

}
