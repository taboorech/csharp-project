namespace Project;

public class Menu : IMenu
{
    private Dictionary<string, float> _menu = new () {};

    public void AddProduct(string title, float price)
    {
        if (title.Length < 3)
        {
            throw new ArgumentException("Занадто коротка назва");
        }
        
        _menu.Add(title, PriceValidation(price));
    }

    public void RemoveProduct(string title)
    {
        _menu.Remove(title);
    }

    public void UpdatePriceOfProduct(string title, float value)
    {
        _menu[title] = PriceValidation(value);
    }

    public Dictionary<string, float> GetMenu()
    {
        return _menu;
    }

    float PriceValidation(float price)
    {
        if (price <= 0)
        {
            throw new ArgumentException("Ціна не може бути нижче нуля або нулем");
        }

        return price;
    }
}