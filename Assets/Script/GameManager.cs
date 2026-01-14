using UnityEngine;
using System.Collections.Generic;
using UnityEditor.Overlays;
using UnityEditor.UIElements;
using System.IO;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    public static int hours;
    public static int minutes;
    public static int coin;

    public Dictionary<string, int> itemData = new Dictionary<string, int>();
    public static float gameTime = 25200; // 오전 7시 (7 * 3600)

    public static float gameDuration = 10f; // 10초에 1시간

    public List<Item> inventoryItems = new List<Item>(); // 인벤토리 아이템 리스트

    // GameManager.cs
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

            LoadItems();
            coin = 100;  // 코인 초기값

            // 시간 초기화
            hours = 7;         // 예: 아침 7시부터 시작
            minutes = 0;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        gameTime += (Time.deltaTime / gameDuration) * 3600;
        hours = (int)(gameTime / 3600) % 24;
        minutes = (int)(gameTime % 3600) / 60;
    }

    public void AddItem(Item newItem)
    {
        foreach (var item in inventoryItems)
        {
            if (item.itemName == newItem.itemName)
            {
                item.quantity++;

                // itemData에도 반영
                if (itemData.ContainsKey(item.itemName))
                    itemData[item.itemName]++;
                else
                    itemData[item.itemName] = item.quantity;

                return;
            }
        }

        newItem.quantity = 1;
        inventoryItems.Add(newItem);

        // itemData에도 추가
        itemData[newItem.itemName] = newItem.quantity;
    }

    void LoadItems()
    {


        // Resources/Items 폴더에 아이템 프리팹 또는 스크립터블 오브젝트가 있다고 가정
        Item[] loadedItems = Resources.LoadAll<Item>("Items");

        inventoryItems.Clear();

        foreach (Item item in loadedItems)
        {
            if (item != null)
                inventoryItems.Add(item);
        }

        Debug.Log($"LoadItems() - 아이템 총 {inventoryItems.Count}개 로드됨");
    }
    public void RemoveItem(Item item)
    {
        if (inventoryItems.Contains(item))
        {
            inventoryItems.Remove(item);
        }
    }

    public void SaveGame()
    {
        SaveData data = new SaveData();

        data.coin = coin;
        data.gameTime = gameTime;

        if (inventoryItems.Count==0)
        {
            return;
        }
        data.inventoryItems = new List<SerializableItem>();
        foreach(var item in inventoryItems)
        {
            SerializableItem sItem = new SerializableItem
            {
                itemName = item.itemName,
                quantity = item.quantity
            };
            data.inventoryItems.Add(sItem);
        }

        string json = JsonUtility.ToJson(data, true);
        string path = Application.persistentDataPath + "/save.json";
        System.IO.File.WriteAllText(path, json);
        Debug.Log("게임 저장 완료! " + path);
    }

    public void LoadGame()
    {
        string path = Application.persistentDataPath + "/save.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            coin = data.coin;
            gameTime = data.gameTime;

            inventoryItems.Clear();
            itemData.Clear();

            foreach( var sItem in data.inventoryItems)
            {
                Item prefab = Resources.Load<Item>("Items/" + sItem.itemName);

                if (prefab != null)
                {
                    Item newItem = Instantiate(prefab);
                    newItem.quantity = sItem.quantity;
                    inventoryItems.Add(newItem);

                    itemData[newItem.itemName] = newItem.quantity;
                }
                else
                {
                    Debug.LogWarning("LoadGame() - 아이템을 찾을 수 없음: " + sItem.itemName);
                }
            }

            Debug.Log("게임 불러오기 완료!");
        }
        else
        {
            Debug.Log("저장 파일이 없음");
        }
    }

}