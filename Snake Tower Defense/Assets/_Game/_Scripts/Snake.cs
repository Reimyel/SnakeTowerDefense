using UnityEngine;
using System.Collections.Generic;

public class Snake : MonoBehaviour
{
    Vector2 _direction = Vector2.right;
    List<Transform> _segments = new List<Transform>();
    public Transform SegmentPrefab;
    public int InitialSize = 1;
    public int CoinAmount;
    public bool CanMove = true;

    private void Start()
    {
        ResetState();
    }

    private void Update()
    {
        if (Pause.IsPaused) return;
        else
        {
            Vector2 newDirection = _direction;

            if (Input.GetKeyDown(KeyCode.W)) { newDirection = Vector2.up; }
            else if (Input.GetKeyDown(KeyCode.S)) { newDirection = Vector2.down; }
            else if (Input.GetKeyDown(KeyCode.A)) { newDirection = Vector2.left; }
            else if (Input.GetKeyDown(KeyCode.D)) { newDirection = Vector2.right; }

            if (_segments.Count > 2 && newDirection == -_direction) { return; }

            _direction = newDirection;
            //normal direction
        }

    }

    private void FixedUpdate()
    {
        if (Pause.IsPaused) return;
        else
        {
            if (CanMove)
            {
                Move();
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Pebble")
        {
            Grow();
            CoinAmount++;
        }
        else if (other.tag == "Obstacle")
        {
            ResetState();
        }
    }

    public void Move()
    {
        for (int i = _segments.Count - 1; i > 0; i--) { _segments[i].position = _segments[i - 1].position; }

        this.transform.position = new Vector3(
                Mathf.Round(this.transform.position.x) + _direction.x,
                Mathf.Round(this.transform.position.y) + _direction.y,
                0f);
        //mathf.round makes the position "fit" into the grid
    }

    void Grow()
    {
        Transform segment = Instantiate(SegmentPrefab);
        segment.position = _segments[_segments.Count - 1].position;
        _segments.Add(segment);
    }

    public void ResetState()
    {
        for (int i = 1; i < _segments.Count; i++) { Destroy(_segments[i].gameObject); }

        _segments.Clear();
        _segments.Add(this.transform);

        for (int i = 1; i < InitialSize; i++) { _segments.Add(Instantiate(SegmentPrefab)); }

        this.transform.position = Vector3.zero;
    }
}
