using System.Collections;
using UnityEngine;

public class MovingPlatforms : MonoBehaviour
{
    private Transform finalPosObj;
    public float speed;
    public bool moving = false;

    private void Start()
    {
        finalPosObj = transform.Find("FinalPos");
    }

    public IEnumerator StartMovement()
    {
        Debug.Log("started function");
        moving = true;
        Debug.Log("started movement");
        Vector3 finalPos = finalPosObj.position;
        while ((transform.position - finalPos).magnitude > 0.01f)
        {
            transform.position = Vector3.Lerp(transform.position, finalPos, speed * Time.deltaTime);
            yield return null;
        }
    }
}
