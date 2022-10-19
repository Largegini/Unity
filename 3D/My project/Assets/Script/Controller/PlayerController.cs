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

    // ** ��Ÿ�� ���Ŀ� ó���� �ѹ� ����Ǵ� �Լ�
    // ** Start �Լ����� ���� �����
    private void Awake()
    {
        // ** ���� ��ũ��Ʈ�� ���Ե� ��ü�� Rigidbody ������Ʈ�� �޾ƿ´�
        // ** ���� Rigidbody ������Ʈ�� �������� �ʴ´ٸ� �ƹ��͵� �޾ƿ��� �ʴ´�
        Rigidbody Rigid = this.GetComponent<Rigidbody>();

        // ** �浹ó���� �����ϱ� ���ؼ��� �Ʒ� �� ������Ʈ�� �ݵ�� ���ԵǾ�� �Ѵ�

        // ** Rigidbody = ��������
        // ** Collider = �浹ü

    }

    // ** ��Ÿ�� ���Ŀ� ó���� �ѹ� ����Ǵ� �Լ�
    void Start()
    {
        Speed = 5.0f;

        Power = 0;
        // ** Rigidbody ������Ʈ�� [transform.forward] �������� [500.0f] ��ŭ�� ���� ���Ѵ�.
        Rigid.AddForce(transform.forward * 500.0f);
    }

    
    // ** ���� �Լ�
    void Update()
    {
       
        // ** �Ǽ� ������ ��ȯ -1 ~ +1
        //float fHor = Input.GetAxis("Horizontal");
        //float fVer = Input.GetAxis("Vertical");

        // ** -1,0,1
        float fHor =Input.GetAxisRaw("Horizontal");
        float fVer = Input.GetAxisRaw("Vertical");

        Vector3 Movement = new Vector3(fHor, 0.0f, fVer)* Speed* Time.deltaTime;

        //Vector3 Movement = Vector3.forward;
        //Vector3 Movement = new Vector3(0.0f, 0.0f, 1.0f);

        transform.position += Movement;

        // ** ��ư�� �ԷµǸ� Power�� �ʱ�ȭ�Ѵ�
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Power = 0;
            Radius = 0;
            Vib = 0;
            BulletCheck = true;
        }
        // ** ��ư�� ������ �ִ� ���� Power�� ���� �����Ѵ�.
        if (Input.GetKey(KeyCode.Space))
        {
            Power += Time.deltaTime;
            Radius += Time.deltaTime;
            Vib += Time.deltaTime;

        }
        //** ��ư�� ������ ��
        if (Input.GetKey(KeyCode.Space))
        {
            // ** Ÿ���� ������ �޾ƿ´�.
            RaycastHit hit;

            // ** ������ ���� �߻��ؼ� �̸� hit ����� Ȯ���Ѵ�.
            // ** hit ���� �ʾҴٸ� if���� ������� �ʴ´�.
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
                    // ** hit.point : ���� hit�� ��ġ
                 // ** ���� �Ѿ��� ��ġ�� hit.point���ʱ�ȭ
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
                // ** hit.point : ���� hit�� ��ġ
                // ** ���� �Ѿ��� ��ġ�� hit.point���ʱ�ȭ
                Obj.transform.position = hit.point;

                */
            }
               
        }
        
        if (Input.GetKey(KeyCode.E))
        {

        }
        /*
        // ** ��ư�� �ԷµǸ� Power�� �ʱ�ȭ�Ѵ�
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Power = 0;
        }
        // ** ��ư�� ������ �ִ� ���� Power�� ���� �����Ѵ�.
        if (Input.GetKey(KeyCode.Space))
        {
            Power += Time.deltaTime;

        }
        //** ��ư�� ������ ��
        if (Input.GetKeyUp(KeyCode.Space))
        {
            // ** �Ѿ��� ���� �Ѵ�.
            GameObject Obj = Instantiate(BulletPrefab);
            // ** ������ �Ѿ��� ��ǥ�� �Ѿ��� ������ ��ġ�� �ʱ�ȭ �Ѵ�
            Obj.transform.position = FirePoint.position;

            // ** Rigidbodt : ��������
            // ** Obj : �Ѿ�
            // ** Obj�� ���� ���ԵǾ��ִ� Rigidbody Conponent�� �޾ƿ´�
            Rigidbody Rigid = Obj.GetComponent<Rigidbody>();
            // **  Rigid : ��������
            // ** AddForce : ���� ���Ѵ�
            // ** FirePoint.transform.foward : �Ѿ��� �߻�� ���� ��
            // ** (Power*1000)�� ������ ���� �߻��Ѵ�
            // ** FirePoint.transform.foward ��������
            Rigid.AddForce(FirePoint.transform.forward * Power * 1000.0f);
        }
        */
    }
}
