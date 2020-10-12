using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DrawingBoard : MonoBehaviour
{
    private int textureSize = 2048;
    private int penSize = 10;
    private Texture2D texture;
    private Color[] color;

    private bool touching, lastTouch;
    private float posX, posY;
    private float lastX, lastY;

    // Start is called before the first frame update
    void Start()
    {
        Renderer renderer = GetComponent<Renderer>();
        this.texture = new Texture2D(textureSize, textureSize);
        renderer.material.mainTexture = this.texture;
    }

    // Update is called once per frame
    void Update()
    {
        int x = (int)(posX * textureSize - (penSize / 2));
        int y = (int)(posY * textureSize - (penSize / 2));

        if(lastTouch)
        {
            texture.SetPixels(x, y, penSize, penSize, color);

            // this is supposed to get rid of empty spaces between drawn lines
            //for (float t = 0.01f; t < 1.00f; t += 0.01f)
            //{
            //    int lerpX = (int)Mathf.Lerp(lastX, (float)x, t);
            //    int lerpY = (int)Mathf.Lerp(lastY, (float)x, t);
            //    texture.SetPixels(lerpX, lerpY, penSize, penSize, color);
            //}

            texture.Apply();
        }

        this.lastX = (float)x;
        this.lastY = (float)y;
        this.lastTouch = this.touching;
    }

    public void ToggleTouch(bool touching)
    {
        this.touching = touching;
    }

    public void SetTouchPosition(float x, float y)
    {
        this.posX = x;
        this.posY = y;
    }

    public void SetColor(Color color)
    {
        this.color = Enumerable.Repeat<Color>(color, penSize * penSize).ToArray<Color>();
    }
}
