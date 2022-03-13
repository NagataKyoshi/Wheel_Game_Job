using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision : MonoBehaviour
{
    public int wheelIndex;
    public Transform wheelPrefab;
    public ParticleSystem pufSmoke;
   // public ParticleSystem obstacleSmoke;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Wheel")
        {
            if (!WheelList.instance.wheels.Contains(other.gameObject))
            {
                other.GetComponent<SphereCollider>().isTrigger = false;
                other.gameObject.tag = "Untagged";
                other.gameObject.GetComponent<Collision>().enabled = true;
                //other.gameObject.AddComponent<Collision>();
                other.gameObject.AddComponent<Rigidbody>();
                Rigidbody rigid = other.gameObject.GetComponent<Rigidbody>();
                rigid.constraints = RigidbodyConstraints.FreezeRotation;
                other.gameObject.layer = LayerMask.NameToLayer("Wheel");//dont collide with each other
                pufSmoke.Play();

                WheelList.instance.StackWheel(other.gameObject, WheelList.instance.wheels.Count - 1);
            }
        }

        if (other.gameObject.tag == "Bricks")
        {
            other.GetComponent<Rigidbody>().isKinematic = false;
            other.GetComponent<BoxCollider>().isTrigger = false;
            Destroy(other.gameObject, 2); //better for optimization
        }

        if (other.gameObject.tag == "Obstacle")
        {
            //When hit the obstacle we collecting index count
            for (int i = 0; i < WheelList.instance.wheels.Count; i++)
            {

                if (gameObject == WheelList.instance.wheels[i])
                {
                    wheelIndex = i;
                    break;
                }
            }
            //Destroy wheels with Index count
            if (wheelIndex > 0)
            {
                for (int i = wheelIndex; i < WheelList.instance.wheels.Count; i++)
                {
                    //Destroy(WheelList.instance.wheels[wheelIndex]);
                    GameObject wheel = WheelList.instance.wheels[wheelIndex];
                    WheelList.instance.wheels.Remove(wheel);
                    Destroy(wheel);
                    //instantiate prefab
                    //Instantiate(wheelPrefab, transform.position, transform.rotation)
                    Instantiate(wheelPrefab,new Vector3(transform.position.x, transform.position.y +  0.1f, transform.position.z), transform.rotation); //get high instantiate
                    
                }
                //obstacleSmoke.Play();
            }


        }
    }


}
