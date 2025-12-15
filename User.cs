namespace Online_shop
{
    /// <summary>
    /// Abstract class represents a user in the online shop
    /// </summary>
    internal abstract class User
    {
        /// <summary>
        /// Property stores user's login
        /// </summary>
        public string Login { get; set; }
        /// <summary>
        /// Property stores user's password
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// Property stores user's email address
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Constructor for User class
        /// </summary>
        /// <param name="login">User's login</param>
        /// <param name="password">User's password</param>
        /// <param name="email">User's email address</param>
        public User(string login, string password, string email)
        {
            Login = login;
            Password = password;
            Email = email;
        }
    }
}
