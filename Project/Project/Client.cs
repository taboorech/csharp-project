namespace Project;

public class Client : IClient
{
    public string _name;
    private Check _check;
    public Table _table { get; private set; }

    public Client(string name)
    {
        NameValidation = name;
    }

    public void ConnectCheck(Check check)
    {
        _check = check;
    }

    public void AddTable(Table table)
    {
        _table = table;
    }

    public Check GetCheck()
    {
        return _check;
    }

    public string NameValidation
    {
        get
        {
            return _name;
        }

        set
        {
            if (value.Length < 3)
            {
                throw new ArgumentException("Занадто коротке ім'я");
            }

            _name = value;
        }
    }
}