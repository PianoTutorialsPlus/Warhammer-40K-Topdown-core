using System.Collections.Generic;

public class NecronWarrior : Necrons
{
    
    
    //public new Dictionary<string, int> stats = new Dictionary<string, int>
    //{
    //    {"Movement", 5},
    //    {"Weapon Skill", 3},
    //    {"Ballistic Skill", 3},
    //    {"Strength", 4},
    //};
    
    public override void SetStats()
    {
        base.stats["Movement"] = 5;
        base.stats["Weapon Skill"] = 3;
        base.stats["Ballistic Skill"] = 3;
        base.stats["Strength"] = 4;
        base.stats["Toughness"] = 4;
        base.stats["Wounds"] = 1;
        base.stats["Attacks"] = 1;
        base.stats["Leadership"] = 10;
        base.stats["Armour Save"] = 4;
    }

    public override void SetWeaponStats()
    {
        base.weaponStats["Range"] = 12;
        base.weaponStats["Type"] = 1;
        base.weaponStats["Strength"] = 8;
        base.weaponStats["Armour Pen"] = -1;
        base.weaponStats["Damage"] = 1;
    }

    //new float moveDistance = 6;

    //// Start is called before the first frame update
    //void Start()
    //{

    //}

    //// Update is called once per frame
    //void Update()
    //{

    //}

}
