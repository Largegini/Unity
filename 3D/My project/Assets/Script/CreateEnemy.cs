using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateEnemy : MonoBehaviour
{
    // ** �ν����ͺ信 �����ش�. (����ȭ�Ѵ�)
    private GameObject EnemyPrefab;

   // private SkinnedMeshRenderer renderer = null;
  //  private List<string> names = new List<string>();
    private List<SkinnedMeshRenderer> renderers = new List<SkinnedMeshRenderer>();

    private void Awake()
    {
        EnemyPrefab = Resources.Load("Prefabs/Objects/Jammo") as GameObject;
    }
    // ** �浹���� �Լ�. �浹�� �����Ǹ� ����ȴ�.
    // �ݶ��̴��� �����ؾ���
    private void OnCollisionEnter(Collision collision)
    {
        // ** �ڷ�ƾ �Լ��� �����Ѵ�.
        StartCoroutine(Create());
    }

    // ** �ڷ�ƾ �Լ�
    // ���� ����ϴ� ���� �����ʴ� (ó���ӵ��� ������)
    IEnumerator Create()
    {
        // ** 0.5�� �ڿ� �Լ��� �����.
        // �� ���� ������ �Լ� 0.5�� ����(������ �ƹ��͵� ���� �ʴ� ���� �ƴϴ�)
        // 0.5�ʰ� �ٸ� �۾��� �ϰ� �ִٰ� 0.5���Ŀ� �����Ѵ�
        // ������ʹ� �ٸ���
        // �� �������� ����
        yield return new WaitForSeconds(0.5f);

        while(true)
        {
            yield return new WaitForSeconds(5.0f);

            if (transform.childCount > 0)
                continue;

            // ** ������Ʈ ����
            GameObject Obj = Instantiate(EnemyPrefab);

            //** �̸��� Jammo�� ����
            Obj.transform.name = "Jammo";
            // ** ������ġ�� ����
            Obj.transform.position = transform.position;

            // ** Obj.transform.parent = ���������� �������ش�.
            // ** parent = �θ� �����Ѵ�.
            Obj.transform.parent = this.transform;

            // ** EnemyManager Ŭ������ ������ ������Ʈ�� �����Ѵ�
            EnemyManager.Instance.AddObject(Obj);

            renderers.Clear();
            FindRenderer(Obj);

            // ** SkinnedMeshRenderer�� ���İ��� �ְ� ������ ��Ÿ���� �Ѵ�
            foreach (SkinnedMeshRenderer renderer in renderers)
            {
                renderer.material.shader = Shader.Find("Legacy Shaders/Transparent/VertexLit");

                if (renderer.material.HasProperty("_Color"))
                {
                    Color color = renderer.material.GetColor("_Color");
                    // ** �������� �����.
                    renderer.material.SetColor("_Color", new Color(color.r, color.g, color.b, 0.0f));

                    StartCoroutine(SetColor(renderer, color));
                }
            }
        }
    }

    // ** GameObject �� ��� ������ Ȯ���Ͽ� SkinnedMeshRenderer�� ã�´�.
    void FindRenderer(GameObject _Obj)
    {
        for(int i =0; i < _Obj.transform.childCount; ++i)
        {
            GameObject Obj = _Obj.transform.GetChild(i).gameObject;

            // ** �������� ������Ʈ�� �� �ٸ� ���������� �����Ѵٸ�
            if (Obj.transform.childCount > 0)
                FindRenderer(Obj);

            SkinnedMeshRenderer renderer = Obj.transform.Find(name).GetComponent<SkinnedMeshRenderer>();

            if (renderer != null)
                renderers.Add(renderer);
        }
    }

    //** ������ ��Ÿ���� �ϴ� ����
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