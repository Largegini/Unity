using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float Speed;
    Rigidbody Rigid = null;

    public float Power;
    public Transform FirePoint;
    public GameObject BulletPrefab;
    public GameObject Bullet_FXPrefab;
    public float Radius;
    public float Vib;

    bool BulletCheck;

    public LayerMask TargetMask;

    // ** 런타임 이후에 처음에 한번 실행되는 함수
    // ** Start 함수보다 먼저 실행됨
    private void Awake()
    {
        // ** 현재 스크립트가 포함된 객체의 Rigidbody 컴포넌트를 받아온다
        // ** 만약 Rigidbody 컴포넌트가 존재하지 않는다면 아무것도 받아오지 않는다
        Rigidbody Rigid = this.GetComponent<Rigidbody>();

        // ** 충돌처리를 진행하기 위해서는 아래 두 컴포넌트가 반드시 포함되어야 한다

        // ** Rigidbody = 물리엔진
        // ** Collider = 충돌체

    }

    // ** 런타임 이후에 처음에 한번 실행되는 함수
    void Start()
    {
        Speed = 5.0f;

        Power = 0;
        // ** Rigidbody 컴포넌트에 [transform.forward] 방향으로 [500.0f] 만큼의 힘을 가한다.
        Rigid.AddForce(transform.forward * 500.0f);
    }

    
    // ** 갱신 함수
    void Update()
    {
       
        // ** 실수 단위로 반환 -1 ~ +1
        //float fHor = Input.GetAxis("Horizontal");
        //float fVer = Input.GetAxis("Vertical");

        // ** -1,0,1
        float fHor =Input.GetAxisRaw("Horizontal");
        float fVer = Input.GetAxisRaw("Vertical");

        Vector3 Movement = new Vector3(fHor, 0.0f, fVer)* Speed* Time.deltaTime;

        //Vector3 Movement = Vector3.forward;
        //Vector3 Movement = new Vector3(0.0f, 0.0f, 1.0f);

        transform.position += Movement;

        // ** 버튼이 입력되면 Power를 초기화한다
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Power = 0;
            Radius = 0;
            Vib = 0;
            BulletCheck = true;
        }
        // ** 버튼을 누르고 있는 동안 Power의 값을 누적한다.
        if (Input.GetKey(KeyCode.Space))
        {
            Power += Time.deltaTime;
            Radius += Time.deltaTime;
            Vib += Time.deltaTime;

        }
        //** 버튼을 놓았을 때
        if (Input.GetKey(KeyCode.Space))
        {
            // ** 타겟의 정보를 받아온다.
            RaycastHit hit;

            // ** 가상의 선을 발사해서 미리 hit 대상을 확인한다.
            // ** hit 되지 않았다면 if문이 실행되지 않는다.
            if (Physics.Raycast(FirePoint.position, FirePoint.transform.forward, out hit, Mathf.Infinity, TargetMask))
            {
                
                Vector3 offset = new Vector3(
                   Mathf.Cos(90 * Mathf.Deg2Rad),
                    Mathf.Sin(90 * Mathf.Deg2Rad),
                    0.1f) * Radius + transform.position;

                float RandNumber = Random.Range(-0.1f, 0.1f);

                Vector3 Vibe = new Vector3(
                   RandNumber,
                    RandNumber,
                    0.0f) * Vib;

               // Debug.DrawLine(FirePoint.position, offset+Vibe, Color.red);

                GameObject Obj = Instantiate(BulletPrefab);

                Obj.transform.position = offset + Vibe;

                if (BulletCheck)
                    Obj.transform.position += hit.point ;
                else
                {
                    // ** hit.point : 현재 hit의 위치
                 // ** 현재 총알의 위치를 hit.point로초기화
                    Obj.transform.position = offset + Vibe;

                    Rigid = Obj.GetComponent<Rigidbody>();

                    Rigid.AddForce(FirePoint.transform.forward * 1000);
                }

                    
                /*
                Vector3 offset = new Vector3(
                    Random.Range(-0.5f, 0.5f),
                    Random.Range(-0.5f, 0.5f),
                    0.0f);
                Debug.DrawLine(FirePoint.position, hit.point, Color.red);
                GameObject Obj = Instantiate(BulletPrefab);
                // ** hit.point : 현재 hit의 위치
                // ** 현재 총알의 위치를 hit.point로초기화
                Obj.transform.position = hit.point;

                */
            }
               
        }
        
        if (Input.GetKey(KeyCode.E))
        {

        }
        /*
        // ** 버튼이 입력되면 Power를 초기화한다
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Power = 0;
        }
        // ** 버튼을 누르고 있는 동안 Power의 값을 누적한다.
        if (Input.GetKey(KeyCode.Space))
        {
            Power += Time.deltaTime;

        }
        //** 버튼을 놓았을 때
        if (Input.GetKeyUp(KeyCode.Space))
        {
            // ** 총알을 복제 한다.
            GameObject Obj = Instantiate(BulletPrefab);
            // ** 복제된 총알의 좌표를 총알이 나가는 위치로 초기화 한다
            Obj.transform.position = FirePoint.position;

            // ** Rigidbodt : 물리엔진
            // ** Obj : 총알
            // ** Obj에 현재 포함되어있는 Rigidbody Conponent를 받아온다
            Rigidbody Rigid = Obj.GetComponent<Rigidbody>();
            // **  Rigid : 물리엔진
            // ** AddForce : 힘을 가한다
            // ** FirePoint.transform.foward : 총알이 발사될 시작 점
            // ** (Power*1000)의 힘으로 총을 발사한다
            // ** FirePoint.transform.foward 방향으로
            Rigid.AddForce(FirePoint.transform.forward * Power * 1000.0f);
        }
        */
    }
}
