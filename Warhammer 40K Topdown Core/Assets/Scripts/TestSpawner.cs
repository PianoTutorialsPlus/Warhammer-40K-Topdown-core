//using System.Collections;
//using UnityEngine;
//using UnityEngine.AI;
//using WH40K.Core;
//using WH40K.PlayerEvents;

////[RequireComponent(typeof(GameTable))]
//public class TestSpawner : MonoBehaviour
//{
//    public GameTable table;
//    private NavigationBaker builder;

//    // Start is called before the first frame update
//    void Start()
//    {
//        var table = Instantiate(Resources.Load("Table", (typeof(GameTable)))) as GameTable;
//        builder = new GameObject().AddComponent<NavigationBaker>();
//        table.Surface = gameObject.AddComponent<NavMeshSurface>();
//        builder.surfaces = new NavMeshSurface[1] { table.Surface };
//        builder.BuildNavMesh();
//        builder.name = "Builder";
//        NecronWarrior dut = Instantiate(Resources.Load("Necron Warrior", (typeof(NecronWarrior)))) as NecronWarrior;

//        UnitMover Target = (UnitMover)dut.UnitMover;
//        float TargetMoveDistance = Target.Range;
//        Vector3 position = new Vector3(6, 0, 0);

//        StartCoroutine(test());

//        Target.SetDestination(position);
//    }

//    private IEnumerator test()
//    {
//        yield return new WaitForSeconds(1);
//    }
//}
