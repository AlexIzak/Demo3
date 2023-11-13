using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Tapping : MonoBehaviour
{
    public GameObject text;

    // Update is called once per frame
    void Update()
    {
        if(Input.touchCount == 1)
        {
            if(Input.GetTouch(0).phase == TouchPhase.Began )
            {
                Vector3 pos = Camera.main.ScreenToWorldPoint( Input.mousePosition );
                Vector2 touchPos = new Vector2( pos.x, pos.y );

                var hit = Physics2D.OverlapPoint(touchPos);
                if(hit)
                {
                    if(hit.transform == transform )
                    {
                        text.GetComponent<TMP_Text>().text = "Tapped!";
                    }
                }
                else
                {
                    text.GetComponent<TMP_Text>().text = "Miss";
                }
            }
            else if(Input.GetTouch(0).phase == TouchPhase.Ended )
            {
                text.GetComponent<TMP_Text>().text = "End of Tap!";
            }
        }
    }

    private void OnMouseDown()
    {
        //text.GetComponent<TMP_Text>().text = "Tapped!";
    }

    private void OnMouseUp()
    {
        //text.GetComponent<TMP_Text>().text = "Released tap!";
    }
}
