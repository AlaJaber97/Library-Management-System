namespace LibraryMS
{
    public abstract class Administration : IAuthentication
    {
        public string Id { get; set; }
        private string Password { get; set; }
        public Administration(string id, string password)
        {
            Id = id;
            Password = password;
        }
        public bool IsAuthentication(string id, string password)
        {
            return string.Equals(Id, id) && string.Equals(Password, password);
        }
    }
}