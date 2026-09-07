using System; 

/// <summary>
/// Summary description for Session
/// </summary>
public class Password
{
    public static string Generate_Password()
    {
        Random rdm = new Random();
        return (rdm.Next(1111111111, 2122222222).ToString());

    }
}
