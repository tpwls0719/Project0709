using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    //플레이어 이동 속도
    [SerializeField]
    float moveSpeed = 1f;
    //현재 미사일 프리팹 인덱스
    int missIndex = 0;
    //미사일 프리팹 배열
    public GameObject[] missilePrefab;
    //미사일 생성 위치
    public Transform spPosition;

    [SerializeField]
    //미사일 발사 간격 (초)
    private float shootInverval = 0.05f;
    //미사일 발사 시간
    private float lastshotTime = 0f;
    //애니메이터 컴포넌트 참조
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    //매 프레임마다 호출
    void Update()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        //Debug.Log("Horizontal Input: " + horizontalInput); //디버그용 로그 출력
        Vector3 moveTo = new Vector3(horizontalInput, 0, 0);
        transform.position += moveTo * moveSpeed * Time.deltaTime; //좌우 이동

        //애니메이션 상태 변경
        if (horizontalInput < 0)
        {
            animator.Play("left"); //왼쪽 이동 애니메이션
        }
        else if (horizontalInput > 0)
        {
            animator.Play("right"); //오른쪽 이동 애니메이션
        }
        else
        {
            animator.Play("idle"); //가운데(정지) 애니메이션
        }
        Shoot(); //미사일 발사

    }

    //미사일 발사 함수

    void Shoot()
    {
        if (Time.time - lastshotTime > shootInverval)
        {
            Instantiate(missilePrefab[missIndex], spPosition.position, Quaternion.identity); //미사일 생성
            lastshotTime = Time.time; //마지막 발사 시간 갱신
        }
    }

    //미사일 업그레이드 함수
    public void MissileUp()
    {
        missIndex++; //미사일 종류 업그레이드
        shootInverval = shootInverval - 0.01f; //발사 간격 감소(더 빠르게 발사))
        if (shootInverval <= 0.1f)
        {
            shootInverval = 0.01f; //최소 발사 간격 제한
        }
        if (missIndex >= missilePrefab.Length)
        {
            missIndex = missilePrefab.Length - 1; //인덱스 범위 제한
        }
    }
    
        // 적과 충돌 시 플레이어 사망 처리
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            // 게임 오버 처리 먼저 실행
            Gamemanager.Instance.OnPlayerDead(); // 게임매니저에 죽었다고 알림

            gameObject.SetActive(false); // 플레이어 비활성화
            Time.timeScale = 0f; // 게임 정지
        }
    }

}
