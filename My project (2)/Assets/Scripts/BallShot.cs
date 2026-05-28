using UnityEngine;

public class BallShot : MonoBehaviour
{
    Rigidbody2D rb;

    Vector2 startPos;
    Vector2 endPos;

    [Header("発射設定")]
    public float power = 0.05f;

    [Header("速度設定")]
    public float maxSpeed = 15f;

    [Range(0.90f, 1f)]
    public float slowDown = 0.98f;

    [Header("プレイヤーHP（発射回数）")]
    public int hp = 5;

    [Header("停止判定")]
    public float stopSpeed = 0.1f;

    [Header("状態")]
    [SerializeField] bool shot = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // =========================
        // 発射済み
        // =========================

        if (shot)
        {
            SpeedControl();
            return;
        }

        // =========================
        // HP0なら発射不可
        // =========================

        if (hp <= 0)
        {
            return;
        }

        // =========================
        // スマホ操作
        // =========================

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            // タッチ開始
            if (touch.phase == TouchPhase.Began)
            {
                startPos = touch.position;
            }

            // タッチ終了
            if (touch.phase == TouchPhase.Ended)
            {
                endPos = touch.position;

                Shoot();
            }
        }

        // =========================
        // PCマウス操作
        // =========================

        // 押した
        if (Input.GetMouseButtonDown(0))
        {
            startPos = Input.mousePosition;
        }

        // 離した
        if (Input.GetMouseButtonUp(0))
        {
            endPos = Input.mousePosition;

            Shoot();
        }
    }

    void Shoot()
    {
        Vector2 direction = startPos - endPos;

        rb.linearVelocity = direction * power;

        shot = true;

        hp--;

        GameManager.instance.UpdateHP(hp);

        Debug.Log("Player HP : " + hp);
    }

    void SpeedControl()
    {
        // =========================
        // 最大速度制限
        // =========================

        if (rb.linearVelocity.magnitude > maxSpeed)
        {
            rb.linearVelocity =
                rb.linearVelocity.normalized * maxSpeed;
        }

        // =========================
        // 減速
        // =========================

        rb.linearVelocity *= slowDown;

        // =========================
        // 停止判定
        // =========================

        if (rb.linearVelocity.magnitude < stopSpeed)
        {
            rb.linearVelocity = Vector2.zero;

            shot = false;

            Debug.Log("Ready");
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // =========================
        // 敵に当たった
        // =========================

        if (collision.gameObject.CompareTag("Enemy"))
        {
            Enemy enemy =
                collision.gameObject.GetComponent<Enemy>();

            if (enemy != null)
            {
                enemy.Damage(1);
            }
        }
    }
}