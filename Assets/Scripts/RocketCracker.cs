using UnityEngine;

public class RocketCracker : MonoBehaviour
{
  //  [SerializeField] float xVelocity;
  //  [SerializeField] float yVelocity;
  //  [SerializeField] float zVelocity;
    [SerializeField] Vector3 Velocities;

    public Rigidbody[] attachedToRocket;
    private Rigidbody thisobject;
    private void Start()
    {
        thisobject = gameObject.GetComponent<Rigidbody>();

        foreach (Rigidbody ao in attachedToRocket)
        { 
            ao.GetComponent<Rigidbody>();
        }
    }
    private void FixedUpdate()
    {
        thisobject.velocity = Velocities;
        foreach (Rigidbody ao in attachedToRocket)
        {
            ao.velocity = Velocities;
        }

    }
}
