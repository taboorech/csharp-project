using Newtonsoft.Json;

namespace Project;

public class Managment
{
    private Queue<Waiter> _waiters = new();
    public List<Table> _tables { get; } = new();
    private Menu _menu = new ();
    private Queue<Order> _orders = new();
    public List<List<string>> groups { get; } = new ();
    private Statistics _dayStatistics = new Statistics(StatisticStatus.Day);
    private Statistics _weekStatistics = new Statistics(StatisticStatus.Week);
    private Statistics _monthStatistics = new Statistics(StatisticStatus.Month);
    private Statistics _sectionStatistics = new Statistics(StatisticStatus.Section);
    private Statistics _yearStatistics = new Statistics(StatisticStatus.Year);
    
    public void FillWaitersAndTables()
    {
        using (StreamReader fs = new StreamReader("input.json"))
        {
            string json = fs.ReadToEnd();
            
            Information information = JsonConvert.DeserializeObject<Information>(json);
            
            foreach (int tableNumber in information.tables)
            {
                _tables.Add(new Table(tableNumber, 4));
            }
            foreach (var dict in information.clients)
            {
                foreach (string key in dict.Keys)
                {
                    groups.Add(dict[key]);
                }
            }
            foreach (string key in information.menu.Keys)
            { 
                _menu.AddProduct(key, information.menu[key]);
            }
            foreach (string waiter in information.waiters)
            {
                _waiters.Enqueue(new Waiter(waiter));
            }
        }
    }

    public void AddClient(List<string> names)
    {
        Table? tmp = _tables[0];
        Check check = new Check(tmp, _menu);
        for(int i = 0; i < _tables.Count; i++)
        {
            if (tmp != null)
            {
                break;
            }
            foreach (Table table in _tables)
            {
                if (table.CountValidation == names.Count + i)
                {
                    tmp = table;
                    break;
                }
            }
        }

        if (tmp == null)
        {
            throw new Exception("Немає вільного столика");
        }
        
        tmp.Close();
        foreach (string name in names)
        {
            Client client = new Client(name);
            client.ConnectCheck(check);
            tmp.AddClient(client);
            client.AddTable(tmp);
        }

        Waiter waiter = _waiters.Dequeue();
        _waiters.Enqueue(waiter);
        tmp.AddWaiter(waiter);
    }

    private void UpdateStatistic(float payment)
    {
        _dayStatistics.UpdateValue(payment);
        _weekStatistics.UpdateValue(payment);
        _monthStatistics.UpdateValue(payment);
        _sectionStatistics.UpdateValue(payment);
        _yearStatistics.UpdateValue(payment);
    }

    public void AddOrder(Client client)
    {
        Order order = new Order(_menu);
        Dictionary<string, float> menu = _menu.GetMenu();

        void ShowMenu()
        {
            int i = 1;
            foreach (string key in menu.Keys)
            {
                Console.WriteLine(i + ". " + key + ": " + menu[key]);
                i++;
            }
            
            Console.WriteLine();
        }

        string ChooseItem()
        {
            ShowMenu();
            Console.Write("Введіть індекс: ");
            return FindItem(Convert.ToInt32(Console.ReadLine()) - 1);
        }
        
        string FindItem(int index)
        {
            int i = 0;
            foreach (string key in menu.Keys)
            {
                if (i == index)
                {
                    return key;
                }
                i++;
            }
            Console.WriteLine("Неправильний вибір");
            Console.WriteLine("Спробувати ще?");
            Console.WriteLine("1. Так");
            Console.WriteLine("0. Ні");
            int isOkey = Convert.ToInt32(Console.ReadLine());
            if (!Convert.ToBoolean(isOkey))
            {
                return null;
            }

            return ChooseItem();
        }

        int operation;
        do
        {
            Console.WriteLine("Бажаєте замовити щось?");
            Console.WriteLine("1. Так");
            Console.WriteLine("0. Ні");
            operation = Convert.ToInt32(Console.ReadLine());
            if (operation == 1)
            {
                string item = ChooseItem();
                order.AddItem(item);

                if (client.GetCheck() == null)
                {
                    Table tmpTable = client._table;
                    Console.WriteLine($"F:{tmpTable}");
                    Check check = new Check(tmpTable, _menu);
                    foreach (Client deepClient in tmpTable._clients)
                    {
                        deepClient.ConnectCheck(check);
                    }
                }
                client.GetCheck().AddOrder(order);
            }
        } while (Convert.ToBoolean(operation));

        if (Convert.ToBoolean(order._items.Count))
        {
            _orders.Enqueue(order);
            client._table.AddOrder(order);
            UpdateStatistic(order.GetSumOfOrder());
        }
    }

    public void AddItemInMenu(string title, int price)
    {
        _menu.AddProduct(title, price);
    }

    public void CheckClose(Table table)
    {
        Check check = table._clients[0].GetCheck();
        table._waiter?.AddСheck(check);
        check.Close();
    }

    public void ShowStatisticOfWaiter(Waiter waiter)
    {
        waiter.GetStatistic();
    }

    public void ShowTableStatistic(Table table)
    {
        table.GetStatistics();
    }

    public void UpdateOrderStatus(Order order)
    {
        order.ChangeOrderStatus();
    }
}