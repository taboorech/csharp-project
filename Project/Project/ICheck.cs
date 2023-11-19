namespace Project;

public interface ICheck
{
    void AddOrder(Order order);
    float GetSum();
    void Close();
}