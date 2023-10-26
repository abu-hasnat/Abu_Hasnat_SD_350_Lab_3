public abstract class User
{
    public string Password { get; set; }
    public abstract string PasswordHash();
}

public class AuthorizeUser : User
{
    public override string PasswordHash()
    {
        return Password + "Authorized";
    }
}

public class Administrator : User
{
    public override string PasswordHash()
    {
        return Password + "Administrator";
    }
}

public abstract class System
{
    protected abstract User CreateUser(string JSON);
}

public class TwoFactorRequired: System
{
    protected override User CreateUser(string JSON)
    {
        if(JSON == "TwoFactorAuthentication: true")
        {
            return new AuthorizeUser();
        }
        else if(JSON == "IsAdmin: true")
        {
            return new Administrator();
        }
       // throw new Exception("Invalid");


    }
}

public class TwoFactorNotRequired : System
{
    protected override User CreateUser(string JSON)
    {
        if(JSON == "TwoFactorAuthenticatio: true")
        {
            return new AuthorizeUser();
        }
        else if(JSON == "IsAdmin: true")
        {
            return new Administrator();
        }

        return new AuthorizeUser();
    }
}
