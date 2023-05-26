using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    private Vector2 _direction = Vector2.right;
    private List<Transform> _snakeSegments = new List<Transform>();
    public Transform snakeSegmentPrefab;
    public int initialSize = 4;

    private void Start()
    {
        ResetState();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.W))
        {
            if (_direction != Vector2.down)
            {
                _direction = Vector2.up;
            }
        }
        else if(Input.GetKeyDown(KeyCode.S))
        {
            if (_direction != Vector2.up)
            {
                _direction = Vector2.down;
            }
        }
        else if(Input.GetKeyDown(KeyCode.A))
        {
            if (_direction != Vector2.right)
            {
                _direction = Vector2.left;
            }
        }
        else if(Input.GetKeyDown(KeyCode.D))
        {
            if(_direction != Vector2.left)
            {
                _direction = Vector2.right;
            }
        }
    }

    private void FixedUpdate()
    {
        for (int i = _snakeSegments.Count - 1; i > 0; i--)
        {
            _snakeSegments[i].position = _snakeSegments[i - 1].position;
        }
        
        this.transform.position = new Vector3(Mathf.Round(this.transform.position.x) + _direction.x, Mathf.Round(this.transform.position.y) + _direction.y, 0.0f);
    }

    private void Grow()
    {
        Transform segment = Instantiate(this.snakeSegmentPrefab);
        segment.position = _snakeSegments[_snakeSegments.Count - 1].position;

        _snakeSegments.Add(segment);
    }

    private void ResetState()
    {
        for (int i = 1; i < _snakeSegments.Count; i++)
        {
            Destroy(_snakeSegments[i].gameObject);

        }
        _snakeSegments.Clear();
        _snakeSegments.Add(this.transform);

        for (int i = 1; i < this.initialSize; i++)
        {
            _snakeSegments.Add(Instantiate(this.snakeSegmentPrefab));
        }

        this.transform.position = Vector3.zero;
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Food")
        {
            Grow();
        }
        else if(other.tag == "Obstacle")
        {
            ResetState();
        }
    }
}
