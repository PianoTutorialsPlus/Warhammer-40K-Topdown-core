using UnityEngine;
using UnityEngine.AI;

public class NavigationBaker : MonoBehaviour
{

    public NavMeshSurface[] surfaces;
    public Transform[] objectsToRotate;

    public void BuildNavMesh()
    {
        for (int i = 0; i < surfaces.Length; i++)
        {
            surfaces[i].BuildNavMesh();
        }
    }

    // Use this for initialization
    //void Update()
    //{

    //    //for (int j = 0; j < objectsToRotate.Length; j++)
    //    //{
    //    //    objectsToRotate[j].localRotation = Quaternion.Euler(new Vector3(0, 50 * Time.deltaTime, 0) + objectsToRotate[j].localRotation.eulerAngles);
    //    //}

    //    for (int i = 0; i < surfaces.Length; i++)
    //    {
    //        surfaces[i].BuildNavMesh();
    //    }
    //}

}
