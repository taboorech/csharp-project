namespace Project;

public interface ITable
{
    int CountValidation { get; set; }
    List<Client> _clients { get; }

    void Close();
    void Open();
    void AddClient(Client client);
    void ClearClients();
    void AddWaiter(Waiter waiter);
    void RemoveWaiter();
    void AddOrder(Order order);
    void GetStatistics();
}