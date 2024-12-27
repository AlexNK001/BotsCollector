using UnityEngine;

public class BotColorStorage : ColorStorage
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    public override void SetColor(Color color)
    {
        _spriteRenderer.color = color;
    }
}