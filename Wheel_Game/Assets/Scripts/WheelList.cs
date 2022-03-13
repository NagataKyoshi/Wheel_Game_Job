using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class WheelList : MonoBehaviour
{
    public static WheelList instance;
    public List<GameObject> wheels = new List<GameObject>();
    public float movementDelay;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            MoveListElements();
        }
        else
        {
            MoveOrigin();
        }
    }

    public void StackWheel(GameObject other, int index)
    {
        other.transform.parent = transform;
        Vector3 newPos = wheels[index].transform.localPosition;
        newPos.z += 0.3f;
        other.transform.localPosition = newPos;
        wheels.Add(other);
        StartCoroutine(MakeObjectsBigger());
    }

    //When player get wheel last index to first index our wheels get bigger and smaller
    //When player hit obstacles that gives an scale error because of tires deleted.
    public IEnumerator MakeObjectsBigger()
    {
        for (int i = wheels.Count - 1; i > 0; i--)
        {
            int index = i;
            Vector3 scale = new Vector3(10, 10, 10);
            scale *= 1.5f;
            wheels[index].transform.DOScale(scale, 0.1f).OnComplete(() =>
            wheels[index].transform.DOScale(new Vector3(10, 10, 10), 0.1f)
            );
            yield return new WaitForSeconds(0.05f);
        }
    }

    private void MoveListElements()
    {
        for (int i = 1; i < wheels.Count; i++)
        {
            Vector3 pos = wheels[i].transform.localPosition;
            pos.x = Mathf.Lerp(wheels[i].transform.localPosition.x, wheels[i - 1].transform.localPosition.x, Time.deltaTime * movementDelay);
            pos.z = wheels[i - 1].transform.localPosition.z + 0.3f;
                     
            wheels[i].transform.localPosition = pos;
        }
    }

    private void MoveOrigin()
    {
        for (int i = 1; i < wheels.Count; i++)
        {
            Vector3 pos = wheels[i].transform.localPosition;
            pos.x = Mathf.Lerp(wheels[i].transform.localPosition.x, wheels[0].transform.localPosition.x, Time.deltaTime * movementDelay);
            pos.z = wheels[i - 1].transform.localPosition.z + 0.3f;
            wheels[i].transform.localPosition = pos;

        }
    }

}
