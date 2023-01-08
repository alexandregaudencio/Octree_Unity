using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ObjectController : MonoBehaviour
{

    [SerializeField][Range(0, 10)] private int speed = 2;
    private Vector3 direction;
    private SphereCollider sphereCollider;
    public Bounds bound => sphereCollider.bounds;
    private float radius => sphereCollider.radius;

    private void Awake()
    {
        sphereCollider = gameObject.GetComponent<SphereCollider>();

    }
    private void Start()
    {
        //randomize direction on start
        direction = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f));

    }
    private void Update()
    {
        OutsideBoundary(transform.position);
        transform.position += direction * speed * Time.deltaTime;

    }


    private void OutsideBoundary(Vector3 targetPosition)
    {
        Vector3 boundarySimulationVector = Vector3.one * GameManager.instance.worldSize;
        if (targetPosition.x + radius > boundarySimulationVector.x) direction.x *= -1;
        if (targetPosition.y + radius > boundarySimulationVector.y) direction.y *= -1;
        if (targetPosition.z + radius > boundarySimulationVector.z) direction.z *= -1;
        if (targetPosition.x + -radius < -boundarySimulationVector.x) direction.x *= -1;
        if (targetPosition.y + -radius < -boundarySimulationVector.y) direction.y *= -1;
        if (targetPosition.z + -radius < -boundarySimulationVector.z) direction.z *= -1;
    }

    public void ChangeDirection(Vector3 target)
    {
        direction = target;
    }

}
