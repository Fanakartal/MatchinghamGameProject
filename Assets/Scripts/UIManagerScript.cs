using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManagerScript : MonoBehaviour
{
    public GameObject[] weapons;
    public GameObject FPSCamera;
    
    private int i = 0;
    public Slider shotgunSlider;
    public Text shotgunSliderText;

    [SerializeField] private bool[] specialPowers;

    public Transform originalScale;

    private void Start()
    {

        Application.targetFrameRate = 30;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            PowerUpBiggerBullet();
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            PowerUpRedBullet();
        }

        if (Input.GetKeyDown(KeyCode.L) && weapons[1].GetComponent<ShotgunFireController>().shotgunBulletCount < 10)
        {
            weapons[1].GetComponent<ShotgunFireController>().shotgunBulletCount++;
            shotgunSlider.GetComponent<Slider>().value =
                weapons[1].GetComponent<ShotgunFireController>().shotgunBulletCount;
            shotgunSliderText.text = "SHOTGUN \nBULLET COUNT: " +
                                     weapons[1].GetComponent<ShotgunFireController>().shotgunBulletCount;
        }

        if (Input.GetKeyDown(KeyCode.K) && weapons[1].GetComponent<ShotgunFireController>().shotgunBulletCount > 3)
        {
            weapons[1].GetComponent<ShotgunFireController>().shotgunBulletCount--;
            shotgunSlider.GetComponent<Slider>().value =
                weapons[1].GetComponent<ShotgunFireController>().shotgunBulletCount;
            shotgunSliderText.text = "SHOTGUN \nBULLET COUNT: " +
                                     weapons[1].GetComponent<ShotgunFireController>().shotgunBulletCount;
        }
        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public void ChangeWeapon()
    {
        if (weapons[i].activeInHierarchy)
        {
            weapons[i].SetActive(false);
            i = (i + 1) % weapons.Length;
            weapons[i].SetActive(true);
            FPSCamera.GetComponent<LookWithMouse>().weapon = weapons[i].transform;
        }
    }

    public void ChangeShotgunBulletCount()
    {
        weapons[1].GetComponent<ShotgunFireController>().shotgunBulletCount = 
            (int) shotgunSlider.GetComponent<Slider>().value;
        shotgunSliderText.text = "SHOTGUN \nBULLET COUNT: "
                                 + weapons[1].GetComponent<ShotgunFireController>().shotgunBulletCount;
    }

    public void PowerUpBiggerBullet()
    {
        if (!specialPowers[0]) specialPowers[0] = true;
        else specialPowers[0] = false;

        for (int j = 0; j < weapons.Length; j++)
        {
            if (specialPowers[0])
            {
                weapons[j].GetComponent<FireController>().bulletPrefab.transform.localScale *= 2f;
            }
            else
            {
                weapons[j].GetComponent<FireController>().bulletPrefab.transform.localScale /= 2f;
            }
        }
    }

    public void PowerUpRedBullet()
    {
        if (!specialPowers[1]) specialPowers[1] = true;
        else specialPowers[1] = false;
        
        for (int j = 0; j < weapons.Length; j++)
        {
            if (specialPowers[1])
            {
                weapons[j].GetComponent<FireController>().bulletPrefab.GetComponent<Renderer>().sharedMaterial.color 
                    = Color.red;
            }
            else
            {
                if (j == 0)
                    weapons[j].GetComponent<FireController>().bulletPrefab.GetComponent<Renderer>().sharedMaterial.color 
                        = Color.black;
                else
                {
                    weapons[j].GetComponent<FireController>().bulletPrefab.GetComponent<Renderer>().sharedMaterial.color 
                        = Color.grey;
                }
            }
        }
    }

    void OnApplicationQuit()
    {
        for (int j = 0; j < weapons.Length; j++)
        {
            weapons[j].GetComponent<FireController>().bulletPrefab.transform.localScale
                = originalScale.localScale;
        }
    }
}
