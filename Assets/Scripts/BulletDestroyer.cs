using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDestroyer : MonoBehaviour
{
    private GameObject _gameManager;

    [SerializeField]
    private GameObject explosionParticles;
    private bool isExploded = false;

    public GameObject _explosionParent;
    // Start is called before the first frame update
    void Start()
    {
        _gameManager = GameObject.FindWithTag("GameManager");
        explosionParticles = _gameManager.GetComponent<UIManagerScript>().explosionPrefab;
        _explosionParent = _gameManager.GetComponent<UIManagerScript>().explosionParent;

        if (_gameManager.GetComponent<UIManagerScript>().specialPowers[2] && !isExploded)
        {
            Invoke("Explode", 1.0f);
            isExploded = true;
        }
        else
        {
            Invoke("DestroyMe", 3.0f);
        }
    }

    void DestroyMe()
    {
        Destroy(this.gameObject);
    }

    void Explode()
    {
        Instantiate(explosionParticles, transform.position, transform.rotation, _explosionParent.transform); 
        
        Destroy(this.gameObject);
    }
}
