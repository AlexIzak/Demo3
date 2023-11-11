using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Swipe : MonoBehaviour
{
    private Vector3 firstTouchPos;
    private Vector3 lastTouchPos;

    public int screenPercentageForSwipe;

    private float dragDistance;
    private TMP_Text text;

    public float moveAmount;

    // Start is called before the first frame update
    void Start()
    {
        dragDistance = Screen.height * screenPercentageForSwipe / 100;
        text = GameObject.Find("Text").GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);

            if(touch.phase == TouchPhase.Began )
            {
                firstTouchPos = touch.position;
                lastTouchPos = touch.position;
            }
            else if(touch.phase == TouchPhase.Moved )
            {
                lastTouchPos = touch.position;
            }
            else if( touch.phase == TouchPhase.Ended )
            {
                lastTouchPos = touch.position;

                //Checks to see if we have dragged at all
                if(Mathf.Abs(lastTouchPos.x - firstTouchPos.x) > dragDistance || 
                    Mathf.Abs(lastTouchPos.y - firstTouchPos.y) > dragDistance)
                {
                    //Checks to see if we swiped vertical or horizontal
                    //True - horizontal / false - vertical
                    if(Mathf.Abs(lastTouchPos.x - firstTouchPos.x) > Mathf.Abs(lastTouchPos.y - firstTouchPos.y))
                    {
                        if(lastTouchPos.x > firstTouchPos.x)
                        {
                            //Right swipe
                            text.text = "Swipe Right!";
                            transform.Translate(new Vector3(moveAmount, 0, 0));
                        }
                        else
                        {
                            //Left swipe
                            text.text = "Swipe Left!";
                            transform.Translate(new Vector3(-moveAmount, 0, 0));
                        }
                    }
                    else
                    {
                        if (lastTouchPos.y > firstTouchPos.y)
                        {
                            //Up swipe
                            text.text = "Swipe Up!";
                            transform.Translate(new Vector3(0, moveAmount, 0));
                        }
                        else
                        {
                            //Down swipe
                            text.text = "Swipe Down!";
                            transform.Translate(new Vector3(0, -moveAmount, 0));
                        }
                    }
                }
                else
                {
                    text.text = "Tap!";
                }
            }
        }
    }
}
