using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseEditor : MonoBehaviour
{
    public static MouseEditor Instance { get; private set; }
    [SerializeField] SpriteRenderer sprRen;
    private void Awake()
    {
        Instance = this;
    }
    private void Update()
    {
        Vector2 tilePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        tilePos.x = Mathf.CeilToInt(tilePos.x) - 0.5f;
        tilePos.y = Mathf.FloorToInt(tilePos.y) + 0.5f;
        transform.position = tilePos;
    }

    public void SetMouseIcon(Sprite sprite)
    {
        sprRen.sprite = sprite;
    }
}
