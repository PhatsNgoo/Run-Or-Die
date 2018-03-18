using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurtleScript : MonoBehaviour {
    public Animator anim;
    public Rigidbody2D rgBody;
    public float runSpeed;
    public int faceDirection;
    public float wait;

    private IEnumerator coroutine;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        faceDirection = -1;
    }
    private void Update()
    {
        transform.position += new Vector3(runSpeed * Time.deltaTime * faceDirection, 0, 0);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag =="Wall") 
        {
            StartCoroutine(changeFaceDirection(wait));
        }
        //coroutine = changeFaceDirection(wait);
        //StartCoroutine(coroutine);
    }
    private IEnumerator changeFaceDirection(float wait)
    {
        
        if (faceDirection == 1)
        {
            faceDirection = -1;
            anim.SetFloat("Move", faceDirection);
            yield return new WaitForSeconds(wait);
        }
        else
        {
            faceDirection = 1;
            anim.SetFloat("Move", faceDirection);
            yield return new WaitForSeconds(wait);
        }
        
    }
}
