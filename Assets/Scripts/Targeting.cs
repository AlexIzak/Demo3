using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Targeting : MonoBehaviour
{
    private Transform target;
    public SpriteRenderer selected;

    public GameObject attackButton;

    public static bool canAttack = false;

    private void Start()
    {
        target = null;
        selected.enabled = false;
        attackButton.SetActive(false);
    }

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
                if (hit.transform.CompareTag("Enemy"))
                {
                    //Tapped, target the enemy - lock on
                    target = hit.transform;
                    //Feedback - show that you are locked on
                    selected.enabled = true;
                    attackButton.SetActive(true);
                    canAttack = true;
                }
                else if(hit.transform.CompareTag("Ground"))
                {
                    //Missed - if target exists, deselect target
                    if (target != null) target = null;
                    //Show that its deselected
                    selected.enabled = false;
                    attackButton.SetActive(false);
                }
            }
            else if (Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                //Enemy targetted, allow attacks - use a bool
                
            }
        }
    }
}
