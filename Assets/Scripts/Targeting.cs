using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityStandardAssets.CrossPlatformInput;

public class Targeting : MonoBehaviour
{
    public static Transform target;
    public SpriteRenderer selected;

    public Canvas combat;

    public static bool canAttack = false;

    private void Start()
    {
        target = null;
        selected.enabled = false;
        combat.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount == 1)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                //Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition);
                Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                Vector2 touchPos = new Vector2(pos.x, pos.y);
                
                var hit = Physics2D.OverlapPoint(touchPos);
                print(hit.name);

                if (hit.transform.CompareTag("Enemy"))
                {
                    //Tapped, target the enemy - lock on
                    target = this.transform;
                    //Feedback - show that you are locked on
                    this.selected.enabled = true;
                    canAttack = true;
                    print("Enemy Clicked");
                }
                else if (hit.transform.CompareTag("Ground") && !IsMouseOverUI())
                {
                    //Missed - if target exists, deselect target
                    if (target != null) target = null;
                    //Show that its deselected
                    this.selected.enabled = false;
                    canAttack = false;
                    print("Ground Clicked");
                }

                combat.enabled = canAttack;

                if (IsMouseOverUI())
                {
                    print("UI Clicked!");
                }
            }
            else if (Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                //Enemy targetted, allow attacks - use a bool
                
            }
        }
    }

    private bool IsMouseOverUI()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }

    //private RaycastHit GetTouchLocation()
    //{
    //    Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition);
    //    Vector2 touchPos = new Vector2(pos.x, pos.y);

        

    //    return hit;
    //}
}
