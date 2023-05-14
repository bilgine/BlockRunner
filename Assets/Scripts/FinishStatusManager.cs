using UnityEngine;
using UnityEngine.Serialization;

public class FinishStatusManager : MonoBehaviour
{
    [SerializeField] private Transform finishObject;
    
    public Vector3 FinishPosition => finishObject.position;
    
    public GameObject FinishObject => finishObject.gameObject;
    
    public void AddFinishPosition(Vector3 position)
    {
        finishObject.position += position;
    }

    public void SetInitialPosition()
    {
        finishObject.position = new Vector3(0f, -0.055f, 15f);
    }

}