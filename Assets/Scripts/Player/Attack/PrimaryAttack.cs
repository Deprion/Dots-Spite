using System.Collections;
using UnityEngine;

public class PrimaryAttack : MonoBehaviour
{
    [SerializeField] private Sprite[] spriteArr;
    [SerializeField] private SpriteRenderer spriteRenderer;
    private void Awake()
    {
        StartCoroutine(ChangeSprite());
    }
    private IEnumerator ChangeSprite()
    {
        foreach (Sprite spr in spriteArr)
        {
            spriteRenderer.sprite = spr;
            yield return new WaitForSeconds(0.2f);
        }
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
    }
}
