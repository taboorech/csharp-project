namespace Project;

public class Waiter : IWaiter
{
    public string _name;
    private List<Check> _checks = new ();
    private float _totalTips;
    private float _averageTips;
    public Waiter(string name)
    {
        NameValidation = name;
    }

    public void AddСheck(Check check)
    {
        _checks.Add(check);
    }

    private float CalculateTotalTips()
    {
        float sum = 0;
        foreach (Check check in _checks)
        {
            sum += Convert.ToSingle(check.GetSum() * 0.05);
        }

        _totalTips = sum;
        return sum;
    }
    
    private float CalculateAverageTips()
    {

        _averageTips = CalculateTotalTips() / _checks.Count;
        return _averageTips;
    }

    public void GetStatistic()
    {
        for (int i = 0; i < _checks.Count; i++)
        {
            float sumByCheck = _checks[i].GetSum();
            Console.WriteLine($"Чек №{i}");
            Console.WriteLine($"Час роботи зі столиком {_checks[i]._table._tableNumber}: {Math.Round((_checks[i]._paymentAt - _checks[i]._сreatedAt).TotalMinutes, 2)} хвилин");
            Console.WriteLine($"Зароблено закладом зі столику: {sumByCheck}");
            Console.WriteLine($"Зароблено офіціантом(5%): {Math.Round(sumByCheck * 0.05, 2)}");
        }
        Console.WriteLine($"Загалом отримано чайових: {Math.Round(CalculateTotalTips(), 2)}");
        Console.WriteLine($"Середній розмір чайових: {Math.Round(CalculateAverageTips(), 2)}");
    }

    string NameValidation
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