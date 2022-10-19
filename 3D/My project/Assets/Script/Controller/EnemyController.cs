using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private List<Vector3> PointList = new List<Vector3>();

    [SerializeField] private LayerMask TargetMask;

    private float Radius;
    float Angle = 0.0f;

    [Tooltip("장애물 감지 선의 수")]
    [Range(5, 30)]
    private int Count;

    void Start()
    {
        Radius = 15.0f;
        Count = 20;
    }
    void Update()
    {
        Angle = transform.eulerAngles.y - 45.0f;
        PointList.Clear();

        for (int i = 0; i < Count; ++i)
        {
            PointList.Add(new Vector3(Mathf.Sin(Angle * Mathf.Deg2Rad), 0.0f, Mathf.Cos(Angle * Mathf.Deg2Rad)));

            Angle += 90.0f/(Count-1);
        }

        float fAngle = 0.0f;
        Ray ray = new Ray();
        for (int i= 0; i < PointList.Count; ++i)
        {
            ray = new Ray(transform.position, PointList[i].normalized);

            RaycastHit hit;
            if (Physics.Raycast(ray.origin, PointList[i].normalized, out hit, Radius, TargetMask))
            {
                    fAngle = Vector3.Angle(transform.forward, PointList[i]);

                    fAngle *= (i > ((int)(PointList.Count * 0.5f)-1)) ? -2 : 2;
            }
        }
        transform.Rotate(transform.up * fAngle * Time.deltaTime);

        transform.position += transform.forward * 5.0f * Time.deltaTime;

        foreach (Vector3 Point in PointList)
        {
            Debug.DrawLine(ray.origin, // 시작점
                 ray.origin + (Point.normalized * Radius) + transform.position, // 도착지점
                 Color.red);   // 라인 색
        }
    }
}
