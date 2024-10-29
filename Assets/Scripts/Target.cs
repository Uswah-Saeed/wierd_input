using System.Collections;
using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float moveDistance = 5f;
    
    private Vector3 startPosition;
    private float currentOffset = 0f;
    private bool movingRight = true;

    private void Start()
    {
        startPosition = transform.position;
    }

    private void Update()
    {
        // side-to-side movement
        if (movingRight)
        {
            currentOffset += moveSpeed * Time.deltaTime;
            if (currentOffset >= moveDistance)
            {
                movingRight = false;
            }
        }
        else
        {
            currentOffset -= moveSpeed * Time.deltaTime;
            if (currentOffset <= -moveDistance)
            {
                movingRight = true;
            }
        }

        transform.position = startPosition + Vector3.right * currentOffset;
    }

    public void OnHit()
    {
        StartCoroutine(DisableTemporarily());
    }

    private IEnumerator DisableTemporarily()
    {
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<Collider>().enabled = false;
        yield return new WaitForSeconds(2f);
        GetComponent<MeshRenderer>().enabled = true;
        GetComponent<Collider>().enabled = true;
    }
}