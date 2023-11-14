using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityStandardAssets.CrossPlatformInput;

public class Targeting : MonoBehaviour
{
    private Transform target;
    public SpriteRenderer selected;

    public GameObject attackButton;

    public static bool canAttack = false;

    private PlayerInput player;

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
                Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition);
                Vector2 touchPos = new Vector2(pos.x, pos.y);
                
                var hit = Physics2D.OverlapPoint(touchPos);
                print(hit.name);

                if (hit.transform.CompareTag("Enemy"))
                {
                    //Tapped, target the enemy - lock on
                    target = hit.transform;
                    //Feedback - show that you are locked on
                    selected.enabled = true;
                    canAttack = true;
                }
                else if (hit.transform.CompareTag("Ground") && !IsMouseOverUI())
                {
                    //Missed - if target exists, deselect target
                    if (target != null) target = null;
                    //Show that its deselected
                    selected.enabled = false;
                    canAttack = false;
                }

                attackButton.SetActive(canAttack);

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
