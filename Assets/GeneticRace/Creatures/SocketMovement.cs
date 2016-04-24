using UnityEngine;
using System.Collections;

enum MovementOrientation
{
    Horizontal, Vertical
}

enum MovementState
{
    Positive, Negative
}

public class SocketMovement : MonoBehaviour
{
    public Socket socket = null;
    public float minAngleRange = 45;
    public float maxAngleRange = 90;
    public float minRotationSpeed = 10;
    public float maxRotationSpeed = 100;

    MovementOrientation movementOrientation;
    MovementState startMovementState;
    float startAngle;
    MovementState movementState;
    float currentAngle;
    float angleRange;
    float rotationSpeed;
    bool allowMovement = true;

	// Use this for initialization
	void Awake ()
    {
        GenerateMovement();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (allowMovement)
        {
            currentAngle += rotationSpeed * (movementState == MovementState.Positive ? 1 : -1) * Time.deltaTime;

            if (movementState == MovementState.Positive && currentAngle >= angleRange * 0.5f)
                movementState = MovementState.Negative;
            else if (movementState == MovementState.Negative && currentAngle <= angleRange * -0.5f)
                movementState = MovementState.Positive;

            currentAngle = Mathf.Clamp(currentAngle, angleRange * -0.5f, angleRange * 0.5f);
            SetMovementAngle(currentAngle);
        }
	}

    public void GenerateMovement()
    {
        int randMovementOrientation = Random.Range(0, 1);
        int randMovementState = Random.Range(0, 1);

        movementOrientation = randMovementOrientation == 1 ? MovementOrientation.Horizontal : MovementOrientation.Vertical;
        startMovementState = randMovementState == 1 ? MovementState.Negative : MovementState.Positive;
        movementState = startMovementState;

        angleRange = Random.Range(minAngleRange, maxAngleRange);
        float randAngle = Random.Range(angleRange * -0.5f, angleRange * 0.5f);
        startAngle = randAngle;
        SetMovementAngle(randAngle);

        rotationSpeed = Random.Range(minRotationSpeed, maxRotationSpeed);
    }

    public void ResetMovement()
    {
        movementState = startMovementState;
        SetMovementAngle(startAngle);
    }

    public void SetMovementAngle(float angle)
    {
        currentAngle = angle;

        Quaternion localRotation = Quaternion.identity;
        Vector3 eulerAngles = Vector3.zero;

        switch(movementOrientation)
        {
            case MovementOrientation.Horizontal:
                {
                    eulerAngles.y = angle;
                    break;
                }
            case MovementOrientation.Vertical:
                {
                    eulerAngles.x = angle;
                    break;
                }
        }

        localRotation.eulerAngles = eulerAngles;
        transform.localRotation = localRotation;
    }
}
