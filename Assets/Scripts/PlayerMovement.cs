using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Vector3 target;
    private float speed = 1f;

    //private void OnMouseDown()
    //{
    //    target = Camera.main.ScreenToWorldPoint( Input.mousePosition );
    //    target.z = transform.position.z;
    //    transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
    //    print("Click");
    //}

    // Start is called before the first frame update
    void Start()
    {
        target = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButton(0))
        {
            target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            target.z = transform.position.z;
        }

        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
    }
}
