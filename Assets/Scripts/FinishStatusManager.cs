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

}