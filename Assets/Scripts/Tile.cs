using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public Color lightColor, darkColor;
    public Position position;
    public TileInputIndicator inputIndicator;

    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        inputIndicator = GetComponentInChildren<TileInputIndicator>();
    }

    public void SetColor(bool isLight)
    {
        spriteRenderer.color = (isLight) ? lightColor : darkColor;
    }

    public void SetPosition(int x, int y)
    {
        position.x = x;
        position.y = y;
    }

    public void OnMouseEnter()
    {
        GameManager.Instance.OnMouseOverTile(this);
    }

    public void OnMouseExit()
    {
        GameManager.Instance.OnMouseExitTile(this);
    }

    public void OnMouseDown()
    {
        GameManager.Instance.OnMouseDownTile(this);
    }

}
