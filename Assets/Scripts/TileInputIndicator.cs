using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileInputIndicator : MonoBehaviour
{
    public Color green, red, blue, yellow;

    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = false;
    }

    public void MakeGreen()
    {
        spriteRenderer.color = green;
        spriteRenderer.enabled = true;
    }

    public void MakeBlue()
    {
        spriteRenderer.color = blue;
        spriteRenderer.enabled = true;
    }

    public void MakeRed()
    {
        spriteRenderer.color = red;
        spriteRenderer.enabled = true;
    }

    public void MakeYellow()
    {
        spriteRenderer.color = yellow;
        spriteRenderer.enabled = true;
    }

    public void Hide()
    {
        spriteRenderer.enabled = false;
    }
}
