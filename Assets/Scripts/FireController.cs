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
    private Vector3 fireProjectile;

    public int currentBulletCount;

    public Text bulletCountText;
    // Start is called before the first frame update
    void Start()
    {
        currentBulletCount = 0;
        
        GameObject bulletProp = Instantiate(bulletPrefab, bulletPos.position, Quaternion.identity, this.transform.parent);
        bulletProp.GetComponent<Rigidbody>().isKinematic = true;
        bulletProp.GetComponent<BoxCollider>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject newBullet = Instantiate(bulletPrefab, bulletPos.position, Quaternion.identity, this.transform.parent);
            //bulletPrefab.transform.position += Vector3.forward * fireSpeed * Time.deltaTime;
            newBullet.GetComponent<Rigidbody>().AddRelativeForce(fireProjectile);
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
