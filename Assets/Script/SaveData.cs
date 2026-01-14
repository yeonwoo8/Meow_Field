using System.Collections.Generic;

[System.Serializable]
public class SaveData
{
    public int coin;
    public float gameTime;
    public List<SerializableItem> inventoryItems;
}

[System.Serializable]
public class SerializableItem
{
    public string itemName;
    public int quantity;
}
