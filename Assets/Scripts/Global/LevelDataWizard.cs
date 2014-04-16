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
    
    //public delegate void LoadLevelDataCallBack(Dictionary<string,Hashtable> levelData);
    public delegate void LoadLevelDataCallBack( List<Hashtable> levelData );
    /// <summary>
    /// In Editor mode , search all the gameobject that layer of 'Element' and save there parameter to Xml file, the parameter define in /Doc/element_params.xls
    /// </summary>
    /// <param name="saveName">xml level name</param>
    /// <returns>XmlDoc</returns>
    static public XmlDocument SaveLevel(string saveName) {
        GameObject[] gobjs = Object.FindObjectsOfType( typeof( GameObject ) ) as GameObject[];
        //SortObjByPosX( ref gobjs, 0, gobjs.Length - 1 );        // sort the gameobject in the scene according position of axis z
  
        XmlDocument xmlDoc = new XmlDocument();
        XmlElement root = xmlDoc.CreateElement( "LevelData" );
        xmlDoc.AppendChild( root );

        

        foreach ( GameObject obj in gobjs ) {
            if ( obj.layer != LayerMask.NameToLayer( "Element" ) ) {        // if not element layer,
                continue;
            }

            XmlElement objRoot = xmlDoc.CreateElement( "Obj" );
            objRoot.SetAttribute( "ObjName", obj.name );

            /*
            XmlElement objParam = xmlDoc.CreateElement( "Name" );
            objParam.InnerText = obj.name;
            objRoot.AppendChild( objParam );
            */

            XmlElement objParam;

            objParam = xmlDoc.CreateElement( "Pos" );
            objParam.InnerText = string.Format( "{0}_{1}_{2}", obj.transform.position.x, obj.transform.position.y, obj.transform.position.z );
            objRoot.AppendChild( objParam );


            switch ( obj.name ) {
                case "Brick":
                    Brick brickScript = obj.GetComponent<Brick>();

                    objParam = xmlDoc.CreateElement( "IsBreakBrick" );
                    objParam.InnerText = brickScript.isbreakBrick ? "1" : "0";
                    objRoot.AppendChild( objParam );

                    objParam = xmlDoc.CreateElement( "IsPickStuff" );
                    objParam.InnerText = brickScript.isPickStuff ? "1" : "0";
                    objRoot.AppendChild( objParam );

                    objParam = xmlDoc.CreateElement( "PickStuffName" );
                    objParam.InnerText = brickScript.pickStuffName;
                    objRoot.AppendChild( objParam );

                    objParam = xmlDoc.CreateElement( "StuffCount" );
                    objParam.InnerText = brickScript.stuffCount.ToString();
                    objRoot.AppendChild( objParam );
                    
                    break;
                case "Player":

                    break;
                
            }

            root.AppendChild( objRoot );

        }

        //xmlDoc.Save( Application.dataPath + "/Resources/LevelData/" + saveName );
        DataCenter.SaveDataToFile( xmlDoc.InnerXml, Application.dataPath + "/Resources/LevelData/", saveName, true);
        return null;
    }
   


    /// <summary>
    /// when the game begin, below function will load level data from xml file, and parse that to currect variable class
    /// Vector3, float , etc... and save all that parameters to a Hashtable, every hashtable is a gameobject, 
    /// that include Prefab name, Position, and there parameters, after that, save all the hashtable to a List, and return
    /// to LevelLoder, then it can load prefab and init the level.
    /// </summary>
    /// <param name="loadName"></param>
    /// <returns>List<Hashtable> leveldata </returns>
    static public List<Hashtable> LoadLevel( string loadName ) {
        List<Hashtable> levelData = new List<Hashtable>();
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.InnerXml = DataCenter.LoadDataFromResources( loadName, true );

        XmlNodeList nodeList = xmlDoc.SelectSingleNode( "root" ).ChildNodes;
        foreach ( XmlElement xe in nodeList ) {

        }
        // Parse the xmlDoc

        return levelData;
    }


    static public IEnumerator LoadLevel( string loadName, LoadLevelDataCallBack callback ) {
        List<Hashtable> levelData = new List<Hashtable>();
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


    /// <summary>
    /// Quick sort gameobject according position of axis z.
    /// </summary>
    /// <param name="objs">gameobject array</param>
    /// <param name="start">array index start</param>
    /// <param name="end">array index end</param>
    static private void SortObjByPosX(ref GameObject[] objs,int start,int end ) {
        if ( objs.Length <= 0 ) { return; }
        GameObject key = objs[start];
        int prev = start;
        int last = end;

        if ( start >= end ) { return; }

        while ( prev < last ) {
            // left
            while ( prev < last ) {
                if ( objs[last].transform.position.x < key.transform.position.x ) {
                    objs[prev] = objs[last];
                    ++prev;
                    break;
                }
                --last;
            }

            // right
            while ( prev < last ) {
                if ( objs[prev].transform.position.x > key.transform.position.x ) {
                    objs[last] = objs[prev];
                    --last;
                    break;
                }
                ++prev;
            }
        }

        objs[last] = key;
        SortObjByPosX( ref objs, start, last - 1 );
        SortObjByPosX( ref objs, last + 1, end );
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