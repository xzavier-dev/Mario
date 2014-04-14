/*
================================================================================
FileName    : 
Description : 
Date        : 2014-03-17
Author      : Linkrules
================================================================================
*/
using UnityEngine;
using System.Collections;
using System.IO;
using System.Security.Cryptography;
using System.Xml;
using System.Text;
using System;

public class DataCenter {

    public delegate void DataLoadFinished( string data );

    /// <summary>
    /// Load file from streammingAssetsPath by StreamReader
    /// </summary>
    /// <param name="path"></param>
    /// <param name="fileName"></param>
    /// <param name="isEncrypted"></param>
    /// <returns></returns>
    static public string LoadDataFromFile( string path, string fileName, bool isEncrypted ) {
        path += fileName;
        if ( !File.Exists( path ) ) {
            Debug.Log( path + " Does not exists!" );
            return null;
        }

        StreamReader reader = File.OpenText( path );
        string data = reader.ReadToEnd();

        if ( isEncrypted ) {
            data = DecryptData( data );
        }

        return data;
    }


    /// <summary>
    /// Load file from streammingAssetsPath by StreamReader , but that is using Coroutine
    /// </summary>
    /// <param name="path"></param>
    /// <param name="fileName"></param>
    /// <param name="isEncrypted"></param>
    /// <param name="callback"></param>
    /// <returns></returns>
    static public IEnumerator LoadDataFromFile( string path, string fileName, bool isEncrypted, DataLoadFinished callback ) {
        path += fileName;

        string data = null;

        if ( File.Exists( path ) ) {
            StreamReader reader = File.OpenText( path );
            data = reader.ReadToEnd();
            if ( isEncrypted ) {
                data = DecryptData( data );
            }
        }

        if ( callback != null ) {
            callback( data );
        }

        yield return null;
    }


    /// <summary>
    /// Load data from Resources folder by TextAsset and using Coroutine
    /// </summary>
    /// <param name="fileName"></param>
    /// <param name="isEncrypted"></param>
    /// <param name="callback"></param>
    /// <returns></returns>
    static public IEnumerator LoadDataFromResources( string fileName, bool isEncrypted, DataLoadFinished callback ) {
        TextAsset textAsset = (TextAsset)Resources.Load( fileName );
        
        string finalData = null;
        if ( isEncrypted ) {
            finalData = DecryptData( textAsset.text );
        } else {
            finalData = textAsset.text;
        }

        callback( finalData );
        yield return null;
    }

    /// <summary>
    /// load data from Resources folder
    /// </summary>
    /// <param name="fileName"></param>
    /// <param name="isEncrypted"></param>
    /// <returns></returns>
    static public string LoadDataFromResources( string fileName, bool isEncrypted ) {
        TextAsset textAsset = (TextAsset)Resources.Load( fileName );

        string finalData = null;
        if ( isEncrypted ) {
            finalData = DecryptData( textAsset.text );
        } else {
            finalData = textAsset.text;
        }

        return finalData;
    }


    /// <summary>
    /// Save data to file , you can save to streammingAssets or Resources folder
    /// </summary>
    /// <param name="data"></param>
    /// <param name="path"></param>
    /// <param name="fileName"></param>
    /// <param name="isEncrypt"></param>
    static public void SaveDataToFile( string data, string path, string fileName, bool isEncrypt ) {
        if ( !Directory.Exists( path ) ) {
            Directory.CreateDirectory( path );
        }

        if ( isEncrypt ) {
            data = EncryptData( data );
        }

        path += fileName;

        if ( File.Exists( path ) ) {
            File.Delete( path );
        }


        StreamWriter writer = File.CreateText( path );
        writer.Write( data );
        writer.Close();
    }





    static private string key = "85645856963214585748596325412568";
    /// <summary>
    /// EncryptData
    /// </summary>
    /// <param name="plainText"></param>
    /// <returns></returns>
    static public string EncryptData( string plainText ) {
        byte[] keyArray = UTF8Encoding.UTF8.GetBytes( key );
        RijndaelManaged rDel = new RijndaelManaged();
        rDel.Key = keyArray;
        rDel.Mode = CipherMode.ECB;
        rDel.Padding = PaddingMode.PKCS7;
        ICryptoTransform cTransform = rDel.CreateEncryptor();

        byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes( plainText );
        byte[] resultArray = cTransform.TransformFinalBlock( toEncryptArray, 0, toEncryptArray.Length );

        return Convert.ToBase64String( resultArray, 0, resultArray.Length );
    }

    /// <summary>
    /// DecryptData
    /// </summary>
    /// <param name="cipherText"></param>
    /// <returns></returns>
    static public string DecryptData( string cipherText ) {
        byte[] keyArray = UTF8Encoding.UTF8.GetBytes( key );

        RijndaelManaged rDel = new RijndaelManaged();
        rDel.Key = keyArray;
        rDel.Mode = CipherMode.ECB;
        rDel.Padding = PaddingMode.PKCS7;
        ICryptoTransform cTransform = rDel.CreateDecryptor();

        byte[] toEncryptArray = Convert.FromBase64String( cipherText );
        byte[] resultArray = cTransform.TransformFinalBlock( toEncryptArray, 0, toEncryptArray.Length );

        return UTF8Encoding.UTF8.GetString( resultArray );

    }

}
