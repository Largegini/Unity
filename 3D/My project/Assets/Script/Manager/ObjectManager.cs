using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    [HideInInspector] public static ObjectManager Instance = null;

    // [HideInInspector] public List<GameObject> ObjectList = new List<GameObject>();

    public Dictionary<string, GameObject> ObjectList = new Dictionary<string, GameObject>();
    private ObjectManager() { }

    // 만들고 찾기
    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        GameObject[] Objects = Resources.LoadAll<GameObject>("Prefabs/Objects");

        foreach (GameObject Element in Objects)
            ObjectList.Add(Element.name, Element);

        // 다음 씬에도 데이터를 지우지 않고 넘어갈수 있게 해줌
        DontDestroyOnLoad(this);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
