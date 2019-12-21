[System.Serializable]
public class PlayerData
{
    public float[] position;

    public PlayerData(PlayerController player)
    {
        position = new float[5];
        position[0] = player.transform.position.x;
        position[1] = player.transform.position.y;
        position[2] = player.transform.position.z;
        position[3] = player.jumpNumber;
        position[4] = player.fallNumber;
    }
 

}
