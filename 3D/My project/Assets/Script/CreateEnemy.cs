using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateEnemy : MonoBehaviour
{
    // ** 인스텍터뷰에 보여준다. (직렬화한다)
    private GameObject EnemyPrefab;

   // private SkinnedMeshRenderer renderer = null;
  //  private List<string> names = new List<string>();
    private List<SkinnedMeshRenderer> renderers = new List<SkinnedMeshRenderer>();

    private void Awake()
    {
        EnemyPrefab = Resources.Load("Prefabs/Objects/Jammo") as GameObject;
    }
    // ** 충돌감지 함수. 충돌이 감지되면 실행된다.
    // 콜라이더가 존재해야함
    private void OnCollisionEnter(Collision collision)
    {
        // ** 코루틴 함수를 실행한다.
        StartCoroutine(Create());
    }

    // ** 코루틴 함수
    // 많이 사용하는 것은 좋지않다 (처리속도가 느려짐)
    IEnumerator Create()
    {
        // ** 0.5초 뒤에 함수를 재실행.
        // 한 번만 실행할 함수 0.5초 슬립(하지만 아무것도 하지 않는 것은 아니다)
        // 0.5초간 다른 작업을 하고 있다가 0.5초후에 실행한다
        // 쓰레드와는 다르다
        // 한 곳에서만 실행
        yield return new WaitForSeconds(0.5f);

        while(true)
        {
            yield return new WaitForSeconds(5.0f);

            if (transform.childCount > 0)
                continue;

            // ** 오브젝트 생성
            GameObject Obj = Instantiate(EnemyPrefab);

            //** 이름을 Jammo로 설정
            Obj.transform.name = "Jammo";
            // ** 생성위치를 셋팅
            Obj.transform.position = transform.position;

            // ** Obj.transform.parent = 계층구조를 설정해준다.
            // ** parent = 부모를 설정한다.
            Obj.transform.parent = this.transform;

            // ** EnemyManager 클래스에 생성된 오브젝트를 보관한다
            EnemyManager.Instance.AddObject(Obj);

            renderers.Clear();
            FindRenderer(Obj);

            // ** SkinnedMeshRenderer에 알파값을 주고 서서히 나타나게 한다
            foreach (SkinnedMeshRenderer renderer in renderers)
            {
                renderer.material.shader = Shader.Find("Legacy Shaders/Transparent/VertexLit");

                if (renderer.material.HasProperty("_Color"))
                {
                    Color color = renderer.material.GetColor("_Color");
                    // ** 투명으로 만든다.
                    renderer.material.SetColor("_Color", new Color(color.r, color.g, color.b, 0.0f));

                    StartCoroutine(SetColor(renderer, color));
                }
            }
        }
    }

    // ** GameObject 의 모든 계층을 확인하여 SkinnedMeshRenderer를 찾는다.
    void FindRenderer(GameObject _Obj)
    {
        for(int i =0; i < _Obj.transform.childCount; ++i)
        {
            GameObject Obj = _Obj.transform.GetChild(i).gameObject;

            // ** 하위계층 오브젝트에 또 다른 하위계층이 존재한다면
            if (Obj.transform.childCount > 0)
                FindRenderer(Obj);

            SkinnedMeshRenderer renderer = Obj.transform.Find(name).GetComponent<SkinnedMeshRenderer>();

            if (renderer != null)
                renderers.Add(renderer);
        }
    }

    //** 서서히 나타나게 하는 구간
    IEnumerator SetColor(SkinnedMeshRenderer renderer, Color color)
    {
        float rColor = 0;

        while (true)
        {
            yield return null;

            rColor += Time.deltaTime;

            renderer.material.SetColor("_Color", new Color(color.r, color.g, color.b, rColor));

            if (rColor >= 255.0f)
                break;
        }
    }
}

/*
   public MeshFilter meshFilter;

   private void Start()
   {

       Mesh mesh = new Mesh();

       meshFilter.mesh = mesh;

       Vector3[] Vertices = new Vector3[3];

       /*
       foreach(Vector3 element in)
       {
           Vertices = new Vector3(0);
       }
       Vertices[0] = Vector3.zero;
       Vertices[1] = new Vector3(-10.0f, 0.0f, 10.00f);
       Vertices[2] = new Vector3(10.0f, 0.0f, 10.00f);

       int[] triangles = new int[3];

       triangles[0] = 0;
       triangles[1] = 1;
       triangles[2] = 2;

       mesh.Clear();
       mesh.vertices = Vertices;
       mesh.triangles = triangles;

       mesh.RecalculateNormals();
   }
       */