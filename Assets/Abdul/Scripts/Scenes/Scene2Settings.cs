using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene2Settings : MonoBehaviour
{
    [SerializeField] private Color _environmentColor;
    // Start is called before the first frame update
    void Awake()
    {
        RenderSettings.ambientLight = _environmentColor;
    }

    private void Start()
    {
        EventsController.RegisterEvent("SceneStart");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
