using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyScript : MonoBehaviour {
    public Animator anim;
    public Rigidbody2D rgBody;
    public float flySpeed;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        transform.position -= new Vector3(flySpeed * Time.deltaTime, 0, 0);
    }
}
