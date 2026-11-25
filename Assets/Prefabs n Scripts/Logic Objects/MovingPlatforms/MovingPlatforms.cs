using System.Collections;
using UnityEngine;

public class MovingPlatforms : MonoBehaviour
{
    private Vector3 startPosition;
    private Transform finalPosObj;
    public float speed;
    public bool moving = false;

    private void Start()
    {
        finalPosObj = transform.Find("FinalPos");
        startPosition = transform.position;
    }

    public IEnumerator StartMovement()
    {
        moving = true;
        Vector3 finalPos = finalPosObj.position;
        while ((transform.position - finalPos).magnitude > 0.01f)
        {
            if (moving == false)
            {
                break;
            }
            transform.position = Vector3.Lerp(transform.position, finalPos, speed * Time.deltaTime);
            yield return null;
        }
    }

    public IEnumerator ResetPosition()
    {
        yield return new WaitForSeconds(0.1f);
        moving = false;
        transform.position = startPosition;
    }
}
