using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{

    [SerializeField] private GameObject Fx;


    private void OnCollisionEnter(Collision collision)
    {
        // ** 지금 스크립트가 포함된 gameObject를 삭제
        Destroy(this.gameObject, 0.05f);

        // ** Instantiate = 복제함수
        // ** FX의 복사본을 Obj로 넘겨줌
        GameObject Obj = Instantiate(Fx);

        // ** 현재 총알 = 지금 스크립트가 포함된 gameObject
        // ** 현재 총알 진행방향의 반대방향을 바라보는 벡터를 구함
        Vector3 Direction = (transform.position - collision.transform.position).normalized;

        // ** 현재 총알의 위치로부터 Direction 방향으로 2.0f 만큼 이동
        // ** 결과적으로 총알진행 방향의 반대방향으로 2만큼 물러나게 된다
        Obj.transform.position = transform.position + (Direction * 2.0f);

        // ** Fx의 복사본인 Obj를 삭제
        // ** Fx의 애니메이션이 끝나는 시간이 1.5f정도가 소요됨
        Destroy(Obj.gameObject, 1.5f);
    }

}
