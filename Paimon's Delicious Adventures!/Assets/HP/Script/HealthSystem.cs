using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour
{
    public int health;
    public int numberOfLives;
    [SerializeField] private GameObject losePanel;
    private CharacterController controller;

    public Image[] lives;

    // private int lose = 0;

    public Sprite fullLive;
    public Sprite emptyLive;
    
    void Start()
    {
        
    }

    
    void Update()
    {
        if(health > numberOfLives)
        {
            health = numberOfLives;
        }

        for(int i = 0; i < lives.Length; i++)
        {
            if(i < health)
            {
                lives[i].sprite = fullLive;
            }
            else
            {
                lives[i].sprite = emptyLive;
            }

            if(i < numberOfLives)
            {
                lives[i].enabled = true;
            }
            else
            {
                lives[i].enabled = false;
            }
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if(hit.gameObject.tag == "obstacle")
        {
            health--;
            Destroy(hit.gameObject);
            if(health == 0)
            {
                losePanel.SetActive(true);
                Time.timeScale = 0;
            }
        }
    }
}
