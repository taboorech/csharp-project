namespace Project;

public class Statistics
{
    private int _orders = 0;
    private float _payment = 0;
    private DateTime _dateOfStart;
    private StatisticStatus _status;

    public Statistics(StatisticStatus status)
    {
        _status = status;
    }

    public void GetValues()
    {
        Console.WriteLine($"З {_dateOfStart}");
        Console.WriteLine($"Виконано замовлень: {_orders}");
        Console.WriteLine($"Отримано грошей: {_payment}");
    }

    public void UpdateValue(float payment)
    {
        if (_orders == 0)
        {
            _dateOfStart = DateTime.Now;
            TimeSpan option = _dateOfStart.AddDays(1) - _dateOfStart;
            if(_status == StatisticStatus.Week)
            {
                option = _dateOfStart.AddDays(7) - _dateOfStart;
            }
            else if (_status == StatisticStatus.Month)
            {
                option = _dateOfStart.AddMonths(1) - _dateOfStart;
            } else if (_status == StatisticStatus.Section)
            {
                option = _dateOfStart.AddMonths(3) - _dateOfStart;
            } else if (_status == StatisticStatus.Year)
            {
                option = _dateOfStart.AddYears(1) - _dateOfStart;
            }
        
            var timer = new System.Timers.Timer(option);
            timer.Elapsed += (sender, e) => Reset();
            timer.AutoReset = false;
            timer.Enabled = true;
        }
        _orders++;
        _payment += payment;
    }

    private void Reset()
    {
        _orders = 0;
        _payment = 0;
    }
}