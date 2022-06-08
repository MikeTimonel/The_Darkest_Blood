using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveManager
{
    public static void SavePlayerData(PlayerData player){
        string dataPath = Application.persistentDataPath + "/player.data";
        FileStream fileStream = new FileStream(dataPath, FileMode.Create);
        BinaryFormatter formatter = new BinaryFormatter();
        formatter.Serialize(fileStream, player);
        fileStream.Close();
    }

    public static PlayerData LoadPlayerData(){
        string dataPath = Application.persistentDataPath + "/player.data";
        if(File.Exists(dataPath)){
            FileStream fileStream = new FileStream(dataPath, FileMode.Open);
            BinaryFormatter formatter = new BinaryFormatter();
            PlayerData playerData = (PlayerData) formatter.Deserialize(fileStream);
            fileStream.Close();
            return playerData;
        }else{
            Debug.LogError("No hay datos guardados o no se ha podido encontrar el archivo player.data");
            return null;
        }
    }
}
