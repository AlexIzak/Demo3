using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Dragging : MonoBehaviour
{
    public GameObject text;

    private GameObject go_to_Move;

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
                        text.GetComponent<TMP_Text>().text = "You got me!";
                        go_to_Move = gameObject;
                    }
                }
                else
                {
                    text.GetComponent<TMP_Text>().text = "Miss";
                }
            }
            else if (Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector2 touchPos = new Vector2(pos.x, pos.y);

                go_to_Move.transform.position = touchPos;

                text.GetComponent<TMP_Text>().text = "We are moving!";
            }
            else if (Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                text.GetComponent<TMP_Text>().text = "You let go!";
                go_to_Move = null;
            }
        }
    }
}
