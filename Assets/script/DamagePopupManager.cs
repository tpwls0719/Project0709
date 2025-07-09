using UnityEngine;

public class DamagePopupManager : MonoBehaviour
{
    //싱글톤 인스턴스
    public static DamagePopupManager Instance { get; private set; }

    //데미지 텍스트가 표시할 캔버스
    public RectTransform canvasRect;

    //데미지 텍스트 프리팹
    public GameObject damageTextPrefab;

    void Awake()
    {
        //싱글톤 인스턴스가 없으면 자신을 할당
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 씬이 바뀌어도 파괴되지 않도록 설정
        }
        else
        {
            Destroy(gameObject); //이미 인스턴스가 있으면 중복 파괴
        }
    }

    //데이미 텍스트를 생성하는 함수
    public void CreateDamageText(int damage, Vector3 worldPos)
    {
        //월드 좌표를 스크린 좌표로 변환
        Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPos);

        //데미지 텍스트 프리팹을 캔버스에 생성
        GameObject textObj = Instantiate(damageTextPrefab, canvasRect);
        textObj.GetComponent<RectTransform>().position = screenPos;

        //데미지 값을 표시
        textObj.GetComponent<DamageText>().Show(damage);
    }
}
