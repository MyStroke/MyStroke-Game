using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Proyecto26;

[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour
{
    Dictionary<string, object> score;
    private CharacterController character;
    private Vector3 direction;
    private int jumpscore = 0;

    // score database
    private string newDocumentName = "score_" + System.Guid.NewGuid().ToString();
    private string databaseURL = "https://yrc-mystroke-default-rtdb.asia-southeast1.firebasedatabase.app/";

    public float jumpForce = 8f;
    public float gravity = 9.81f * 2f;

    private void Awake()
    {
        character = GetComponent<CharacterController>();
    }

    private void OnEnable()
    {
        direction = Vector3.zero;
    }

    private void Update()
    {
        direction += Vector3.down * gravity * Time.deltaTime;

        if (character.isGrounded)
        {
            direction = Vector3.down;

            if (Input.GetMouseButtonDown(0))
            {
                direction = Vector3.up * jumpForce;
                jumpscore++;
                Database(); // save score to database
            }
        }

        character.Move(direction * Time.deltaTime);
    }

    // save score to database
    private void Database()
    {
        score = new Dictionary<string, object>();
        score["score"] = jumpscore;
        RestClient.Post(databaseURL + newDocumentName + ".json", score)
        .Then(response => Debug.Log("Score saved to database " + response.Text))
        .Catch(error => Debug.Log(error.Message));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            FindFirstObjectByType<GameManager>().GameOver();
        }
    }
}
