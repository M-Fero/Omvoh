//using Leap.in
using UnityEngine;

public class Marble : MonoBehaviour
{
    public MarbleType marbleType { get; private set; }
    private Material material;
    private Rigidbody rb;
    private void Start()
    {
        material = GetComponent<MeshRenderer>().material;
        rb = GetComponent<Rigidbody>();

        marbleType = (MarbleType)Random.Range(0, 2);

        SetupMarble();
    }
    private void Update()
    {
        if(rb.velocity.magnitude > 20f)
        {
            Destroy(gameObject);
        }
    }
    private void SetupMarble()
    {
        if (marbleType == MarbleType.Red)
        {
            material.color = Color.red;
        }
        else if(marbleType == MarbleType.Blue)
        {
            material.color = Color.blue;
        }
        //GetComponent<InteractionBehaviour>().manager = FindObjectOfType<InteractionManager>();
    }
    public enum MarbleType
    {
        Red,
        Blue
    }    
}