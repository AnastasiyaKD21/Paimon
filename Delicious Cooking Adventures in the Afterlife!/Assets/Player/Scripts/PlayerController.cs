using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 dir;
    private Animator anim;
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float gravity;
    [SerializeField] private int mora;
    [SerializeField] private Text moraText;
    [SerializeField] private int stone;
    [SerializeField] private Text stoneText;
    [SerializeField] private int recipe;
    [SerializeField] private Text recipeText;
    [SerializeField] private GameObject winPanel;
    // [SerializeField] private GameObject losePanel;
    // [SerializeField] private GameObject healthBar;

    private int lineToMove = 1;
    public float lineDistance = 4; 
    private float maxSpeed = 110;
    private int gloat = 4;

    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        controller = GetComponent<CharacterController>();
        StartCoroutine(SpeedIncrease());
        moraText.text = ": " + mora.ToString();
        stoneText.text = ": " + stone.ToString();
        recipeText.text = ": " + recipe.ToString() + "/4";
    }

    private void Update()
    {
        if(SwipeController.swipeRight)
        {
            if(lineToMove < 2)
                lineToMove++;
        }

        if(SwipeController.swipeLeft)
        {
            if(lineToMove > 0)
                lineToMove--;
        }

        if(SwipeController.swipeUp)
        {
            if(controller.isGrounded)
                Jump();
        }

        if (controller.isGrounded)
        anim.SetBool("isRun", true);
        else
        anim.SetBool("isRun", false);

        Vector3 targetPosition = transform.position.z * transform.forward + transform.position.y * transform.up;
        if(lineToMove == 0)
            targetPosition += Vector3.left * lineDistance;
        else if(lineToMove == 2)
            targetPosition += Vector3.right * lineDistance;
        
        if(transform.position == targetPosition)
            return;
        Vector3 diff = targetPosition - transform.position;
        Vector3 moveDir = diff.normalized * 25 * Time.deltaTime;
        if(moveDir.sqrMagnitude < diff.sqrMagnitude)
            controller.Move(moveDir);
        else
            controller.Move(diff);
    }

    private void Jump()
    {
        dir.y = jumpForce;
        anim.SetTrigger("isJump");
    }

    void FixedUpdate()
    {
        dir.z = speed;
        dir.y += gravity * Time.fixedDeltaTime;
        controller.Move(dir * Time.fixedDeltaTime);
    }

    private IEnumerator SpeedIncrease()
    {
        yield return new WaitForSeconds(3);
        if(speed < maxSpeed)
        {
            speed += 2;
            StartCoroutine(SpeedIncrease()); 
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Mora")
        {
            mora++;
            moraText.text = ": " + mora.ToString();
            Destroy(other.gameObject);
        }

        if(other.gameObject.tag == "Stone")
        {
            stone++;
            stoneText.text = ": " + stone.ToString();
            Destroy(other.gameObject);
        }

        if(other.gameObject.tag == "Recipe")
        {
            recipe++;
            recipeText.text = ": " + recipe.ToString() + "/4";
            Destroy(other.gameObject);
        }

        if(recipe == gloat)
        {
            winPanel.SetActive(true);
            Time.timeScale = 0;
        }
    }

    // private void OnControllerColiderHit(ControllerColiderHit hit)
    // {

    // }
}
