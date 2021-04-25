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

        private string selectByLoginAndPass => $"SELECT id, firstname, secondname, lastname, phone, email" +
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
            return CreateCurrentUser(selectedRow.ItemArray);
        }

        private User CreateCurrentUser(object[] personalData)
        {
            User currentUser = new ();
            var properties = currentUser.GetType().GetProperties().Skip(1).ToArray();
            for (int i = 0; i < personalData.Length; i++)
            {
                if (properties[i].Name == "UserId")
                {
                    properties[i].SetValue(currentUser, Convert.ToInt32(personalData[i]));
                }
                else
                    properties[i].SetValue(currentUser, personalData[i]);
            }
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