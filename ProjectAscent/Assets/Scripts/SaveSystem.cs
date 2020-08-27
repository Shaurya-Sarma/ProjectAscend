using System.IO;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{

  public static void SavePlayer()
  {
    BinaryFormatter formatter = new BinaryFormatter();
    string path = Application.persistentDataPath + "/player.data";
    using (FileStream stream = new FileStream(path, FileMode.Create))
    {
      PlayerData data = new PlayerData();
      formatter.Serialize(stream, data);
      Debug.Log("Path" + path);
    }

  }

  public static PlayerData LoadPlayer()
  {
    string path = Application.persistentDataPath + "/player.data";
    if (File.Exists(path))
    {
      BinaryFormatter formatter = new BinaryFormatter();
      using (FileStream stream = new FileStream(path, FileMode.Open))
      {
        PlayerData data = formatter.Deserialize(stream) as PlayerData;
        return data;
      }
    }
    else
    {
      Debug.LogError("Save file not found in" + path);
      return null;
    }

  }
}
