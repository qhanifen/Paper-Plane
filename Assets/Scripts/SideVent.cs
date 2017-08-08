using UnityEngine;
using System.Collections;

public class SideVent : Obstacle {

    public float blowForce;

    public enum Direction
    {    
        Forward,
        Backward,
        Left,
        Right
    }

    public Direction direction;

    void OnEnable()
    {
        RandomizeDirection();
        FaceDirection(direction);
    }

	void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.tag == "Player")
        {
            switch(direction)
            {                
                case Direction.Left:
                    collider.attachedRigidbody.AddForce(blowForce * Vector3.left);
                    break;
                case Direction.Right:
                    collider.attachedRigidbody.AddForce(blowForce * Vector3.right);
                    break;
                default:
                    break;
            }
        }
    }

    void OnTriggerStay(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            switch (direction)
            {
                case Direction.Forward:
                    LevelManager.instance.Deccelerate();
                    break;
                case Direction.Backward:
                    LevelManager.instance.Accelerate();
                    break;
                default:
                    break;
            }
        }
    }

    void OnTriggerExit()
    {
        LevelManager.instance.StartCoroutine(LevelManager.ResetSpeed());
    }

    public void FaceDirection(Direction faceDirection)
    {
        direction = faceDirection;
        switch (direction)
        {
            case Direction.Forward:
                transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
                break;
            case Direction.Backward:
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
                break;
            case Direction.Left:
                transform.rotation = Quaternion.Euler(new Vector3(0, 270, 0));
                break;
            case Direction.Right:
                transform.rotation = Quaternion.Euler(new Vector3(0, 90, 0));
                break;
        }
    }

    void RandomizeDirection()
    {
        direction = (Direction)Random.Range(0, 4);
    }

}
