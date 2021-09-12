using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunFireController : FireController
{
    public GameObject[] shotgunBulletArray;
    public int shotgunBulletCount = 4;

    public float shotgunFireProjectile;

    public float[] sgBulletAnglesArray;
    // Start is called before the first frame update
    void Start()
    {
        currentBulletCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ShotgunFire();
        }
    }

    /*public override void InitializeBarrel()
    {
        base.InitializeBarrel();
        
        GameObject bulletProp = Instantiate(bulletPrefab, bulletPos.position, Quaternion.identity, this.transform);
        bulletProp.name = "Barrel";
        if (!bulletProp.activeInHierarchy)
        {
            bulletProp.SetActive(true);
        }
        bulletProp.transform.localScale = new Vector3(2.0f, 2.0f, 0.2f);
        bulletProp.GetComponent<Rigidbody>().isKinematic = true;
        bulletProp.GetComponent<BoxCollider>().enabled = false;
        bulletProp.transform.localScale *= 0.5f;
    } */

    private void ShotgunFire()
    {
        shotgunBulletArray = new GameObject[shotgunBulletCount];
        float sliceAngle;
        float currentAngle;
        sgBulletAnglesArray = new float[shotgunBulletCount];
        
        if (shotgunBulletCount % 2 == 1)
        {
            sliceAngle = 30.0f / (shotgunBulletCount - 1);
            currentAngle = -15.0f;
            sgBulletAnglesArray[0] = currentAngle;
            for (int i = 1; i < shotgunBulletCount; i++)
            {
                sgBulletAnglesArray[i] = currentAngle + sliceAngle;
                currentAngle = sgBulletAnglesArray[i];
            }

        }
        else if (shotgunBulletCount % 2 == 0)
        {
            sliceAngle = 30.0f / (shotgunBulletCount + 1);
            currentAngle = -15.0f + sliceAngle;
            sgBulletAnglesArray[0] = currentAngle;
            for (int i = 1; i < shotgunBulletCount; i++)
            {
                sgBulletAnglesArray[i] = currentAngle + sliceAngle;
                currentAngle = sgBulletAnglesArray[i];
            }
        }

        for (int i = 0; i < shotgunBulletArray.Length; i++)
        {
            shotgunBulletArray[i] = Instantiate(bulletPrefab, bulletPos.position, Quaternion.identity,
                bulletsParent.transform);
            shotgunBulletArray[i].transform.localScale *= 0.5f;

            Vector3 dir = Quaternion.AngleAxis(sgBulletAnglesArray[i], Vector3.up) * this.transform.forward;
            shotgunBulletArray[i].GetComponent<Rigidbody>().AddRelativeForce(dir * shotgunFireProjectile);
            
            /*shotgunBulletArray[i].GetComponent<Rigidbody>().
                AddRelativeForce(
                    new Vector3(shotgunFireProjectile * Mathf.Sin(Mathf.Deg2Rad * sgBulletAnglesArray[i]), 
                        0.0f, shotgunFireProjectile * Mathf.Cos(Mathf.Deg2Rad * sgBulletAnglesArray[i])));*/

            // shotgunBulletArray[i].GetComponent<Rigidbody>().AddRelativeForce(shotgunFireProjectile);
            shotgunBulletArray[i].AddComponent<BulletDestroyer>();
            currentBulletCount++;
            bulletCountText.text = "BULLETS FIRED: " + currentBulletCount;
            
        }
        
        StopCoroutine("ResetBulletCount");
            
        StartCoroutine("ResetBulletCount");
    }
    
    private Vector3 SetVectorFromAngle (float x, float y, float z) 
    {
        Quaternion rotation = Quaternion.Euler(x, y, z);
        Vector3 forward = Vector3.forward * 0.5f;
        
        return new Vector3();
    }

    IEnumerator ResetBulletCount()
    {
        yield return new WaitForSeconds(2.0f);

        currentBulletCount = 0;
        bulletCountText.text = "NO BULLETS FIRED YET!";
    }
}
