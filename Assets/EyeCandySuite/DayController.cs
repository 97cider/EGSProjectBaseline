using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayController : MonoBehaviour
{
    public Gradient nightDayColor;

    public Light mainLight;
    public float cycleTime, speedMultiplier;
    public float currentTime = 0.0f;
    public GameObject Stars;
    public GameObject FireFlies;

    void Start()
    {
        //do all that component shit later
        //mainLight = GetComponent<Light>();

    }
    //oof
    //this can probably be streamlined but fuck it
    //need to connect to a manager down the road i guess
    //man this is some spaghetti-ass code
    void Update()
    {
        if (currentTime < cycleTime)
        {
            currentTime += Time.deltaTime;
        }
        else
        {
            currentTime = 0;
        }
        if (currentTime <= cycleTime / 8)
        {
            //its still close to night i guess
            if (!Stars.activeSelf)
            {
                Stars.SetActive(true);
            }
            if (!FireFlies.activeSelf)
            {
                FireFlies.SetActive(true);
            }
        }
        else
        {
            if (Stars.activeSelf)
            {
                Stars.SetActive(false);
            }
            if (!FireFlies.activeSelf)
            {
                FireFlies.SetActive(false);
            }
        }
        mainLight.color = nightDayColor.Evaluate(currentTime / speedMultiplier);
        RenderSettings.ambientLight = mainLight.color;
    }
}
