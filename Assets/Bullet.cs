using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] int _lifetime = 3, _speed = 10;
    private void OnEnable()
    {
        StartCoroutine(AutoDeactive());
    }
    void Update()
    {
        transform.Translate(new Vector3(0,0,1)* _speed * Time.deltaTime);
    }
    IEnumerator AutoDeactive()
    {
        yield return new WaitForSeconds(_lifetime);
        this.gameObject.SetActive(false);
    }
}
