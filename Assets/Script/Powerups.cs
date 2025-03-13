using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerups : MonoBehaviour
{
    int randomNum = 0;
    void Start()
    {
        randomNum = Random.Range(1, 5);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            switch (randomNum)
            {
                case 1:
                    // Effect for power-up 1
                    Debug.Log("Power-up 1 activated!");
                    break;
                case 2:
                    // Effect for power-up 2
                    Debug.Log("Power-up 2 activated!");
                    break;
                case 3:
                    // Effect for power-up 3
                    Debug.Log("Power-up 3 activated!");
                    break;
                case 4:
                    // Effect for power-up 4
                    Debug.Log("Power-up 4 activated!");
                    break;
                case 5:
                    // Effect for power-up 5
                    Debug.Log("Power-up 5 activated!");
                    break;
            }
        }
    }
}
