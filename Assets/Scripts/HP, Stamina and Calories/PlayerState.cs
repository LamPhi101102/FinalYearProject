using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    // create a singleton
    public static PlayerState Instance { get; set; }
    // Create properties
    // ===========> Play HP <===========
    // Using public in order to easy to test
    public float currentHP;
    public float maxHP;
    
    // ===========> Play Stamina <===========
    public float currentStamina;
    public float maxStamina;

    float distanceTravelled = 0;
    Vector3 lastPosition;
    public GameObject playerBody;

    // ===========> Play Calories <===========
    public float currentCaloriesPercent;
    public float maxCaloriesPercent;

    public bool isColoriesActions;

    // ======= Animation ===============
    Animator animator;


    public void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        // Player will have a max HP board when starting
        animator = GetComponent<Animator>();
        currentHP = maxHP;
        currentStamina = maxStamina;
        currentCaloriesPercent = maxCaloriesPercent;
        StartCoroutine(decreaseCalories());      
    }

    IEnumerator decreaseCalories()
    {
        while (true)
        {
            currentCaloriesPercent -= 1;
            yield return new WaitForSeconds(10);
        }
    }

    public void TakeDamage(float damageAmount)
    {
        currentHP -= damageAmount;
        animator.SetTrigger("damage");

        if(currentHP <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(this.gameObject);
    }

        // Update is called once per frame
    void Update()
    {
        // get the distance
        distanceTravelled += Vector3.Distance(playerBody.transform.position, lastPosition);
        // assign the position to lastposition
        lastPosition = playerBody.transform.position;
        //StartCoroutine(checkDistance());

        //check if it is greater than 5, reset the distance and - 1 calories
        if (distanceTravelled >= 5)
        {
            distanceTravelled = 0;
            currentStamina -= 1;
        }       
        // Testing with by pressing N
        if (Input.GetKeyDown(KeyCode.N))
        {
            currentHP -= 10;
        }
    }



    // set new HP
    public void setHealth(float newHP)
    {
        currentHP = newHP;
    }
    // Set New Stamina
    public void setStamina(float newStamina)
    {
        currentStamina = newStamina;
    }
    // Set New Calories
    public void setCalories(float newCalories)
    {
        currentCaloriesPercent = newCalories;
    }
    // Set for equip armor and weapon
    public void setMaxHealth(float newmaxHP)
    {
        maxHP = newmaxHP;
    }
    // Set New Stamina
    public void setMaxStamina(float newmaxStamina)
    {
        maxStamina = newmaxStamina;
    }

}
