using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

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
        bulletPrefab.GetComponent<Renderer>().sharedMaterial.color 
            = Color.grey;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            ShotgunFire();
        }
    }
    
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
            
            shotgunBulletArray[i].AddComponent<BulletDestroyer>();
            currentBulletCount++;
            bulletCountText.text = "BULLETS FIRED: " + currentBulletCount;
            
        }
        
        StopCoroutine("ResetBulletCount");
            
        StartCoroutine("ResetBulletCount");
    }

    IEnumerator ResetBulletCount()
    {
        yield return new WaitForSeconds(2.0f);

        currentBulletCount = 0;
        bulletCountText.text = "NO BULLETS FIRED YET!";
    }
}
