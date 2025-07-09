using System.Data;
using UnityEngine;

public class Missile : MonoBehaviour
{
    [SerializeField]

    private float moveSpeed = 1f; // 미사일 이동 속도

    [SerializeField]

    public int missileDamage = 1; // 미사일 데미지

    [SerializeField]

    GameObject Expeffect; // 폭발 이펙트 프리팹

    void Update()
    {
        transform.position += Vector3.up * moveSpeed * Time.deltaTime; // 미사일 위로 이동
        if (transform.position.y > 7f) // 화면 밖으로 나가면 삭제
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy") // 적과 충돌 시
        {
           // Debug.Log("Missile hit enemy!"); // 디버그 로그 출력
            GameObject effect = Instantiate(Expeffect, transform.position, Quaternion.identity); // 폭발 이펙트 생성
            Destroy(effect, 1f); // 1초 후 폭발 이펙
            Destroy(gameObject); // 적 삭제
        }
    }
}
