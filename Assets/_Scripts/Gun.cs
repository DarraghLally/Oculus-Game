using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public float speed = 100.0f;
    public Bullet bullet;
    public Transform barrel;
    //public AudioSource audioSource;
    //public AudioClip audioClip;

    public void fire()
    {
        Debug.Log("In fire method...... XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX");
        
        Bullet spawnedBullet = Instantiate(bullet, barrel.position, barrel.rotation); // Create a bullet 
        //spawnedBullet.GetComponent<Rigidbody>().velocity = speed * barrel.forward; // Set inital vel to direction of barrel

        Rigidbody bulletRB = spawnedBullet.GetComponent<Rigidbody>();
        bulletRB.AddForce(barrel.forward * speed);
        Debug.Log("In fire method...... " + spawnedBullet.GetComponent<Rigidbody>().velocity);
        
        //audioSource.PlayOneShot(audioClip);
        //Destroy(bullet, 2);
    }
}
