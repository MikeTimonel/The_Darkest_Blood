[System.Serializable]

public class PlayerData
{
    // variables de los datos que se quieren guardar
    public int potions ; //# de pociones que cuenta el jugador
    public int scene; // # de la escena en la que se encuentra

    public PlayerData(int numberPotions, int numberScene){
        potions = numberPotions;
        scene = numberScene;
    }
}
