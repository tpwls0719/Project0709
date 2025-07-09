using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public GameObject damageTextPrefab; // 데미지 텍스트 프리팹

    private SpriteRenderer spriteRenderer; // 스프라이트 렌더러 컴포넌트
    public Color flashColor = Color.red; // 플래시 색상
    public float flashDuration = 0.1f; // 플래시 지속 시간
    private Color originalColor; // 원래 색상 저장용

    public float enemyHp = 1; // 적의 체력

    [SerializeField]
    public float moveSpeed = 1f; // 적 이동 속도

    public GameObject Coin;
    public GameObject Effect;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>(); // 스프라이트 렌더러 컴포넌트 가져오기
        originalColor = spriteRenderer.color; // 원래 색상 저장   
    }

    /*
    public void Flash()
    {
        StopAllCoroutines(); // 모든 코루틴 중지
        StartCoroutine(FlashCoroutine()); // 플래시 코루틴 시작
    }

    private IEnumerator FlashCoroutine()
    {
        spriteRenderer.color = flashColor; // 색상 변경
        yield return new WaitForSeconds(flashDuration); // 잠시 대기
        spriteRenderer.color = originalColor; // 원래 색상으로 복원
    }*/

    // 이동 속도 설정 함수
    public void SetMoveSpeed(float moveSpeed)
    {
        this.moveSpeed = moveSpeed; // 이동 속도 설정
    }

    // 매 프레임마다 아래로 이동, 화면 밖으로 나가면 삭제
    void Update()
    {
        transform.position += Vector3.down * moveSpeed * Time.deltaTime;

        if (transform.position.y < -7f)
        {
            Destroy(this.gameObject);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Missile") // 미사일과 충돌 시
        {
            Missile missile = collision.GetComponent<Missile>();
            StopAllCoroutines(); // 모든 코루틴 중지
            StartCoroutine("HitColor");

            enemyHp = enemyHp - missile.missileDamage; // 적의 체력 감소
            if (enemyHp < 0)
            {
                Destroy(gameObject); // 적 삭제
                Instantiate(Coin, transform.position, Quaternion.identity); // 코인 생성
                Instantiate(Effect, transform.position, Quaternion.identity); // 이펙트 생성
            }
            TakeDamage(missile.missileDamage); // 데미지 처리
        }
    }

//피격 시 색상 변경 코루틴
    IEnumerator HitColor()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = Color.red; // 색상 변경
        yield return new WaitForSeconds(0.1f); // 0.1초 대기 혹은 0,1f 자리에 flashDuration 넣는 것도 가능
        spriteRenderer.color = Color.white; // 원래 색상으로 복원
    }

    //메세지 팝업 함수
    void TakeDamage(int damage)
    {
        DamagePopupManager.Instance.CreateDamageText(damage, transform.position); // 데미지 텍스트 생성
    }
}
