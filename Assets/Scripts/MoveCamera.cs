using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private bool autoMove;
    void Update()
    {
        float move = autoMove ? speed * Time.deltaTime : Input.GetAxis("Horizontal")*speed * Time.deltaTime;
        transform.position += new Vector3(move, 0, 0);
        
    }
}
