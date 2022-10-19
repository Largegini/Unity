using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
   [HideInInspector] public static GameManager Instance = null;

    private GameObject JammoPointObject;

    private GameManager() { }

    // ����� ã��
    private void Awake()
    {
        JammoPointObject = Resources.Load("Points/JammoPoint") as GameObject;

        new GameObject("PointList");

        if (Instance == null)
            Instance = this;

        // ���� ������ �����͸� ������ �ʰ� �Ѿ�� �ְ� ����
        DontDestroyOnLoad(this);
    }

    private int Count = 0;

    // �ʱ�ȭ
    // ������ �����ܰ�� Update���� ������
    private IEnumerator Start()
    {
        Count = 1;

        // Update�� ȣ������� ����
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
