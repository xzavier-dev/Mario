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


/*
====================

 * Description: Resources folder path is 'Application.dataPath/Resources/'
====================
*/
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




    static private string key = "85645856963214585748596325412568";

    static public string EncryptData( string plainText ) {

        /*
        if ( plainText == null || plainText.Length <= 0 ) {
            return plainText;
        }

        byte[] encrypted;

        using ( RijndaelManaged rijAlg = new RijndaelManaged() ) {
            rijAlg.Key = UTF8Encoding.UTF8.GetBytes( key );
            ICryptoTransform encryptor = rijAlg.CreateEncryptor();

            using ( MemoryStream msEncrypt = new MemoryStream() ) {
                using ( CryptoStream csEncrypt = new CryptoStream( msEncrypt, encryptor, CryptoStreamMode.Write ) ) {
                    using ( StreamWriter swEncrypt = new StreamWriter( csEncrypt ) ) {
                        swEncrypt.Write( plainText );
                    }
                    encrypted = msEncrypt.ToArray();
                }
            }
        }

        return UTF8Encoding.UTF8.GetString( encrypted );
        */

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

    static public string DecryptData( string cipherText ) {
        /*
        if ( cipherText == null || cipherText.Length <= 0 ) {
            return null;
        }

        byte[] decrypt = cipherText;

        string plaintext = null;

        using ( RijndaelManaged rijAlg = new RijndaelManaged() ) {
            rijAlg.Key = UTF8Encoding.UTF8.GetBytes( key );
            ICryptoTransform decryptor = rijAlg.CreateEncryptor();

            using ( MemoryStream msDecrypt = new MemoryStream( decrypt ) ) {
                using ( CryptoStream csDecrypt = new CryptoStream( msDecrypt, decryptor, CryptoStreamMode.Read ) ) {
                    using ( StreamReader srDecrypt = new StreamReader( csDecrypt ) ) {
                        plaintext = srDecrypt.ReadToEnd();
                    }
                }
            }
        }

        plaintext = UTF8Encoding.UTF8.GetString( UTF8Encoding.UTF8.GetBytes( plaintext ) );
        return plaintext;
        */
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
