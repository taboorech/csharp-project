using Project;

Console.OutputEncoding = System.Text.Encoding.Unicode;
Console.InputEncoding = System.Text.Encoding.Unicode;

Managment managment = new Managment();

managment.FillWaitersAndTables();

foreach (var group in managment.groups)
{
    managment.AddClient(group);
}

managment.AddOrder(managment._tables[0]._clients[0]);
managment.ShowTableStatistic(managment._tables[0]);
Waiter waiter = managment._tables[0]._waiter;
managment.CheckClose(managment._tables[0]);
managment.ShowStatisticOfWaiter(waiter);