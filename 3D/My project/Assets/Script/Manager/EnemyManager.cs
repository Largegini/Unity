using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
   [HideInInspector] public static EnemyManager Instance = null;


    private EnemyManager() { }
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    [SerializeField] private List<GameObject> EnemyList = new List<GameObject>();

    public void AddObject(GameObject Obj)
    {
        EnemyList.Add(Obj);
    }
}
