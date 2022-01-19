using UnityEngine;
using WH40K;


public class UserControl : MonoBehaviour
{
    public Camera GameCamera;
    private Unit m_Selected = null;
    private Unit m_EnemySelected = null;
    public GameObject DistanceIndicator;
    public GameManager gameManager;
    float scale = 3.5f;
    float baseSize = 0.5f;
    public float range;

    public ShootingSO _shooting;



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

                DisplayStats();
            }

        }

    }

    public void DisplayStats()
    {
        gameManager.infoPanel[0].text = $"Player Stats: {m_Selected.name}\n";

        foreach (var stat in m_Selected.stats)
        {
            gameManager.infoPanel[0].text += $"{stat.Key.Substring(0, 1)}: {stat.Value} ";
        }
        gameManager.infoPanel[0].text += $"\nWeapon Stats: ";
        //foreach (var stat in m_Selected.weaponStats)
        //{
        //    gameManager.infoPanel[0].text += $"{stat.Key.Substring(0, 1)}: {stat.Value} ";
        //}

        gameManager.infoPanel[0].text += $"{m_Selected._weaponSO.Type}";
    }

    public void HandleEnemySelection()
    {
        var ray = GameCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            var unit = hit.collider.GetComponent<Unit>();

            if (unit != null && gameManager.player[0].tag != unit.tag)
            {
                m_EnemySelected = unit;

                gameManager.infoPanel[1].text = $"Enemy Stats: {m_EnemySelected.name}\n";

                foreach (var stat in m_EnemySelected.stats)
                {
                    gameManager.infoPanel[1].text += $"{stat.Key.Substring(0, 1)}: {stat.Value} ";
                }

            }
        }
    }

    public void HandleShooting()
    {
        int shots;
        int rapidFire;
        int hits;
        int wounds = 0;
        int notSaved = 0;

        gameManager.infoPanel[2].text = "Hits:\n";
        gameManager.infoPanel[3].text = "Wounds:\n";
        gameManager.infoPanel[4].text = "Saves:\n";
        gameManager.infoPanel[5].text = "Damage:\n";

        if (range < m_Selected.weaponRange / 2)
        {
            rapidFire = m_Selected.weaponStats["Type"] * 2;
        }
        else
        {
            rapidFire = m_Selected.weaponStats["Type"];
        }

        shots = m_Selected.stats["Attacks"] * rapidFire;

        hits = HandleToHit(m_Selected.stats["Ballistic Skill"], shots);

        if (hits > 0)
            wounds = HandleToWound(m_Selected.weaponStats["Strength"], m_EnemySelected.stats["Toughness"], hits);

        if (wounds > 0)
            notSaved = HandleSaveRoles(m_EnemySelected.stats["Armour Save"], m_Selected.weaponStats["Armour Pen"], wounds);


        if (notSaved > 0)
            HandleDamage(m_Selected.weaponStats["Damage"], notSaved);

        m_Selected.canShoot = false;
        m_EnemySelected = null;
    }

    public void HandleDamage(int damage, int notSaved) // ABSTRACTION
    {
        m_EnemySelected.stats["Wounds"] -= damage * notSaved;


        if (m_EnemySelected.stats["Wounds"] <= 0)
            m_EnemySelected.Destroy();
    }
    public int HandleSaveRoles(int saves, int modifier, int wounds) // ABSTRACTION
    {
        int notSaved = 0;
        for (int i = 0; i < wounds; i++)
        {

            int saveResult = Random.Range(1, 7);
            gameManager.infoPanel[4].text += $"S{i + 1}: {saveResult} ";
            if (saveResult < (saves - modifier))
                notSaved++;
        }
        gameManager.infoPanel[5].text += $"{notSaved}";
        return notSaved;
    }
    public int HandleToWound(int strength, int toughness, int hits)
    {
        int wounds = 0;
        int toWound = 0;

        for (int i = 0; i < hits; i++)
        {
            toWound = CalculateToWound(strength, toughness);
            int woundResult = Random.Range(1, 7);
            gameManager.infoPanel[3].text += $"W{i + 1}: {woundResult}; ToW: {toWound} ";

            if (woundResult >= toWound)
                wounds++;
        }

        return wounds;
    }

    public int CalculateToWound(int strength, int toughness) // ABSTRACTION
    {
        int toWound = 0;
        if (strength >= 2 * toughness)
        {
            toWound = 2;
        }
        else if (strength > toughness)
        {
            toWound = 3;
        }
        else if (strength == toughness)
        {
            toWound = 4;
        }
        else if (2 * strength < toughness)
        {
            toWound = 6;
        }
        else if (strength < toughness)
        {
            toWound = 5;
        }

        return toWound;
        //float woundResult = strength / toughness;

        //switch (woundResult)
        //{
        //    case >= 2:
        //        toWound = 2;
        //        break;
        //    case >1:
        //        toWound = 3;
        //        break;
        //    case 1:
        //        toWound = 4;
        //        break;
        //    case <= 0.5f:
        //        toWound = 6;
        //        break;
        //    case < 1:
        //        toWound = 5;
        //        break;

        //}


    }

    public int HandleToHit(int toHit, int shots)
    {
        int hits = 0;
        for (int i = 0; i < shots; i++)
        {
            int hitResult = Random.Range(1, 7);
            gameManager.infoPanel[2].text += $"H{i + 1}: {hitResult} ";

            hits = m_Selected._weaponSO.HitModifier(hits);

            if (hitResult >= toHit)
                hits++;
        }
        return hits;

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
        float actionRadiusXZ = (m_Selected.restDistance + baseSize) * scale;
        Vector3 actionArea = new Vector3(actionRadiusXZ, 1, actionRadiusXZ);

        DistanceIndicator.transform.localScale = actionArea;
    }

    public bool InRange()
    {
        range = (m_EnemySelected.GetComponent<CapsuleCollider>().ClosestPoint(m_Selected.GetComponent<CapsuleCollider>().transform.position)
             - m_Selected.GetComponent<CapsuleCollider>().ClosestPoint(m_EnemySelected.GetComponent<CapsuleCollider>().transform.position)).magnitude;

        if (range > m_Selected.weaponRange)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.phase == "Movement Phase")
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
        else if (gameManager.phase == "Shooting Phase")
        {
            if (Input.GetMouseButtonDown(0))
            {
                HandleSelection();
                ConnectDistanceIndicator();
            }
            else if (m_Selected != null && Input.GetMouseButtonDown(1) && m_Selected.canShoot)
            {
                HandleEnemySelection();

                if (m_EnemySelected != null && InRange())
                {
                    HandleShooting();
                }
            }
        }

    }

}
