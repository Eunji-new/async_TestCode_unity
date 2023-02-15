using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class ItemManager : MonoBehaviour
{
    public Item[] Item_Data;
    public GameObject[] frames;
    List<GameObject> currentItems;

    private void Awake()
    {
        currentItems = new List<GameObject>();
    }

    private void Start()
    {
        foreach (var item in Item_Data)
            CreateItem(item._category, item._frame);
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

    public void CreateItem(Item.category category, Item.frame frame)
    {
        InstantiateItem(category, frame, GetItem(category).lat, GetItem(category).lon);
    }

    public Item GetItem(Item.category category)
    {
        return Item_Data[(int)category];
    }

    public GameObject GetFrameObj(Item.frame frame)
    {
        if(frame == Item.frame.card)
            return frames[0];
        else
            return frames[1];
    }

    public void SetText(Item.category category, GameObject frame)
    {
        frame.transform.GetChild(0).GetComponent<TextMesh>().text = category.ToString();
    }

    public void InstantiateItem(Item.category category, Item.frame frame, string lat, string lon)
    {
        float _lat = float.Parse(lat);
        float _lon = float.Parse(lon);

        Vector3 createPos = new Vector3(_lat, 0.0f, _lon);

        GameObject item = Instantiate(GetItem(category).character, createPos, Quaternion.identity) as GameObject;
        GameObject frameObj = Instantiate(GetFrameObj(frame), createPos + (Vector3.up * 2), Quaternion.identity) as GameObject;
        SetText(category, frameObj);
        currentItems.Add(item);
    }

    /*
    코루틴을 이용한 비동기 프로그래밍
    각 아이템에 맞는 오브젝트 애니메이션 함수를 실행한다.
    함수가 끝나는 것을 기다리지않고, 다른 함수들도 같이 실행된다.
    */
    public void MovingItems_Coroutine()
    {
        foreach (var item in currentItems)
        {
            StartCoroutine(PlayAnim_Coroutine(item));
            Debug.Log($"코루틴");
        }
    }

    /*
    각 아이템에 맞는 오브젝트 애니메이션 함수를 실행하고
    실행한 함수가 끝날때까지 기다린다.
    */
    public async Task MovingItems_Async_v1()
    {
        foreach (var item in currentItems)
        {
            if (item.name.Contains("Cube"))
                await MoveCube();
            else if (item.name.Contains("Sphere"))
                await MoveSphere();
            else if (item.name.Contains("Cylinder"))
                await MoveCylinder();
            else
                await MoveCapsule();

            Debug.Log($"{item.name} 애니메이션 끝남 ");
        }
    }

    /*
    각 아이템에 맞는 오브젝트 애니메이션 함수를 실행하고
    실행한 함수가 종료될 때, 해당 함수가 끝났다는 로그를 띄운다.
    함수가 끝나는 것을 기다리지않고, 다른 함수들도 같이 실행된다.
    */
    public async Task MovingItems_Async_v2()
    {
        var t0 = MoveCube();
        Debug.Log($"MoveCube");
        var t1 = MoveSphere();
        Debug.Log($"MoveSphere");
        var t2 = MoveCylinder();
        Debug.Log($"MoveCylinder");
        var t3 = MoveCapsule();
        Debug.Log($"MoveCapsule");

        List<Task> taskList = new List<Task> { t0, t1, t2, t3 };

        while (taskList.Count > 0)
        {
            Task finishedTask = await Task.WhenAny(taskList);

            if (finishedTask == t0)
            {
                Debug.Log("cube 애니메이션 끝남");
            }
            else if (finishedTask == t1)
            {
                Debug.Log("Sphere 애니메이션 끝남");
            }
            else if (finishedTask == t2)
            {
                Debug.Log("Cylinder 애니메이션 끝남");
            }
            else if (finishedTask == t3)
            {
                Debug.Log("Capsule 애니메이션 끝남");
            }

            taskList.Remove(finishedTask);
        }
    }

    public IEnumerator PlayAnim_Coroutine(GameObject obj)
    {
        
        Debug.Log($"{obj.name} 코루틴 시작");
        GetItemAnim(obj).Play();

        int seconds = 0;
        if (obj.name.Contains("Cube"))
            seconds = 1;
        else if (obj.name.Contains("Sphere"))
            seconds = 2;
        else if (obj.name.Contains("Cylinder"))
            seconds = 3;
        else
            seconds = 4;
        Debug.Log($"{obj.name} 코루틴 기다리기 전");
        yield return new WaitForSeconds(seconds);

        if (obj.name.Contains("Cube"))
            Debug.Log("cube 애니메이션 끝남");
        else if (obj.name.Contains("Sphere"))
            Debug.Log("sphere 애니메이션 끝남");
        else if (obj.name.Contains("Cylinder"))
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
        Debug.Log($"MoveCube 시작");
        if (!currentItems[0]) return;

        GetItemAnim(currentItems[0]).Play();

        Debug.Log($"MoveCube 대기 전");
        await Task.Delay(1000);
    }

    public async Task MoveSphere()
    {
        Debug.Log($"MoveSphere 시작");
        if (!currentItems[1]) return;

        GetItemAnim(currentItems[1]).Play();
        Debug.Log($"MoveSphere 대기 전");
        await Task.Delay(2000);
    }

    public async Task MoveCylinder()
    {
        Debug.Log($"MoveCylinder 시작");
        if (!currentItems[2]) return;

        GetItemAnim(currentItems[2]).Play();
        Debug.Log($"MoveCylinder 대기 전");
        await Task.Delay(3000);
    }

    public async Task MoveCapsule()
    {
        Debug.Log($"MoveCapsule 시작");
        if (!currentItems[3]) return;

        GetItemAnim(currentItems[3]).Play();
        Debug.Log($"MoveCapsule 대기 전");
        await Task.Delay(4000);
    }
    #endregion

}
