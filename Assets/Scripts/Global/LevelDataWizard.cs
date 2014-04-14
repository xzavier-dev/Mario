/*
================================================================================
FileName    : LevelDataWizard.cs
Description : Save level to xml file, load data from xml file and parse to Dictionary
Date        : 2014-04-14
Author      : Linkrules
================================================================================
*/
using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Xml;
using System.Collections.Generic;

public class LevelDataWizard {
    
    public delegate void LoadLevelDataCallBack(Dictionary<string,Hashtable> levelData);

    /// <summary>
    /// SaveLevel
    /// </summary>
    /// <param name="saveName">xml level name</param>
    /// <returns>XmlDoc</returns>
    static public XmlDocument SaveLevel(string saveName) {
        GameObject[] gobjs = Object.FindObjectsOfType( typeof( GameObject ) ) as GameObject[];
        XmlDocument xmlDoc = new XmlDocument();

        foreach ( GameObject obj in gobjs ) {
            if ( obj.layer != LayerMask.NameToLayer( "Element" ) ) {        // if not element layer,
                continue;
            }

            switch ( obj.name ) {
                case "Brick":

                    break;
                case "Player":

                    break;
                // -----
            }
        }

        DataCenter.SaveDataToFile( xmlDoc.InnerXml, Application.dataPath + "/Resources/LevelData/", saveName, true );
        return null;
    }

    /// <summary>
    /// LoadLevel
    /// </summary>
    /// <param name="loadName">level name of xml file</param>
    /// <returns>Dictionary<string,Hashtable> level data</returns>
    static public Dictionary<string,Hashtable> LoadLevel(string loadName) {
        Dictionary<string,Hashtable> levelData = new Dictionary<string, Hashtable>();
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.InnerXml = DataCenter.LoadDataFromResources( loadName, true );

        XmlNodeList nodeList = xmlDoc.SelectSingleNode( "root" ).ChildNodes;
        foreach ( XmlElement xe in nodeList ) {

        }
        // Parse the xmlDoc

        return levelData;
    }

    /// <summary>
    /// LoadLevel
    /// </summary>
    /// <param name="levelName">level name of xml file</param>
    /// <param name="callback">load level data by Conroutine, load finished , callback</param>
    /// <returns>null</returns>
    static public IEnumerator LoadLevel( string loadName,LoadLevelDataCallBack callback) {
        Dictionary<string,Hashtable> levelData = new Dictionary<string, Hashtable>();
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.InnerXml = DataCenter.LoadDataFromResources( loadName, true );

        XmlNodeList nodeList = xmlDoc.SelectSingleNode( "root" ).ChildNodes;
        foreach ( XmlElement xe in nodeList ) {

        }
        // Parse the xmlDoc
        
        yield return null;
        if ( callback != null ) {
            callback( levelData );
        }
    }
    

}

/*
<root>
    <Player>
        <Pos x = "0",y = "0",z = "0"/>
    </Player>
    <Brick_Dynamic>
        <Pos x = "0",y = "-1", z = "0"/>
        <isPickStuff>0</isPickStuff>
        <pickStuff>Gold</pickStuff>
        <stuffCount>5</stuffCount>
        <isBreakBrick>1</isBreakBrick>
    </Brick_Dynamic>
    <Brick_Static>
        <Pos x = "0",y = "0",z = "0"/>
    </Brick_Static>
</root>
*/