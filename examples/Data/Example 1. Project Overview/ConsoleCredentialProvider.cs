using Comos.Walkinside.Common.Http;
using System;
using System.Net;
using System.Security;

namespace DataSdkExamples
{
    /// <summary>
    /// Implements console input of user name and password.
    /// </summary>
    class ConsoleCredentialProvider : ICredentialProvider
    {
        public NetworkCredential GetCredential(string resourceName, string[] authTypes)
        {
            if (this.goodCreds != null)
            {
                return this.goodCreds;
            }

            Console.WriteLine("Please provide credentials for {0}", resourceName);

            var userName = string.Empty;
            do
            {
                Console.Write("User name: ");
                userName = Console.ReadLine();
            }
            while (string.IsNullOrWhiteSpace(userName));

            Console.Write("Password: ");
            var password = new SecureString();
            for (; ; )
            {
                var key = Console.ReadKey(intercept: true);

                if (key.Key == ConsoleKey.Enter)
                {
                    Console.WriteLine();
                    break;
                }

                if (key.Key == ConsoleKey.Backspace)
                {
                    if (password.Length > 0)
                    {
                        password.RemoveAt(password.Length - 1);
                        Console.Write("\b \b");
                    }
                    continue;
                }

                password.AppendChar(key.KeyChar);
                Console.Write("*");
            }

            return new NetworkCredential(userName: userName, password: password);
        }

        public void ConfirmCredential(
            NetworkCredential credential,
            bool confirmed)
        {
            if (confirmed)
            {
                this.goodCreds = credential;
            }
            else
            {
                this.goodCreds = null;
            }
        }

        NetworkCredential goodCreds;
    }
}
