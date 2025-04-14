using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;

public class Fire : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float bulletSpeed = 20f;
    [SerializeField] private AudioClip gunshotSound;

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void FireBullet()
    {
        GameObject spawnBullet = Instantiate(bullet, spawnPoint.position, spawnPoint.rotation);
        spawnBullet.GetComponent<Rigidbody>().linearVelocity = spawnPoint.forward * bulletSpeed;
        Destroy(spawnBullet, 5f);

        if (gunshotSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(gunshotSound);
        }
    }
}
