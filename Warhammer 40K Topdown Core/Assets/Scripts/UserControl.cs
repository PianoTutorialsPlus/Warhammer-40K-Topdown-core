using UnityEngine;

public class UserControl : MonoBehaviour
{
    public Camera GameCamera;
    private Unit m_Selected = null;
    public GameObject DistanceIndicator;
    public GameManager gameManager;
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

            if (unit != null && gameManager.player[0].tag == unit.tag)
            {
                m_Selected = unit;
                Debug.Log(m_Selected.stats["Movement"]);
            }
            
        }

    }

    public void HandleAction()
    {
        var ray = GameCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            m_Selected.AddMovedDistance();
            m_Selected.GoTo(hit.point);
            m_Selected.GetDistance(hit.point);

        }
    }

    public void ConnectDistanceIndicator()
    {
        if (m_Selected != null && gameManager.player[0].tag == m_Selected.tag)
        {
            DistanceIndicator.transform.SetParent(m_Selected.gameObject.transform);
            DistanceIndicator.transform.position = m_Selected.transform.position;
            SetActionRadius();
        }
    }

    public void SetActionRadius()
    {
        float actionRadiusXZ = m_Selected.restDistance * scale;
        Vector3 actionArea = new Vector3(actionRadiusXZ, 1, actionRadiusXZ);

        DistanceIndicator.transform.localScale = actionArea;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            HandleSelection();
            ConnectDistanceIndicator();
        }
        else if (m_Selected != null && Input.GetMouseButtonDown(1))
        {
            HandleAction();
        }
        if (m_Selected != null)
        {
            SetActionRadius();
        }
    }

}
