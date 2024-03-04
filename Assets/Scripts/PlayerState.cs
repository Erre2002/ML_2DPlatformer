using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{

    public int healthPoints = 2;
    public int initialHealthPoints = 2;

    public int coinAmount = 0;

    public int healthPickup = 0;

    public int initialBreath = 10;

    public int breath = 10;

    private float timeRemaining = 2.5f;

    private bool done = false;

    private bool isInWater = false;

    private int useBreath = 1;

    public GameObject respawnPosition;
    [SerializeField] public GameObject startPosition;
    [SerializeField] private bool useStartPosition = true;

    void Start()
    {
        healthPoints = initialHealthPoints;
        breath = initialBreath;
        if (useStartPosition == true) { 
            gameObject.transform.position = startPosition.transform.position;
        }
        respawnPosition = startPosition;
    }

    void Update()
    {
        checkTime();
    }
    //timeRemaining = 3
    //breath = 10 at start/respawn

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Water") == true)
        {
            isInWater = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Water") == true)
        {

            isInWater = false;
            breath = initialBreath;

        }
    }

    private void checkTime()
    {
        if(isInWater == true)
        {
            
            if (timeRemaining > 1f)
            {
            
            
                timeRemaining -= Time.deltaTime;
                print(timeRemaining);
            
            }
            if (timeRemaining <= 1f)
            {
                done = true;

                breath -= useBreath;

                timeRemaining = 2.5f;
                print(timeRemaining + " andra");
            }

            if (breath <= 0)
            {
                Respawn();
            }
        }

        

        
    }
    
    /*public void ExhertedBreath(int useBreath)
    {
        if (breath > 0)
        {

            if(done == true)
            {
                
                print(breath);
                timeRemaining = 3f;
            }
            
        }

        
        if(breath <= 0)
        {
            Respawn();
        }
        
    }*/

    public void DoHarm(int doHarmByThisMuch)
    {
        healthPoints -= doHarmByThisMuch;
        if (healthPoints <= 0)
        {
            Respawn();
        } 
    }

    public void Respawn()
    {
        healthPoints = initialHealthPoints;
        breath = initialBreath;
        gameObject.transform.position = respawnPosition.transform.position;
    }

    public void CoinPickup()
    {
        coinAmount++;
        
    }

    public void HealthPickup()
    {
        healthPoints = initialHealthPoints;
    }


    public void ChangeRespawnPosition(GameObject newRespawnPosition)
    {
        respawnPosition = newRespawnPosition;
    }

    
}
