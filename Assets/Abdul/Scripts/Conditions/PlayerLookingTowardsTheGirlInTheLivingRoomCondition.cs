using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLookingTowardsTheGirlInTheLivingRoomCondition : Condition
{
    private string _guideCharacterTagName = "GuideCharacter";
    private GameObject _guideCharacter;

    [Range(0, 90f)]
    [SerializeField] private float _visionAngle = 30f;
    [SerializeField] private int _rayIntensity;

    private void Start()
    {
        _guideCharacter = GameObject.FindGameObjectWithTag(_guideCharacterTagName);
    }

    public override bool Check()
    {
        bool inVision = false;

        // Get the normalized forward direction of the guide character
        Vector3 gForwardN = _guideCharacter.transform.forward.normalized;
        // Get the normalized forward direction of the player character
        Vector3 pForwardN = transform.forward.normalized;

        // Calculate the angle between the two directions
        if(Vector3.Angle(gForwardN, pForwardN) < _visionAngle)
        {
            print("Player Looking toward the character");
            // Check if the player can see the girl character, or if there is anything between them
            if(_DetectTarget())
            {
                print("Player can see the guide character");
                inVision = true;
            }
        }

        return inVision;
    }

    #region Copied From Another Project
    Ray[] visionRays;
    List<Ray> rays = new List<Ray>();

    protected bool _DetectTarget()
    {
        bool targetDetected = false;
        rays.Clear();

        if (visionRays == null || visionRays.Length != _rayIntensity)
        {
            _rayIntensity = (_rayIntensity % 2) != 1 ? _rayIntensity + 1 : _rayIntensity; // Make sure the number of ray is odd.
            visionRays = new Ray[_rayIntensity];
        }


        float angle = -_visionAngle;
        float angleShift = (_visionAngle * 2) / visionRays.Length;
        RaycastHit hit;

        Vector3 raysOrigin = transform.position + transform.up + transform.forward / 2f;

        for (int i = 0; i < visionRays.Length; i++)
        {

            visionRays[i] = new Ray(raysOrigin, _RotateVectorBy(transform.forward, angle));


            // Check if that ray is hitting the player
            if (Physics.Raycast(visionRays[i], out hit))
            {
                if (hit.collider.CompareTag(_guideCharacterTagName))
                {
                    rays.Add(visionRays[i]); // For debugging purposes
                    targetDetected = true;
                    break;
                }
                else
                    print(hit.collider.gameObject.name);
            }

            angle += angleShift;
        }


        return targetDetected;
    }
    private Vector3 _RotateVectorBy(Vector3 v, float angle)
    {
        return Quaternion.AngleAxis(angle, Vector3.up) * v;
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {


        //if (visionRays == null) return;
        _DetectTarget();
        Gizmos.color = Color.red;
        for (int i = 0; i < visionRays.Length; i++)
        {
            //Gizmos.DrawLine(transform.forward, transform.forward + (visionRays[i].direction * 10));
            Gizmos.DrawLine(visionRays[i].origin, visionRays[i].origin + visionRays[i].direction * 100);
        }

        Gizmos.color = Color.green;
        for (int i = 0; i < rays.Count; i++)
        {
            //Gizmos.DrawRay(rays[i]);
            Gizmos.DrawLine(rays[i].origin, rays[i].origin + rays[i].direction * 100);
        }

    }
#endif

    #endregion
}
