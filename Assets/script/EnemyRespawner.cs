using System.Collections;
using UnityEngine;

public class EnemyRespown : MonoBehaviour
{// 생성할 적 프리팹 배열
    public GameObject[] Enemies;

    // 적들이 생성될 X 좌표 배열
    float[] arrPosx = { -2f, 0f, 2f };

    [SerializeField]
    float spawnInterval = 0.5f;   // 적 생성 간격

    [SerializeField]
    float moveSpeed = 5f;         // 적 이동 속도

    // 적 생성 위치 (Transform 컴포넌트)
    public Transform spawnPosition;

    int currentEnemyIndex = 0;    // 현재 생성할 적 인덱스
    int spawnCount = 0;           // 적 생성 횟수

    void Start()
    {
        // 적 생성 코루틴 실행
        StartCoroutine(EnemyRoutine());
    }

    // 적을 주기적으로 생성하는 코루틴
    IEnumerator EnemyRoutine()
    {
        yield return new WaitForSeconds(3); // 게임 시작 후 3초 대기

        while (true)
        {
            for (int i = 0; i < 10; i++) // 10번 반복
            {
                // arrPosx에 있는 모든 x좌표에 적 생성
                for (int j = 0; j < arrPosx.Length; j++)
                {
                    int randomEnemyIndex = Random.Range(0, Enemies.Length); // 랜덤 인덱스
                    SpawnEnemy(arrPosx[j], randomEnemyIndex, moveSpeed);
                }
                spawnCount++;

                // 2회마다 이동속도 증가
                if (spawnCount % 2 == 0)
                {
                    moveSpeed += 2f;  // 속도 증가
                }

                yield return new WaitForSeconds(spawnInterval); // 다음 생성까지 대기
            }
            // 10번 생성 후 3초 대기
            yield return new WaitForSeconds(3f);
        }
    }

    // 적 생성 함수
    void SpawnEnemy(float posX, int index, float moveSpeed)
    {
        Vector3 spawnPos = new Vector3(posX, spawnPosition.position.y, spawnPosition.position.z);

        GameObject enemyObject = Instantiate(Enemies[index], spawnPos, Quaternion.identity);
        Enemy enemy = enemyObject.GetComponent<Enemy>();
        enemy.SetMoveSpeed(moveSpeed);  // 이동 속도 설정
    }
}
