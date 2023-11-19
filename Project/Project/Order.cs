namespace Project;

public class Order
{
    public Dictionary<string, int> _items = new();
    private OrderStatus _status = 0;
    private Dictionary<string, float> _menu = new ();

    public Order(Menu menu)
    {
        _menu = menu.GetMenu();
    }

    public int AddItem(string item)
    {
        if (_items.ContainsKey(item))
        {
            return _items[item]++;
        }

        return _items[item] = 1;
    }

    private void ShowStatuses()
    {
        int i = 1;
        Console.WriteLine("Статуси замовлення: ");
        foreach (string status in Enum.GetNames(typeof(OrderStatus)))
        {
            Console.WriteLine(i + ": " + status);
            i++;
        }
    }

    private OrderStatus ChooseStatus(int index)
    {
        return (OrderStatus)index;
    }

    public void ChangeOrderStatus()
    {
        ShowStatuses();
        Console.WriteLine("Введіть індекс статусу: ");
        _status = ChooseStatus(Convert.ToInt32(Console.ReadLine()) - 1);
    }

    public float GetSumOfOrder()
    {
        float sum = 0;
        foreach (string key in _items.Keys)
        {
            sum += _menu[key];
        }

        return sum;
    }
}