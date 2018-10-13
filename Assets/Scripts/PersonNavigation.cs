using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonNavigation : MonoBehaviour
{
    [SerializeField]
    private float speed = 3f;
    [SerializeField]
    private bool useDisperse = false;

    [SerializeField]
    private Transform targetTransform;
    [SerializeField]
    private float finishMargin = .01f;

    [SerializeField]
    private Transform navigationCastTransform;
    [SerializeField]
    private Transform heightCastTransform;

    [SerializeField]
    private Transform graphics;

    [SerializeField]
    private LayerMask navigationMask;

    private int navigateAdjustCount = 0;

    private void Update()
    {
        float distance = Vector3.Distance(GetXZVector(transform.position), GetXZVector(targetTransform.position));

        if (distance > finishMargin)
            Navigate();
        else
            if(navigateAdjustCount < (useDisperse ? 1 : 0))
                CreateNearbyTarget();
    }

    private void SetTarget(Transform newTarget)
    {
        navigateAdjustCount = 0;
        targetTransform = newTarget;
    }

    private void CreateNearbyTarget()
    {
        navigateAdjustCount++;
        GameObject temp = Instantiate(targetTransform.gameObject) as GameObject;
        temp.transform.position = targetTransform.position;
        float rand = .08f;
        temp.transform.position += GetXZVector(new Vector3(Random.Range(-rand, rand), Random.Range(-rand, rand), Random.Range(-rand, rand)));
        targetTransform = temp.transform;
    }

    private void TurnGraphicsToTarget(Vector3 targetPos)
    {
        graphics.transform.LookAt(targetPos);
        graphics.transform.rotation = Quaternion.Euler(new Vector3(0, graphics.transform.eulerAngles.y + 180f, 0));
    }

    private void Navigate()
    {
        TurnGraphicsToTarget(targetTransform.position);

        RaycastHit hit;

        if (Physics.Raycast(navigationCastTransform.position, Vector3.down, out hit, 1000000f, navigationMask))
        {
            switch (hit.transform.tag)
            {
                case "terrain":
                    Move();
                    break;
            }
        }
    }

    private void Move()
    {
        Debug.Log("Moving");
        RaycastHit hit;
        if (Physics.Raycast(heightCastTransform.position, Vector3.down, out hit, 100000f, navigationMask))
        {
            Vector3 lerpPos = Vector3.Lerp(GetXZVector(transform.position), GetXZVector(navigationCastTransform.position), speed * Time.deltaTime);
            //Debug.Log("heightcast point = " + hit.point);
            transform.position = new Vector3(lerpPos.x, hit.point.y, lerpPos.z);
        }

    }

    private Vector3 GetXZVector(Vector3 vector)
    {
        return new Vector3(vector.x, 0, vector.z);
    }
}
