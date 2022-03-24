using System.Collections;
using UnityEngine;
using UnityEngine.AI;

//[RequireComponent(typeof(GameTable))]
public class TestSpawner : MonoBehaviour
{
    public GameTable table;
    private NavigationBaker builder;

    // Start is called before the first frame update
    void Start()
    {
        var table = Instantiate(Resources.Load("Table", (typeof(GameTable)))) as GameTable;
        builder = new GameObject().AddComponent<NavigationBaker>();
        table.Surface = gameObject.AddComponent<NavMeshSurface>();
        builder.surfaces = new NavMeshSurface[1] { table.Surface };
        builder.BuildNavMesh();
        builder.name = "Builder";
        NecronWarrior dut = Instantiate(Resources.Load("Necron Warrior", (typeof(NecronWarrior)))) as NecronWarrior;

        UnitMover Target = dut.UnitMover;
        float TargetMoveDistance = Target.MoveDistance;
        Vector3 position = new Vector3(6, 0, 0);

        StartCoroutine(test());

        Target.SetDestination(position);



        //var DUT = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        //DUT.gameObject.AddComponent<NecronWarrior>();
        //var warrior = DUT.gameObject.GetComponent<NecronWarrior>();
        //warrior._unitSO = ScriptableObject.CreateInstance<NecronWarriorSO>();
        //DUT.transform.localPosition = new Vector3(0, 1.5f, 0);
        //DUT.name = "DUT";
        //////navmeshsu
        //var test = new GameObject().AddComponent<NecronWarrior>();
        //test.transform.localPosition = Vector3.zero;
    }

    private IEnumerator test()
    {
        yield return new WaitForSeconds(1);
    }
}
