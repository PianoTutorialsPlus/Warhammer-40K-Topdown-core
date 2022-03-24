using System.Collections;
using UnityEngine;

namespace WH40K
{
    public class UnitManager : MonoBehaviour
    {
        public GameObject unit;
        // Start is called before the first frame update
        private void Awake()
        {

        }

        //// Update is called once per frame
        //void Update()
        //{

        //}
        public void Load()
        {
            StartCoroutine(Sanctum());
        }

        public IEnumerator Sanctum()
        {
            yield return new WaitForEndOfFrame();
            //foreach(Transform child in unit.transform)
            //    child.GetComponent<Unit>().onPointerEnter += UnitTapped;
            //unit.onPointerEnter += UnitTapped;
            //UnitTapped();

        }

        private void UnitTapped()
        {
            Debug.Log("EventData");
        }


    }
}
