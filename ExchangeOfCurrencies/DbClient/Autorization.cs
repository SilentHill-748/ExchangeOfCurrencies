using System;
using System.Linq;
using System.Data;

using ExchangeOfCurrencies.ClientModel;

namespace ExchangeOfCurrencies.DbClient
{
    public class Autorization
    {
        private string login;
        private string password;

        private string selectByLoginAndPass => $"SELECT firstname, secondname, lastname, email, phone, login, password" +
            $" FROM users WHERE login = \'{login}\' AND  password = \'{password}\';";

        private DataRow selectedRow;

        public Autorization(string login, string password)
        {
            this.login = login;
            this.password = password;
        }

        public User BeginAutorization()
        {
            GetSelectedRows();
            var personalData = selectedRow.ItemArray.Select(value => value as string).ToArray();
            return CreateCurrentUser(personalData);
        }

        private User CreateCurrentUser(string[] personalData)
        {
            User currentUser = new ();
            var properties = currentUser.GetType().GetProperties();
            for (int i = 0; i < personalData.Length; i++)
                properties[i].SetValue(currentUser, personalData[i]);
            return currentUser;
        }

        private void GetSelectedRows()
        {
            DataSet data = Request.Send(selectByLoginAndPass);
            if (data.Tables.Count == 0)
                throw new Exception("Введен неверный логин или пароль!");
            selectedRow = data.Tables[0].Rows[0];
        }
    }
}