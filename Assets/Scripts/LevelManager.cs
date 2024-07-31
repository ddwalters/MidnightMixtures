using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System.Linq;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public List<bool> levels;


    // Start is called before the first frame update
    void Start()
    {
        levels = new List<bool> { true, false, false, false, false };
        DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public bool getState(int index) => levels[index];

    public void unlockLevel(int index)
    {
        index--;
        levels[index] = true;
    }
}
