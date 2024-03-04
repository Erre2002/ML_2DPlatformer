using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_PlayerBreath : MonoBehaviour
{
    public PlayerState playerState;

    private Slider slider;


    int maxplayerBreath;



    // Start is called before the first frame update
    void Start()
    {
        maxplayerBreath = playerState.initialBreath;
        slider = gameObject.GetComponent<Slider>();
        slider.wholeNumbers = true;
        slider.maxValue = maxplayerBreath;
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = playerState.breath;

    }
}
