using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Xml;

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
        if ( GUILayout.Button( "Save As New Level" ,GUILayout.Height(30)) ) {
            if ( levelName == "" ) {
                Debug.LogError( " You should enter a level name" );
                return;
            } else {
                SaveLevel( false );
            }
        
        }
        if ( GUILayout.Button( "Decrypt", GUILayout.Height( 30 ) ) ) {
            if ( levelName == "" ) {
                Debug.LogError( " You should enter a level name" );
                return;
            } else {
                DecryptLevelDataFile();
                levelName = "";
            }
        }
        GUILayout.EndVertical();
    }


   
    

    void DrawEditPanel() {
    

        GUILayout.Label( "" );        

        if ( GUILayout.Button( "Save Level", GUILayout.Height( 30 ) ) ) {
            SaveLevel( false );            
        }

        GUILayout.Label("");

        levelSaveAsName = GUILayout.TextField( levelSaveAsName,GUILayout.Height(30) );
        GUILayout.BeginHorizontal();
        if ( GUILayout.Button( "Save As",GUILayout.Height(30) ) ) {
            SaveLevel( true );
        }
        if ( GUILayout.Button( "Clean", GUILayout.Height(30) ) ) {
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
            LevelDataWizard.SaveLevel( levelName );
            AssetDatabase.Refresh();
        }

    }

    void LoadLevel() {

    }

    void DecryptLevelDataFile() {
        string data = DataCenter.LoadDataFromFile( Application.dataPath + "/Resources/LevelData/", levelName, true );
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.InnerXml = data;
        xmlDoc.Save( Application.dataPath + "/Resources/LevelData/" + levelName + "_dc.xml" );
        AssetDatabase.Refresh();
        
        
    }

    

}
