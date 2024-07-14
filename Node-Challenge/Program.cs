class Family
{
    public static List<Family> NormalFamilies = new();    
    public string Parent { get; set; }
    public string Child { get; set; }
    public Family() { }
    public Family(string parent, string child) { Parent = parent; Child = child; }
}

class Program
{
    public static void Main()
    {
        var familyOne = new Family("Node-1", "Node-1-1");
        var familyTwo = new Family("Node-1-1", "Node-1-1-1");
        var familyThree = new Family("Node-1-1", "Node-1-1-2");
        var familyFour = new Family("Node-1-1", "Node-1-1-3");
        var familyFive = new Family("Node-1-1-2", "Node-1-1-2-1");
        var familySix = new Family("Node-1-1-2-1", "Node-1-1-2-1-1");
        var normalFamilies = new Family[] { familyOne, familyTwo, familyThree, familyFour, familyFive, familySix };

        var deNormalizedFamilies = DeNormalizeData(normalFamilies);

        Console.WriteLine("Parent   Child");
        foreach (var family in deNormalizedFamilies)
        {
            Console.WriteLine($"{family.Parent}  {family.Child}");
        }

        //var children = FindAllChildren("Node-1", normalFamilies).ToList();
        //children.ForEach(x => Console.WriteLine(x));
    }

    public static string[] FindImmediateChildren(string parent, IEnumerable<Family> families)
    {
        var children = new List<string>();        
        foreach(var family in families)
        {
            if (family.Parent == parent)
                children.Add(family.Child);
        }        
        return children.ToArray();
    }

    public static string[] FindAllChildren(string parent, IEnumerable<Family> families)
    {        
        var children = FindImmediateChildren(parent, families).ToList();        
        if (children.Count() == 0)
        {
            return children.ToArray();
        }  
        else
        {
            foreach(var child in children.ToArray())
            {
                children.AddRange(FindAllChildren(child, families));
            }
        }        
        return children.ToArray();
    }

    public static IEnumerable<Family> DeNormalizeData(IEnumerable<Family> families)
    {
        List<Family> result = new List<Family>();
        foreach(Family family in families)
        {            
            if (result.All(x => x.Parent != family.Parent))
            {
                var allChilds = FindAllChildren(family.Parent, families);
                foreach (var child in allChilds)
                {
                    result.Add(new Family(family.Parent, child));
                }
            }            
        }
        return result;
    }
}
