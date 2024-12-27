using UnityEngine;

public class BaseColorStorage : ColorStorage
{
    [SerializeField] private MeshRenderer[] _dicorations;

    public Color Color { get; private set; }

    public override void SetColor(Color color)
    {
        Color = color;

        for (int i = 0; i < _dicorations.Length; i++)
            _dicorations[i].material.color = color;
    }
}
