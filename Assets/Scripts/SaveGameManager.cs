using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

[System.Serializable]

public static class SaveGameManager
{
    public static void SaveHighScore(int hs)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/Rage.kt";
        //Debug.Log("Path Created");
        FileStream stream = new FileStream(path, FileMode.OpenOrCreate);
        //FileStream stream2 = new FileStream(path, FileMode.Open);

        int highScoreInFile = hs;

        if (File.Exists(path) && stream.Length > 0)
        {
            stream.Close();
            Debug.Log("File Exists");
            FileStream stream2 = new FileStream(path, FileMode.Open);
            int? tmphighScoreInFile = formatter.Deserialize(stream2) as int?;
             stream2.Close();
            if (tmphighScoreInFile <= highScoreInFile)
            {
                //stream2.Close();
                FileStream stream3 = new FileStream(path, FileMode.Create); 
                formatter.Serialize(stream3, highScoreInFile);
                Debug.Log("File Created :  "+tmphighScoreInFile);
                stream3.Close();
            }
        }
        else
        {
            formatter.Serialize(stream, highScoreInFile);
            stream.Close();
        }
        //stream2.Close();
    }

    public static int? loadHighScore()
    {
        string path = Application.persistentDataPath + "/Rage.kt";
        if(File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path,FileMode.Open);
            //stream.
            int? highScoreInFile = formatter.Deserialize(stream) as int?;
            stream.Close();

            return highScoreInFile;
        }

        else
        {
            Debug.Log("File Not Found");
            return 0;
        }
    }
}
