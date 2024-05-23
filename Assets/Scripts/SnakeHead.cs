using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SnakeHead : MonoBehaviour

{
    public float moveSpeed = 5f;
    public GameObject snakeBodyPrefab;
    public Vector2 direction = Vector2.right;

    private List<Transform> snakeBodies = new List<Transform>();
    private List<Vector2> positions = new List<Vector2>();
    private bool growSnake = false;
    private float stepDelay = 0.1f;
    private float stepTimer = 0f;

    void Start()
    {
        positions.Add(transform.position);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) && direction != Vector2.down)
            direction = Vector2.up;
        else if (Input.GetKeyDown(KeyCode.DownArrow) && direction != Vector2.up)
            direction = Vector2.down;
        else if (Input.GetKeyDown(KeyCode.LeftArrow) && direction != Vector2.right)
            direction = Vector2.left;
        else if (Input.GetKeyDown(KeyCode.RightArrow) && direction != Vector2.left)
            direction = Vector2.right;
    }

    void FixedUpdate()
    {
        stepTimer += Time.fixedDeltaTime;
        if (stepTimer >= stepDelay)
        {
            stepTimer = 0f;
            Move();
        }
    }

    void Move()
    {
        Vector2 currentPosition = transform.position;
        Vector2 newPosition = currentPosition + direction * moveSpeed * stepDelay;
        transform.position = newPosition;

        positions.Insert(0, newPosition);

        if (growSnake)
        {
            GameObject bodyPart = Instantiate(snakeBodyPrefab, positions[positions.Count - 1], Quaternion.identity);
            snakeBodies.Insert(0, bodyPart.transform);
            growSnake = false;
        }

        // Kuyruk parçalarının pozisyonlarını güncelle
        for (int i = 0; i < snakeBodies.Count; i++)
        {
            if (i + 1 < positions.Count)
            {
                snakeBodies[i].position = positions[i + 1];
            }
        }

        // Pozisyon listesini sınırla
        if (positions.Count > snakeBodies.Count + 1)
        {
            positions.RemoveAt(positions.Count - 1);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Food"))
        {
            Destroy(collision.gameObject);
            growSnake = true;
            FindObjectOfType<GameManager>().SpawnFood();
        }
        else if (collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("SnakeBody"))
        {
            // Oyun Bitti
            Time.timeScale = 0; // Oyunu durdur
        }
    }
}



