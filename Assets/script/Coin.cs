using UnityEngine;

public class Coin : MonoBehaviour
{
    Rigidbody2D rb; // Rigidbody2D 컴포넌트
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Rigidbody2D 컴포넌트 가져오기
        Jump(); // 점프 함수 호출
        
    }

    void Jump()
    {
        rb.AddForce(new Vector2(Random.Range(-1, 1), Random.Range(3, 6)), ForceMode2D.Impulse);
    }

    // 플레이어와 충돌 시 호출됨
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Gamemanager.Instance.ShowCoinCount(); // 코인 개수 표시
            Destroy(gameObject); // 코인 오브젝트 삭제
        }
    }
}
