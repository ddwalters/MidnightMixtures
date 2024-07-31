using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class lockedLevel : MonoBehaviour
{

    public LevelManager levelManager;
    public changeScene sceneSelect;
    private Image image;
    public int index;
    public bool unlocked = false;
    public Sprite unlockedImage;
    public Sprite lockedImage;

    void Start()
    {
        image = GetComponent<Image>();
        levelManager = FindAnyObjectByType<LevelManager>();
        sceneSelect = FindAnyObjectByType<changeScene>();
    }

    void Update()
    {
        int i = index - 1;
        unlocked = levelManager.getState(i);
        if (unlocked == true)
        {
            image.sprite = unlockedImage;
        }
        if (unlocked == false)
        {
            image.sprite = lockedImage;
        }
    }

    public void changeScene()
    {
        if (unlocked == true)
        {
            sceneSelect.SceneSelect(index + 1);
        }
    }
}
