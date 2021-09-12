using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FireController : MonoBehaviour
{
    [SerializeField]
    private GameObject bulletPrefab;
    [SerializeField] 
    private Transform bulletPos;
    [SerializeField] 
    private GameObject bulletsParent;
    [SerializeField] 
    private float fireProjectile;

    public int currentBulletCount;

    public Text bulletCountText;
    // Start is called before the first frame update
    void Start()
    {
        currentBulletCount = 0;
        
        GameObject bulletProp = Instantiate(bulletPrefab, bulletPos.position, Quaternion.identity, this.transform);
        bulletProp.name = "Barrel";
        bulletProp.transform.localScale = new Vector3(2.0f, 2.0f, 0.2f);
        bulletProp.GetComponent<Rigidbody>().isKinematic = true;
        bulletProp.GetComponent<BoxCollider>().enabled = false;
        bulletProp.transform.localScale *= 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject newBullet = Instantiate(bulletPrefab, bulletPos.position, Quaternion.identity, bulletsParent.transform);
            //bulletPrefab.transform.position += Vector3.forward * fireSpeed * Time.deltaTime;
            //newBullet.GetComponent<Rigidbody>().AddForce(this.transform.forward * fireProjectile);
            newBullet.GetComponent<Rigidbody>().AddRelativeForce(this.transform.forward * fireProjectile);// fireProjectile);
            newBullet.AddComponent<BulletDestroyer>();
            currentBulletCount++;
            bulletCountText.text = "BULLETS FIRED: " + currentBulletCount;
            
            StopCoroutine("ResetBulletCount");
            
            StartCoroutine("ResetBulletCount");
        }
        
    }

    IEnumerator ResetBulletCount()
    {
        yield return new WaitForSeconds(2.0f);

        currentBulletCount = 0;
        bulletCountText.text = "NO BULLETS FIRED YET!";
    }
}
