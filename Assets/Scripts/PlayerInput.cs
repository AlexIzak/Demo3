using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerInput : MonoBehaviour
{
    private Rigidbody2D rb;
    private float speed = 1f;

    public GameObject spell;

    public float castingCD;
    private float curCastingCD;

    public float spellSpeed;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 moveVec = new Vector3(CrossPlatformInputManager.GetAxis("Horizontal"), CrossPlatformInputManager.GetAxis("Vertical"), 0);

        if(moveVec.magnitude > 1 )
        {
            moveVec = moveVec.normalized;
        }

        rb.velocity = moveVec * speed;

        curCastingCD += Time.deltaTime;
        if(curCastingCD > castingCD ) 
        {
            if(Mathf.Abs(CrossPlatformInputManager.GetAxis("Horizontal_2")) > 0 || Mathf.Abs(CrossPlatformInputManager.GetAxis("Vertical_2")) > 0)
            {
                GameObject s = Instantiate(spell, transform.position, Quaternion.identity);
                s.GetComponent<Rigidbody2D>().velocity = new Vector2(CrossPlatformInputManager.GetAxis("Horizontal_2"), CrossPlatformInputManager.GetAxis("Vertical_2")).normalized * spellSpeed;

                curCastingCD = 0;
            }
        }
    }
}
