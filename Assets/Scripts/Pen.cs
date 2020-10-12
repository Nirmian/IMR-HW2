using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class Pen : VRTK_InteractableObject
{
    public DrawingBoard drawingBoard;
    private RaycastHit touch;
    private bool lastTouch;
    private Quaternion lastAngle;

    // Start is called before the first frame update
    void Start()
    {
        this.drawingBoard = GameObject.Find("DrawingBoard").GetComponent<DrawingBoard>();   
    }

    // Update is called once per frame
    void Update()
    {
        float tipHeight = transform.Find("Tip").transform.localScale.y;
        Vector3 tip = transform.Find("Tip").transform.position;

        if (Physics.Raycast(tip, transform.up, out touch, tipHeight))
        {
            if (touch.collider.tag != "DrawingBoard") return;

            this.drawingBoard = touch.collider.GetComponent<DrawingBoard>();

            this.drawingBoard.SetColor(Color.blue);
            this.drawingBoard.SetTouchPosition(touch.textureCoord.x, touch.textureCoord.y);
            this.drawingBoard.ToggleTouch(true);

            if (!lastTouch)
            {
                lastTouch = true;
                lastAngle = transform.rotation;
            }
        }

        else
        {
            this.drawingBoard.ToggleTouch(false);
            lastTouch = false;
        }

        if(lastTouch)
        {
            transform.rotation = lastAngle;
            tipHeight *= 1.1f;
        }
    }
}
