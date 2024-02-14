using UnityEngine;

public class EnemySpriteChanger : MonoBehaviour
{
    public Sprite movingEnemySprite;
    public Sprite staticEnemySprite;
    public Sprite turelEnemySprite;

    void Start()
    {
        TurelAI turelAI = GetComponent<TurelAI>();
        if (turelAI != null)
        {
            EnemyType enemyType = turelAI.GetEnemyType();

            SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
            if (spriteRenderer != null)
            {
                switch (enemyType)
                {
                    case EnemyType.Moving:
                        spriteRenderer.sprite = movingEnemySprite;
                        break;

                    case EnemyType.Static:
                        spriteRenderer.sprite = staticEnemySprite;
                        break;

                    case EnemyType.Turel:
                        spriteRenderer.sprite = turelEnemySprite;
                        break;

                    default:
                        Debug.LogError("Невідомий тип ворога");
                        break;
                }
            }
        }
    }
}