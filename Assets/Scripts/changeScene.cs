using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class changeScene : MonoBehaviour
{
    public GameObject bigBlackBar;

    void Start()
    {
        
    }

    public void SceneSelect(int index)
    {
        if (bigBlackBar != null)
        {
            bigBlackBar.SetActive(true);
        }
        SceneManager.LoadSceneAsync(index);
    }
}
