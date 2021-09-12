using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManagerScript : MonoBehaviour
{
    public GameObject[] weapons;
    private int i = 0;
    public Slider shotgunSlider;
    public Text shotgunSliderText;
    
    public void ChangeWeapon()
    {
        if (weapons[i].activeInHierarchy)
        {
            weapons[i].SetActive(false);
            i = (i + 1) % weapons.Length;
            weapons[i].SetActive(true);
        }
    }

    public void ChangeShotgunBulletCount()
    {
        weapons[1].GetComponent<ShotgunFireController>().shotgunBulletCount = 
            (int) shotgunSlider.GetComponent<Slider>().value;
        shotgunSliderText.text = "SHOTGUN \nBULLET COUNT: "
                                 + weapons[1].GetComponent<ShotgunFireController>().shotgunBulletCount;
    }
}
