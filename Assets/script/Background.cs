using UnityEngine;

public class Background : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 1f;

    //매 프레임마다 호출
    void Update()
    {
        transform.position += Vector3.down * moveSpeed * Time.deltaTime;

        if (transform.position.y < -10f)
        {
            transform.position += new Vector3(0, 20f, 0);
        }
    }
}
