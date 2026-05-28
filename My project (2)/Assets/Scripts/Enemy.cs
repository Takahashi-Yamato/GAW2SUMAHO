using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("“GHP")]
    public int hp = 7;

    [Header("“|‚µ‚½Žž‚ÌƒXƒRƒA")]
    public int scoreValue = 100;

    public void Damage(int damage)
    {
        hp -= damage;

        Debug.Log("Enemy HP : " + hp);

        // Ž€–S
        if (hp <= 0)
        {
            Die();
        }
    }
    void Die()
    {
        // ƒXƒRƒA‰ÁŽZ
        GameManager.instance.AddScore(scoreValue);

        // V‚µ‚¢“G¶¬
        GameManager.instance.SpawnEnemy();

        Destroy(gameObject);
    }
}