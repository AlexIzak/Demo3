using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StatusEffects : MonoBehaviour
{
    //Visuals for effects
    private TMP_Text timer;
    public static bool active;

    private void Start()
    {
        timer = GetComponentInChildren<TMP_Text>();
    }

    public void DisplayEffect(GameObject effect, float duration)
    {
        if(duration > 0)
        {
            active = true;
            effect.SetActive(active);
            duration -= Time.deltaTime;
            timer.text = ((int)duration).ToString();
        }
        else
        {
            active = false;
            effect.SetActive(active);
        }
    }

    private void Update()
    {
        
    }
}
