using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeController : MonoBehaviour
{
    // control the fast of time
    [SerializeField]
    private float timeMultiplier;

    // the hour the game start at
    [SerializeField]
    private float startHour;

    // display the time
    [SerializeField]
    private TextMeshProUGUI timeText;

    // assign the light
    [SerializeField]
    private Light sunLight;

    // adjust sun
    [SerializeField]
    private float sunriseHours;

    [SerializeField]
    private float sunsetHour;

    [SerializeField]
    private Color dayAmbientLight;

    [SerializeField]
    private Color nightAmbientLight;

    [SerializeField]
    private AnimationCurve lightChangeCurve;

    // adjust the itensity of sun Light
    [SerializeField]
    private float maxSunLightIntensity;

    [SerializeField]
    private Light moonlight;

    // adjust the itensity of sun Light
    [SerializeField]
    private float maxMoonLightIntensity;

    private DateTime currentTime;

    private TimeSpan sunriseTime;

    private TimeSpan sunsetTime;
    // Start is called before the first frame update
    void Start()
    {
        currentTime = DateTime.Now.Date + TimeSpan.FromHours(startHour);

        sunriseTime = TimeSpan.FromHours(sunriseHours);
        sunsetTime = TimeSpan.FromHours(sunsetHour);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateTimeOfDay();
        RotationSun();
        UpdateLightSettings();
    }

    private void UpdateTimeOfDay()
    {
        currentTime = currentTime.AddSeconds(Time.deltaTime * timeMultiplier);

        if(timeText != null)
        {
            timeText.text = currentTime.ToString("HH:mm");
        }
    }

    private void RotationSun()
    {
        float sunLightRotaion;

        if(currentTime.TimeOfDay > sunriseTime && currentTime.TimeOfDay < sunsetTime)
        {
            // calculate the time between senrise and sunset
            TimeSpan sunrisetoSunsetDuration = CalculateTimeDifferent(sunriseTime, sunsetTime);
            // calculate the real time between sunriseTime and curentime
            TimeSpan timeSinceSunrise = CalculateTimeDifferent(sunriseTime, currentTime.TimeOfDay);

            // calculate minutes
            double percentage = timeSinceSunrise.TotalMinutes / sunrisetoSunsetDuration.TotalMinutes;

            // sun will ratation based on percentage
            sunLightRotaion = Mathf.Lerp(0, 180, (float)percentage);
        }
        else
        {
            // calculate the time between senset and sunrise
            TimeSpan sunsettoSunRiseDuration = CalculateTimeDifferent(sunsetTime, sunriseTime);
            // calculate the real time between sunset and curentime
            TimeSpan timeSinceSunSet = CalculateTimeDifferent(sunsetTime, currentTime.TimeOfDay);

            // calculate minutes
            double percentage = timeSinceSunSet.TotalMinutes / sunsettoSunRiseDuration.TotalMinutes;
            // sun will ratation based on percentage
            sunLightRotaion = Mathf.Lerp(180,360, (float)percentage);
        }
        //link to sunLight to change rotation in unity through this statements
        sunLight.transform.rotation = Quaternion.AngleAxis(sunLightRotaion, Vector3.right);
    }

    private void UpdateLightSettings()
    {
        float dotProduct = Vector3.Dot(sunLight.transform.forward, Vector3.down);
        sunLight.intensity = Mathf.Lerp(0, maxSunLightIntensity, lightChangeCurve.Evaluate(dotProduct));
        moonlight.intensity = Mathf.Lerp(maxMoonLightIntensity, 0, lightChangeCurve.Evaluate(dotProduct));
        RenderSettings.ambientLight = Color.Lerp(nightAmbientLight, dayAmbientLight, lightChangeCurve.Evaluate(dotProduct));
    }

    // calcualte to determine sunset and sunrise
    private TimeSpan CalculateTimeDifferent(TimeSpan fromtime, TimeSpan toTime)
    {
        TimeSpan different = toTime - fromtime;

        if (different.TotalSeconds < 0)
        {
            different += TimeSpan.FromHours(24);
        }
        return different;
    }
}
