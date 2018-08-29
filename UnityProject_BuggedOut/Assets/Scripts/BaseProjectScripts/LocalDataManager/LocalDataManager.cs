using UnityEngine;
using System.IO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

// TODO - get lloyd to flesh this out
public class LocalDataManager
{
    /// <summary>
    /// Saves an object of Serializable type T : BirdGameSettings to file in the application's persistent data path. If it fails, it will save a default(T) to file instead.
    /// </summary>
    /// <typeparam name="T">Type of the Object being serialized (must be a Serializable type).</typeparam>
    /// <param name="filename">File to save to. Will be saved in the application's persistent data path.</param>
    /// <param name="data">Object to be serialized.</param>
    /// /// <returns>Returns the object that was passed into the function (used for returning value if a new file was created in LoadSettingsFromFile)</returns>
    static object SaveObjectToFile(string filename, object data)
    {
        FileStream fs = new FileStream(Application.persistentDataPath + "/" + filename, FileMode.Create);

        try
        {
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(fs, data);
        }
        catch (SerializationException e)
        {
            Debug.LogWarning("Failed to save \"" + Application.persistentDataPath + " / " + filename + "\". Error reason: " + e.Message);
            fs.Close();
            return data;
        }

        Debug.Log("Saved data to file: " + Application.persistentDataPath + "/" + filename);
        fs.Close();
        return data;
    }

    static object LoadObjectFromFile(string filename)
    {
        FileStream fs = null;

        try
        {
            fs = new FileStream(Application.persistentDataPath + "/" + filename, FileMode.Open);
        }
        catch (FileNotFoundException e)
        {
            Debug.LogWarning("Failed to load the file named \"" + filename + "\"; creating a new one. Error reason: " + e.Message);
            if (fs != null)
            {
                fs.Close();
            }
            throw;
        }

        object data;

        try
        {
            BinaryFormatter bf = new BinaryFormatter();
            data = bf.Deserialize(fs);
        }
        catch (SerializationException e)
        {
            Debug.LogWarning("Failed to load the data from the file named \"" + filename + "\"\nReason: " + e.Message);
            if (fs != null)
            {
                fs.Close();
            }
            throw;
        }

        fs.Close();
        return data;
    }
}
