namespace Project;

public class Check : ICheck
{
    private List<Order> _orders = new ();
    public DateTime _сreatedAt { get; private set; }
    public DateTime _paymentAt { get; private set; }
    public Table _table { get; }
    private Waiter _waiter;
    private Menu _menu;

    public Check(Table table, Menu menu)
    {
        _table = table;
        _сreatedAt = DateTime.Now;
        _menu = menu;
    }

    public void AddOrder(Order order)
    {
        _orders.Add(order);
    }

    public float GetSum()
    {
        float sum = 0;
        foreach (Order order in _orders)
        {
            sum += order.GetSumOfOrder();
        }

        return sum;
    }

    private void ShowCheck()
    {
        Dictionary<string, float> menu = _menu.GetMenu();
        Order tmp = new Order(_menu);
        foreach (Order order in _orders)
        {
            foreach (string key in order._items.Keys)
            {
                tmp.AddItem(key);
            }
        }
        foreach (string key in tmp._items.Keys)
        {
            Console.WriteLine(key + ": " + tmp._items[key] + "x" + menu[key]);
        }

        float totalSum = GetSum();
        
        Console.WriteLine($"Total sum: {totalSum}");
    }

    public void Close()
    {
        _paymentAt = DateTime.Now;
        ShowCheck();
        _table.RemoveWaiter();
        _table.ClearClients();
        _table.Open();
    }
}