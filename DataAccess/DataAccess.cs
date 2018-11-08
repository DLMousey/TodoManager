using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary
{
    public static class DataAccess
    {
        public static void InitialiseDatabase()
        {
            using (SqliteConnection db =
                new SqliteConnection("Filename=sqliteSample.db"))
            {
                db.Open();

                String TableCommand = "CREATE TABLE IF NOT " +
                    "EXISTS TodoItems (" +
                    "`id` INTEGER PRIMARY KEY," +
                    "`title` VARCHAR(50) NOT NULL DEFAULT 'Set a title'," +
                    "`detail` TEXT NULL," +
                    "`date_created` DATETIME NULL DEFAULT CURRENT_TIMESTAMP" +
                    ")";

                SqliteCommand CreateTable = new SqliteCommand(TableCommand, db);
                CreateTable.ExecuteReader();
            }
        }

        public static void AddData(TodoModel TodoModel)
        {
            using (SqliteConnection db =
                new SqliteConnection("Filename=sqliteSample.db"))
            {
                db.Open();

                SqliteCommand insertCommand = new SqliteCommand();
                insertCommand.Connection = db;

                List<SqliteParameter> QueryParams = new List<SqliteParameter>();
                QueryParams.Add(new SqliteParameter("@title", TodoModel.Title));
                QueryParams.Add(new SqliteParameter("@description", TodoModel.Description));

                insertCommand.CommandText = "INSERT INTO TodoItems VALUES (" +
                    "null, @title, @detail, date('now')" +
                    ")";

                insertCommand.Parameters.AddWithValue("@title", TodoModel.Title);
                insertCommand.Parameters.AddWithValue("@detail", TodoModel.Description);

                insertCommand.ExecuteReader();

                db.Close();
            }
        }

        public static List<TodoModel> GetData()
        {
            List<TodoModel> entries = new List<TodoModel>();

            using (SqliteConnection db =
                new SqliteConnection("Filename=sqliteSample.db"))
            {
                db.Open();

                SqliteCommand selectCommand = new SqliteCommand
                    ("SELECT * FROM TodoItems", db);

                SqliteDataReader query = selectCommand.ExecuteReader();

                while (query.Read())
                {
                    TodoModel Todo = new TodoModel
                    {
                        Id = query.GetInt32(0),
                        Title = query.GetString(1),
                        Description = query.GetString(2),
                        DateCreated = query.GetDateTime(3)
                    };

                    entries.Add(Todo);
                }

                db.Close();
            }

            return entries;
        }

        public static TodoModel GetDetail(int id)
        {
            TodoModel Todo = new TodoModel();

            using (SqliteConnection db =
                new SqliteConnection("Filename=sqliteSample.db"))
            {
                db.Open();

                SqliteCommand selectCommand = new SqliteCommand
                    ("SELECT * FROM TodoItems WHERE id = @id", db);

                selectCommand.Parameters.AddWithValue("@id", id);
                SqliteDataReader query = selectCommand.ExecuteReader();

                while (query.Read())
                {
                    Todo.Id = query.GetInt32(0);
                    Todo.Title = query.GetString(1);
                    Todo.Description = query.GetString(2);
                    Todo.DateCreated = query.GetDateTime(3);
                }

                db.Close();
            }

            return Todo;
        }

        public static void DeleteData(string InputText)
        {
            using (SqliteConnection db =
                new SqliteConnection("Filename=sqliteSample.db"))
            {
                db.Open();

                SqliteCommand deleteCommand = new SqliteCommand();
                deleteCommand.Connection = db;

                deleteCommand.CommandText = "DELETE FROM MyTable WHERE Text_Entry = @Entry";
                deleteCommand.Parameters.AddWithValue("@Entry", InputText);

                deleteCommand.ExecuteReader();

                db.Close();
            }
        }
    }
}
