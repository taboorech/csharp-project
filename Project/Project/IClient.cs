namespace Project;

public interface IClient
{
    void ConnectCheck(Check check);
    Check GetCheck();
}