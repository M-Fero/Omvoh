using UnityEngine;

public class Basket : MonoBehaviour
{
    [SerializeField] private Marble.MarbleType recievingType;

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<Marble>(out Marble marble))    
        {
            if(marble.marbleType == recievingType)
            {
                Debug.Log("Found a match!");
                // increaase score

            } else {

                Debug.Log("Wrong type!");
                // decrease score
            }
        }
    }
}
