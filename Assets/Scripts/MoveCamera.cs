using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    [SerializeField] private float speed;
    void Update()
    {
        float move = speed * Time.deltaTime;
        transform.position += new Vector3(move, 0, 0);

        
    }
}
