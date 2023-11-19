namespace Project;

public interface IMenu
{
    void AddProduct(string title, float price);
    void RemoveProduct(string title);
    void UpdatePriceOfProduct(string title, float value);
    Dictionary<string, float> GetMenu();
}