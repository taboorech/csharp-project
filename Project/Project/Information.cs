namespace Project;

public class Information
{
    public List<int> tables { get; set; }
    public List<Dictionary<string, List<string>>> clients { get; set; }
    public List<string> waiters { get; set; }
    public Dictionary<string, float> menu { get; set; }
    
    // public int millis;
    // public string stamp;
    // public DateTime datetime;
    // public string light;
    // public float temp;
    // public float vcc;

    // public Information(int[] tables, Dictionary<string, string[]>[] clients, string[] waiters,
    //     Dictionary<string, float> menu)
    // {
    //     _tables = tables;
    //     _clients = clients;
    //     _waiters = waiters;
    //     _menu = menu;
    // }
}