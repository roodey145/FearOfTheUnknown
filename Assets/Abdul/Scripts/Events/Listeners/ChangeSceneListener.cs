using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneListener : Listener
{
    [SerializeField] private int _sceneBuildIndex = -1;

    protected override void _Action()
    {
        if(_sceneBuildIndex != -1)
        {
            SceneManager.LoadScene(_sceneBuildIndex);
        }
    }
}
