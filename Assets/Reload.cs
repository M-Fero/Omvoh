using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Reload : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("ReloadScene", 5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ReloadScene()
    {
        SceneManager.LoadScene(0);
    }
}
