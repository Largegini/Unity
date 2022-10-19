using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
   [HideInInspector] public static GameManager Instance = null;

    private GameObject JammoPointObject;

    private GameManager() { }

    // 만들고 찾기
    private void Awake()
    {
        JammoPointObject = Resources.Load("Points/JammoPoint") as GameObject;

        new GameObject("PointList");

        if (Instance == null)
            Instance = this;

        // 다음 씬에도 데이터를 지우지 않고 넘어갈수 있게 해줌
        DontDestroyOnLoad(this);
    }

    private int Count = 0;

    // 초기화
    // 시작은 설정단계로 Update보다 빠르다
    private IEnumerator Start()
    {
        Count = 1;

        // Update와 호출시점이 같다
        while(true)
        {
            //yield return new WaitForSeconds(Time.deltaTime);
            yield return null;

            GameObject Obj = Instantiate(JammoPointObject);

            Obj.transform.position = new Vector3(
                Random.Range(-25.0f, 25.0f),
               Random.Range(10.0f, 20.0f),
                Random.Range(-25.0f, 25.0f));

            Obj.transform.name = Count.ToString();

            Obj.transform.parent = GameObject.Find("PointList").transform;

            if (Count >= 10)
                break;

            ++Count;
        }
    }
}
