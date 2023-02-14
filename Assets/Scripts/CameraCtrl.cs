using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCtrl : MonoBehaviour
{
    private Transform _transform;
    public Transform target;

    public float dist = 7.0f; //플레이어와 거리
    public float height = 2.0f; //플레이어와 높이
    public float dampTrace = 20.0f; //부드러운 전환을 위한 변수

    // Start is called before the first frame update
    void Start()
    {
        _transform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        _transform.position = Vector3.Lerp(_transform.position, target.position - (target.forward * dist) + (Vector3.up * height), Time.deltaTime * dampTrace);
        _transform.LookAt(target.position);
    }
}
