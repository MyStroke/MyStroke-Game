using UnityEngine;
using Firebase.Firestore;
using System.Collections.Generic;

[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour
{

    FirebaseFirestore db;
    Dictionary<string, object> score;
    private CharacterController character;
    private Vector3 direction;
    private int jumpscore = 0;

    public float jumpForce = 8f;
    public float gravity = 9.81f * 2f;

    private void Awake()
    {
        db = FirebaseFirestore.DefaultInstance;
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

            if (Input.GetMouseButtonDown(0)) {
                direction = Vector3.up * jumpForce;
                jumpscore++;
                Updatescore();
            }
        }

        character.Move(direction * Time.deltaTime);
    }

    private void Updatescore() {
        string newDocumentName = "score_" + System.Guid.NewGuid().ToString();
        DocumentReference docRef = db.Collection("user-score").Document("XWffAwRUY1X5WNXufZ8q");
        score = new Dictionary<string, object>
        {
            { newDocumentName, jumpscore }
        };
        docRef.UpdateAsync(score).ContinueWith(task => {
            if (task.IsCompleted)
            {
                Debug.Log("Score updated");
            }

            else {
                Debug.Log("Score not updated");
            }
        });
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle")) {
            FindObjectOfType<GameManager>().GameOver();
        }
    }

}