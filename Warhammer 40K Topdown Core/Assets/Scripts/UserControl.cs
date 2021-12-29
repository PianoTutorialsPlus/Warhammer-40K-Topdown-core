using UnityEngine;

public class UserControl : MonoBehaviour
{
    public Camera GameCamera;
    private Unit m_Selected = null;
    public GameObject DistanceIndicator;
    float scale = 3.5f;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void HandleSelection()
    {
        var ray = GameCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            var unit = hit.collider.GetComponent<Unit>();
            m_Selected = unit;
            Debug.Log(m_Selected.name);

            DistanceIndicator.transform.SetParent(m_Selected.gameObject.transform);
            DistanceIndicator.transform.position = m_Selected.transform.position;
            DistanceIndicator.transform.localScale = new Vector3(m_Selected.restDistance * scale, 1, m_Selected.restDistance * scale);


        }

    }

    public void HandleAction()
    {
        var ray = GameCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            m_Selected.GoTo(hit.point);
            m_Selected.GetDistance(hit.point);

        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            HandleSelection();
        }
        else if (m_Selected != null && Input.GetMouseButtonDown(1))
        {
            HandleAction();
        }
        if (m_Selected != null)
        {
            DistanceIndicator.transform.localScale = new Vector3(m_Selected.restDistance * scale, 1, m_Selected.restDistance * scale);

        }
    }
}
