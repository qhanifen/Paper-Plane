using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    #region Movement Variables
    public float moveSpeed = 5.0f;
    public float fallAngle = 5.0f;
    public float riseAngle = 10.0f;
    public float boundsX;
    #endregion

    Animator anim;    

    // Use this for initialization
    void Start ()
    {
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        float inputX = Input.acceleration.x;
#if UNITY_EDITOR
        if(Input.GetKey(KeyCode.LeftControl))
            inputX = Input.GetAxisRaw("Mouse X");
#endif

        if ((inputX > 0f || inputX < 0f) && !GameManager.instance.gameOver)
        {
            //Debug.Log(inputX);
            transform.rotation = Quaternion.Slerp(Quaternion.identity, Quaternion.Euler(new Vector3(0, 0, 20.0f) * Mathf.Sign(inputX)), Mathf.Abs(inputX));
            float newX = Mathf.Clamp(transform.position.x + (inputX * moveSpeed * Time.fixedDeltaTime), -boundsX, boundsX);
            Vector3 newPos = new Vector3(newX, transform.position.y, transform.position.z);
            transform.position = newPos;
        }
	}

    void OnCollisionEnter(Collision collision)
    {
        anim.SetBool("Game Over", true);
        GameManager.GameOver();
        Rigidbody rigid = GetComponent<Rigidbody>();
        rigid.drag = 0;
    }
}
