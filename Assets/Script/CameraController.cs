using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CameraController : MonoBehaviour {
	public List<Transform> targets;
    public HealthScript healthScript;
    public int cameraSpeed;
	public float distance;
    private void Awake()
    {
        healthScript = (HealthScript)FindObjectOfType<HealthScript>();
    }
    void LateUpdate () {
        // 188 162 262
        if (targets.Max((target) => target.position.x > transform.position.x - distance))
            transform.position = transform.position.WithX(targets.Max((target) => target.position.x) + distance);
        else if (!healthScript.isPausing){ transform.position += new Vector3(cameraSpeed,0,0); }
	}
}
