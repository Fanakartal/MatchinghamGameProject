using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class FireController : MonoBehaviour
{
    [SerializeField]
    public GameObject bulletPrefab;
    [SerializeField] 
    public Transform bulletPos;
    [SerializeField] 
    public GameObject bulletsParent;
    [SerializeField] 
    private float fireProjectile;

    public int currentBulletCount;

    public Text bulletCountText;
    // Start is called before the first frame update
    void Start()
    {
        currentBulletCount = 0;
        bulletPrefab.GetComponent<Renderer>().sharedMaterial.color 
            = Color.black;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            GameObject newBullet = Instantiate(bulletPrefab, bulletPos.position, Quaternion.identity, bulletsParent.transform);
            newBullet.GetComponent<Rigidbody>().AddRelativeForce(this.transform.forward * fireProjectile);
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
