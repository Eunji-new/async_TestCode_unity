using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public Item[] Item_Data;
    List<GameObject> currentItems;

    private void Awake()
    {
        currentItems = new List<GameObject>();
    }

    private void Start()
    {
        foreach (var item in Item_Data)
            CreateItem(item._Category);
    }

    private async void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            await MovingItems_Async();
            Debug.Log("MovingItems_Async 끝난 후에 발생하는 로그.");
        }
        else if (Input.GetKeyDown(KeyCode.B))
        {
            StartCoroutine(MovingItems_Couroutine());
            Debug.Log("MovingItems_Couroutine가 시작된 후(MovingItems_Couroutine는 진행중이다.).");
        }
    }

    public void CreateItem(Item.category category)
    {
        InstantiateItem(category);
    }

    public Item GetItem(Item.category category)
    {
        return Item_Data[(int)category];
    }

    public void InstantiateItem(Item.category category)
    {
        float lat = float.Parse(GetItem(category).lat);
        float lon = float.Parse(GetItem(category).lon);

        Vector3 createPos = new Vector3(lat, 0.0f, lon);

        GameObject item = Instantiate(GetItem(category).character, createPos, Quaternion.identity) as GameObject;
        currentItems.Add(item);
    }

    public async Task MovingItems_Async()
    {
        foreach (var item in currentItems)
        {
            MovingItem(item);
            await Task.Delay(1000);
        }

    }

    IEnumerator MovingItems_Couroutine()
    {
        foreach (var item in currentItems)
        {
            MovingItem(item);
            yield return new WaitForSeconds(1);
        }
    }

    public void MovingItem(GameObject item)
    {
        GetItemAnim(item).Play();
    }

    public Animation GetItemAnim(GameObject item)
    {
        return item.GetComponent<Animation>();
    }
}
