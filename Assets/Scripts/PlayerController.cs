using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float speed = 10;
    public TextMeshProUGUI countText;
    public GameObject winTextObject;
    public bool islevel1destroyed = false;

    private Rigidbody rb;
    private int count;
    private float movementX;
    private float movementY;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        rb = GetComponent <Rigidbody>();
        count = 0;

        SetCountText();
        winTextObject.SetActive(false);
    }

    void OnMove(InputValue movementValue) {
        Vector2 movementVector = movementValue.Get<Vector2>(); 

        movementX = movementVector.x; 
        movementY = movementVector.y;
    }

    void SetCountText() {
        countText.text = "Count: " + count.ToString();
        if(count >= 15 && !islevel1destroyed) {
            islevel1destroyed = true;
            count = 0;
            DestroyLevel1Objects();
            Destroy(GameObject.FindGameObjectWithTag("Enemy"));
            
        }
    }

    void DestroyLevel1Objects () {
        GameObject[] level1Objects = GameObject.FindGameObjectsWithTag("Level1");
        foreach (GameObject obj in level1Objects) {
            Destroy(obj);
        }
        StartCoroutine(DelayWinText());
        
    }

    private void FixedUpdate() {
    Vector3 movement = new Vector3 (movementX, 0.0f, movementY);

    rb.AddForce(movement * speed); 
   }

   private void OnCollisionEnter(Collision collision) {
    if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Enemy2")) {
        Destroy(gameObject);
        winTextObject.gameObject.SetActive(true);
        winTextObject.GetComponent<TextMeshProUGUI>().text = "You Lose!";
    }
   }

   private void OnTriggerEnter(Collider other) {
    if(other.gameObject.CompareTag("PickUp")) {
        other.gameObject.SetActive(false);
        count = count + 1;

        SetCountText();
    }
   }

   private IEnumerator DelayWinText() {
    winTextObject.SetActive(true);

    yield return new WaitForSeconds(3f);    // delay 3 seconds

    winTextObject.SetActive(false);
   }
}