using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{

    [SerializeField] private GameObject Fx;


    private void OnCollisionEnter(Collision collision)
    {
        // ** ���� ��ũ��Ʈ�� ���Ե� gameObject�� ����
        Destroy(this.gameObject, 0.05f);

        // ** Instantiate = �����Լ�
        // ** FX�� ���纻�� Obj�� �Ѱ���
        GameObject Obj = Instantiate(Fx);

        // ** ���� �Ѿ� = ���� ��ũ��Ʈ�� ���Ե� gameObject
        // ** ���� �Ѿ� ��������� �ݴ������ �ٶ󺸴� ���͸� ����
        Vector3 Direction = (transform.position - collision.transform.position).normalized;

        // ** ���� �Ѿ��� ��ġ�κ��� Direction �������� 2.0f ��ŭ �̵�
        // ** ��������� �Ѿ����� ������ �ݴ�������� 2��ŭ �������� �ȴ�
        Obj.transform.position = transform.position + (Direction * 2.0f);

        // ** Fx�� ���纻�� Obj�� ����
        // ** Fx�� �ִϸ��̼��� ������ �ð��� 1.5f������ �ҿ��
        Destroy(Obj.gameObject, 1.5f);
    }

}
