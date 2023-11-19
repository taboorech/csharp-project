namespace Project;

public class Table : ITable
{
    public int _tableNumber { get; private set; }
    private int _countOfPeople;
    private bool _isOccupied;
    private DateTime _startTime;
    private TimeSpan _totalTime;
    private float _totalSum;
    private int _totalClients;
    public List<Client> _clients { get; private set; } = new();
    private List<Order> _activeOrders = new ();
    public Waiter? _waiter { get; private set; }

    public Table(int number, int countOfPeople)
    {
        _tableNumber = number;
        _countOfPeople = countOfPeople;
    }

    public int CountValidation
    {
        get
        {
            return _countOfPeople;
        }
        set
        {
            if (value > 10)
            {
                throw new ArgumentException("Занадто велике число");
            }

            _countOfPeople = value;
        }
    }

    public void Close()
    {
        _startTime = DateTime.Now;
        _isOccupied = true;
    }

    public void Open()
    {
        _totalTime += DateTime.Now - _startTime;
        _isOccupied = false;
    }

    public void AddClient(Client client)
    {
        _clients.Add(client);
        _totalClients += 1;
    }

    public void ClearClients()
    {
        _clients.Clear();
    }

    public void AddWaiter(Waiter waiter)
    {
        _waiter = waiter;
    }

    public void RemoveWaiter()
    {
        _waiter = null;
    }

    public void AddOrder(Order order)
    {
        _activeOrders.Add(order);
        _totalSum += order.GetSumOfOrder();
    }

    public void GetStatistics()
    {
        Console.WriteLine($"Кількість обсугованих клієнтів за столиком: {_clients.Count}");
        Console.WriteLine($"Загальний час роботи столика: {_totalTime}");
        Console.WriteLine($"Загальний сума зароблених грошей зі столика: {_totalSum}");
        Console.WriteLine($"Середній час проведений за столиком: {_totalTime / _totalClients}");
    }
    
}