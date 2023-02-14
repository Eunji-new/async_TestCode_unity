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
            CreateItem(item._category);
    }

    private async void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            MovingItems_Coroutine();
            Debug.Log("MovingItems_Couroutine가 시작된 후(MovingItems_Couroutine는 진행중이다)");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            await MovingItems_Async_v1();
            Debug.Log("MovingItems_Async_v1 끝난 후에 발생하는 로그(사실상 비동기)");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            await MovingItems_Async_v2();
            Debug.Log("MovingItems_Async_v2 끝난 후에 발생하는 로그");
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

    public void MovingItems_Coroutine()
    {
        foreach(var item in currentItems)
        {
            StartCoroutine(PlayAnim_Coroutine(item));
        }
    }
  
    public async Task MovingItems_Async_v1()
    {   
        foreach(var item in currentItems)
        {
            if(item.name.Contains("Cube"))
                await MoveCube();
            else if(item.name.Contains("Sphere"))
                await MoveSphere();
            else if(item.name.Contains("Cylinder"))
                await MoveCylinder();
            else
                await MoveCapsule();
        }
    }

    public async Task MovingItems_Async_v2()
    {
        var t0 = MoveCube();
        var t1 = MoveSphere();
        var t2 = MoveCylinder();
        var t3 = MoveCapsule();

        List<Task> taskList = new List<Task>{t0, t1, t2, t3};

        while(taskList.Count > 0)
        {
            Task finishedTask = await Task.WhenAny(taskList);
            
            taskList.Remove(finishedTask);
        }
    }
  
    public IEnumerator PlayAnim_Coroutine(GameObject obj)
    {
        GetItemAnim(obj).Play();

        int seconds = 0;
        if(obj.name.Contains("Cube"))
            seconds = 1;
        else if(obj.name.Contains("Sphere"))
            seconds = 2;
        else if(obj.name.Contains("Cylinder"))
            seconds = 3;
        else
            seconds = 4;

        yield return new WaitForSeconds(seconds);

        if(obj.name.Contains("Cube"))
            Debug.Log("cube 애니메이션 끝남");
        else if(obj.name.Contains("Sphere"))
            Debug.Log("sphere 애니메이션 끝남");
        else if(obj.name.Contains("Cylinder"))
            Debug.Log("cylinder 애니메이션 끝남");
        else
            Debug.Log("capsule 애니메이션 끝남");

    }

    public Animation GetItemAnim(GameObject obj)
    {
        return obj.GetComponent<Animation>();
    }

#region Move Animation
    public async Task MoveCube()
    {
        if(!currentItems[0]) return;

        GetItemAnim(currentItems[0]).Play();
        await Task.Delay(1000);
        Debug.Log("cube 애니메이션 끝남");
    }

    public async Task MoveSphere()
    {
        if(!currentItems[1]) return;

        GetItemAnim(currentItems[1]).Play();
        await Task.Delay(2000);
        Debug.Log("Sphere 애니메이션 끝남");
    }

    public async Task MoveCylinder()
    {
        if(!currentItems[2]) return;

        GetItemAnim(currentItems[2]).Play();
        await Task.Delay(3000);
        Debug.Log("Cylinder 애니메이션 끝남");
    }

    public async Task MoveCapsule()
    {
        if(!currentItems[3]) return;

        GetItemAnim(currentItems[3]).Play();
        await Task.Delay(4000);
        Debug.Log("Capsule 애니메이션 끝남");
    }
#endregion

}
