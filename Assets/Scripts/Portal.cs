using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float rotateSpeed;
    private LevelManager levelManager;
    Vector2 newPosition;

    void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
        ChangePosition();
    }

    void Update()
    {
        float distanceToNewPosition = Vector3.Distance(transform.position, newPosition);
        if (distanceToNewPosition < 0.5f)
        {
            ChangePosition();
        }

        transform.position = Vector3.Lerp(transform.position, newPosition, speed * Time.deltaTime);
        transform.Rotate(Vector3.forward, rotateSpeed * Time.deltaTime);

        if (Player.Instance.hasWeapon == true)
        {
            gameObject.GetComponent<Renderer>().enabled = true;
            gameObject.GetComponent<Collider2D>().enabled = true;
        }
        else
        {
            gameObject.GetComponent<Renderer>().enabled = false;
            gameObject.GetComponent<Collider2D>().enabled = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
           levelManager.LoadScene("Main");
        }
    }
    void ChangePosition()
    {
        newPosition = new Vector3(
            Random.Range(-10f, 10f),
            Random.Range(-5f, 5f),
            0f
        );
    }

}
