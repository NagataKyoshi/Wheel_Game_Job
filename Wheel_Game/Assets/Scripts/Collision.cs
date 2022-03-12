using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Wheel")
        {
            if (!WheelList.instance.wheels.Contains(other.gameObject))
            {
                other.GetComponent<SphereCollider>().isTrigger = false;
                other.gameObject.tag = "Untagged";
                other.gameObject.AddComponent<Collision>();
                other.gameObject.AddComponent<Rigidbody>();
                Rigidbody rigid = other.gameObject.GetComponent<Rigidbody>();
                rigid.constraints = RigidbodyConstraints.FreezeRotation;


                WheelList.instance.StackWheel(other.gameObject, WheelList.instance.wheels.Count - 1);
            }
        }

        if (other.gameObject.tag == "Bricks")
        {
            other.GetComponent<Rigidbody>().isKinematic = false;
            other.GetComponent<BoxCollider>().isTrigger = false;
        }
    }
}
