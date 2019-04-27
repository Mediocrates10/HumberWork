using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem{

    public static string playerPath;
    public static string gamePath;

    public static void SavePlayer (GodStats god)
    {
        playerPath = Application.persistentDataPath + "/player.save";
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(playerPath, FileMode.Create);

        PlayerData data = new PlayerData(god);

        formatter.Serialize(stream, data);

        stream.Close();
    }

    public static PlayerData LoadPlayer ()
    {
        playerPath = Application.persistentDataPath + "/player.save";
        if (File.Exists(playerPath))
        {           
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(playerPath, FileMode.Open);

            PlayerData data = formatter.Deserialize(stream) as PlayerData;

            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError("Save file not found in " + playerPath);
            return null;
        }
    }

    public static void SaveGame (PauseGame gm)
    {
        gamePath = Application.persistentDataPath + "/game.save";
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(gamePath, FileMode.Create);

        GameData data = new GameData(gm);

        formatter.Serialize(stream, data);

        stream.Close();
    }

    public static GameData LoadGame()
    {
        gamePath = Application.persistentDataPath + "/game.save";
        if (File.Exists(gamePath))
        {          
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(gamePath, FileMode.Open);

            GameData data = formatter.Deserialize(stream) as GameData;

            stream.Close();

            return data;
        }

        else
        {
            Debug.LogError("Save file not found in " + gamePath);
            return null;
        }
    }

}
