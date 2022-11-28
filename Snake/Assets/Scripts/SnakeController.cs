using UnityEngine;
using System.Collections.Generic;
using System;

public class SnakeController : MonoBehaviour
{
    private Vector2 _direction = Vector2.right;
    public Transform _segmentPrefab;
    private List<Transform> _segments = new List<Transform>();

    private void Start() 
    {
        ResetState();
    }

    private void Update() 
    {
        if(Input.GetKeyDown(KeyCode.W))
            _direction = Vector2.up;
        else if(Input.GetKeyDown(KeyCode.S))
            _direction = Vector2.down;
        else if(Input.GetKeyDown(KeyCode.A))
            _direction = Vector2.left;
        else if(Input.GetKeyDown(KeyCode.D))
            _direction = Vector2.right;
    }

    private void FixedUpdate() 
    {
        for (int i = _segments.Count -1 ; i > 0; i--)
        {
            _segments[i].position = _segments[i-1].position;
        }

        this.transform.position = new Vector3 (
            Mathf.Round(this.transform.position.x) + _direction.x,
            Mathf.Round(this.transform.position.y) + _direction.y,
            0.0f
       );

    }
    private void Grow()
    {
        Transform segment = Instantiate(_segmentPrefab);
        segment.position = _segments[_segments.Count -1 ].position;
        _segments.Add(segment);
    }   


    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Food"))
        {
            Grow();
        }    
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.collider.CompareTag("Player"))
        {
            Debug.Log("Mi son morso");
            ResetState();
        }
    }

    private void ResetState()
    {
        for (int i = 1 ; i < _segments.Count; i++)
        {
            Destroy(_segments[i].gameObject);
        }
        _segments.Clear();
        _segments.Add(this.transform);
        this.transform.position = Vector3.zero;
    }
}
