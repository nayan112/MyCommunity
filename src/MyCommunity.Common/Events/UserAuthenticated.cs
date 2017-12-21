namespace MyCommunity.Common.Events
{
    public class UserAuthenticated:IEvent
    {
        public string Email{get;}
        public UserAuthenticated()
        {
            
        }
        public UserAuthenticated(string email)
        {
            Email = email;
        }
    }
}