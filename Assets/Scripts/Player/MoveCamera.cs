using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    public Transform camPos;
    public Transform meleeAttackSpawn;
    public Transform mainCam;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = camPos.position;

        // Make meleeAttackSpawn look forward in the same direction as the camera
        meleeAttackSpawn.rotation = Quaternion.LookRotation(mainCam.forward, Vector3.up);
    }
}
