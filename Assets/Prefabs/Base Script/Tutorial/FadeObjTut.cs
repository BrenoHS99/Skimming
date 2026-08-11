using System.Threading.Tasks;
using UnityEngine;

public class FadeObjTut : MonoBehaviour
{
    private GameObject plr;
    private Renderer objRender;

    private float distance;

    public float minDist = 10f;
    public float maxDist = 50f;

    private Vector3 startPos;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        plr = GameObject.FindWithTag("Player");
        objRender = plr.GetComponent<Renderer>();
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        distance = (startPos - plr.transform.position).magnitude;

        float yChange = Mathf.Clamp(distance, minDist, maxDist);
		yChange -= minDist;
		yChange /= maxDist;

        yChange *= 100;

        transform.position = new Vector3(
            transform.position.x,
            yChange += startPos.y,
            transform.position.z
            );
    }
}
