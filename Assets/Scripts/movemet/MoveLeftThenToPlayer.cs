using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeftThenToPlayer : MonoBehaviour
{
    [SerializeField] bool isInGame = false;

    public float speed = 5;
    public float attackSpeed = 15;
    public float rotateSpeed = 50;
    public float attackDistanceTrigger = 12;
    private float xBoundary = -7;
    private GameObject player;
    private int rotationAngle;
    private bool isInAttackMode = false;
    private bool isInRotationMode = false;

    private void Start()
    {
        rotationAngle = Random.Range(20, 46); ;
        player = GameObject.Find("Player");
    }

    public void BeginGame()
    {
        isInGame = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isInGame)
        {
            float xDistanceToPlayer = transform.position.x - player.transform.position.x;
            if ((!isInAttackMode && xDistanceToPlayer < attackDistanceTrigger) || isInRotationMode)
            {
                if (transform.rotation.eulerAngles.z <= rotationAngle)
                {
                    isInRotationMode = true;
                    transform.Rotate(Vector3.forward * Time.deltaTime * rotateSpeed);
                }
                else
                {
                    isInAttackMode = true;
                    isInRotationMode = false;
                }
            }
            else if (isInAttackMode)
            {
                transform.Translate(Vector3.left * Time.deltaTime * attackSpeed);
            }
            else
            {
                transform.Translate(Vector3.left * Time.deltaTime * speed);
            }
            if (transform.position.x < xBoundary && !gameObject.CompareTag("Ground") && !gameObject.CompareTag("GroundParent")) Destroy(gameObject);
        }
    }

}
