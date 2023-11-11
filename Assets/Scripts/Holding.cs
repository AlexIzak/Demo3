using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Holding : MonoBehaviour
{
    public GameObject text;

    public float holdTimer;
    private float curTimer;
    private bool isHolding = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount == 1)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector2 touchPos = new Vector2(pos.x, pos.y);

                var hit = Physics2D.OverlapPoint(touchPos);
                if (hit)
                {
                    if (hit.transform == transform)
                    {
                        text.GetComponent<TMP_Text>().text = "Hold...";
                        isHolding = true;
                    }
                }
                else
                {
                    text.GetComponent<TMP_Text>().text = "Miss";
                }
            }
            else if (Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                ResetHold("Reset!");
            }
        }

        if(isHolding)
        {
            curTimer += Time.deltaTime;
            if(curTimer > holdTimer)
            {
                ResetHold("Success!");
            }
        }
    }

    private void ResetHold(string msg)
    {
        curTimer = 0;
        text.GetComponent<TMP_Text>().text = msg;
        isHolding = false;
    }
}
