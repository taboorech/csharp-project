namespace Project;

public interface IOrder
{
    Dictionary<string, int> _items { get; }
    void ChangeOrderStatus();
    float GetSumOfOrder();
}