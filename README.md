# async_TestCode_unity
Unity에서 async-await를 이용한 비동기 프로그래밍 예제

1. 정해진 위치에 아이템이 생성됩니다.
    * 아이템 총 4 개 : cube, cylinder, sphere, capsule 오브젝트

2. 각 오브젝트의 애니메이션을 실행시킵니다.

    (cube -> sphere -> cylinder -> capsule 순)

3. 1번키, 2번키, 3번키를 눌러 각각의 작동방식을 확인할 수 있습니다.
    * 1번키 : 코루틴 이용
    * 2번키 : async 이용(하지만 사실상 비동기)
    * 3번키 : async 이용 비동기 구현


<br>

# 결과

- 디폴트 상태(가운데 하얀색 캡슐은 Player를 뜻함)
![image](https://user-images.githubusercontent.com/28985207/218659247-59f424d7-b9bf-4402-9909-167ab6d4c04b.png)


- 방식 1, 3 : 오브젝트 4개 애니메이션이 동시에 시작
![image](https://user-images.githubusercontent.com/28985207/218660219-213a2eab-edbf-4986-acb6-fdc9671f2405.png)

- 방식 2 : 1개의 오브젝트 애니메이션이 모두 끝난 후에 다음 오브젝트 애니메이션 시작 
![image](https://user-images.githubusercontent.com/28985207/218660366-40f74664-a985-410a-ab45-8b8f343d4bb7.png)
