using UnityEngine;
using System.Collections;

//<summary>
//Ball movement controlls and simple third-person-style camera
//</summary>
public class RollerBall : MonoBehaviour
{

    public GameObject ViewCamera = null;
    public AudioClip JumpSound = null;
    public AudioClip HitSound = null;
    public AudioClip CoinSound = null;

    private Rigidbody mRigidBody = null;
    private AudioSource mAudioSource = null;
    private bool mFloorTouched = false;

    public float force = 100f;
    public float forceOffset = 10.1f;

    void Start()
    {
        mRigidBody = GetComponent<Rigidbody>();
        mAudioSource = GetComponent<AudioSource>();
    }

    void FixedUpdate()
    {
        /*
        if (mRigidBody != null)
        {
            if(Input.GetAxis("Horizontal") != 0)
            {
                mRigidBody.AddForce(Vector3.right * Input.GetAxis("Horizontal") * 10);
            }

            if (Input.GetAxis("Vertical") != 0)
            {
                mRigidBody.AddForce(Vector3.right * Input.GetAxis("Vertical") * 10);
            }
        }

            */
        if (mRigidBody != null)
        {
            if (Input.GetButton("Horizontal"))
            {
                mRigidBody.AddForce(Vector3.right * Input.GetAxis("Horizontal") * 10);
            }
            if (Input.GetButton("Vertical"))
            {
                mRigidBody.AddForce(Vector3.back * Input.GetAxis("Vertical") * -10);
            }
            if (Input.GetButtonDown("Jump"))
            {
                if (mAudioSource != null && JumpSound != null)
                {
                    mAudioSource.PlayOneShot(JumpSound);
                }
                mRigidBody.AddForce(Vector3.up * 200);
            }
        }

        if (ViewCamera != null)
        {
            Vector3 direction = (Vector3.up * 2 + Vector3.back) * 2;
            RaycastHit hit;
            Debug.DrawLine(transform.position, transform.position + direction, Color.red);
            if (Physics.Linecast(transform.position, transform.position + direction, out hit))
            {
                ViewCamera.transform.position = hit.point;
            }
            else
            {
                ViewCamera.transform.position = transform.position + direction;
            }
            ViewCamera.transform.LookAt(transform.position);
        }
    }

    void OnCollisionEnter(Collision coll)
    {

        if (coll.gameObject.tag.Equals("Floor"))
        {
            mFloorTouched = true;
            if (mAudioSource != null && HitSound != null && coll.relativeVelocity.y > .5f)
            {
                mAudioSource.PlayOneShot(HitSound, coll.relativeVelocity.magnitude);
            }
        }
        else if (coll.gameObject.tag.Equals("Wall"))
        {
            MeshDeformer deformer = coll.collider.GetComponent<MeshDeformer>();
            if (deformer)
            {
                print("deform");
                Vector3 point = coll.GetContact(0).point;
                point += coll.GetContact(0).normal * forceOffset;
                deformer.AddDeformingForce(point, force);
            }
        }
        else
        {
            if (mAudioSource != null && HitSound != null && coll.relativeVelocity.magnitude > 2f)
            {
                mAudioSource.PlayOneShot(HitSound, coll.relativeVelocity.magnitude);
            }
        }
    }

    void OnCollisionExit(Collision coll)
    {
        if (coll.gameObject.tag.Equals("Floor"))
        {
            mFloorTouched = false;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Coin"))
        {
            if (mAudioSource != null && CoinSound != null)
            {
                mAudioSource.PlayOneShot(CoinSound);
            }
            Destroy(other.gameObject);
        }
    }
}
