using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data.SQLite;

namespace DataAccessLibrary
{
    public class SqlDataAccess : ISqlDataAccess
    {
        private readonly string connectionString;

        public SqlDataAccess() 
        {
            Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;  
            var dbFilePath = "./cookbook.sqlite";
            if (!File.Exists(dbFilePath))
            {
                SQLiteConnection.CreateFile(dbFilePath);
            }
            connectionString = string.Format("Data Source={0};", dbFilePath);
            CreateTables();
        }

        private void CreateTables()
        {
            using (IDbConnection connection = new SQLiteConnection(connectionString))
            {
                string sql = @"

                    CREATE TABLE IF NOT EXISTS ""RECIPE"" (
                        id INTEGER PRIMARY KEY,
                        name TEXT NOT NULL,
                        recipe TEXT NOT NULL,
                        numberOfPortions INTEGER NOT NULL,
                        type TEXT NOT NULL
                    );

                    CREATE TABLE IF NOT EXISTS ""INGREDIENT"" (
                        id INTEGER PRIMARY KEY,
                        name TEXT NOT NULL,
                        category TEXT NOT NULL,
                        unit TEXT NOT NULL
                    );

                    CREATE TABLE IF NOT EXISTS ""RECIPE_INGREDIENT"" (
                        recipe_id INTEGER,
                        ingredient_id INTEGER,
                        amount DOUBLE NOT NULL,
                        PRIMARY KEY (recipe_id, ingredient_id),
                        FOREIGN KEY (recipe_id) 
                            REFERENCES Recipe (id) 
                                ON DELETE RESTRICT
                                ON UPDATE NO ACTION,
                        FOREIGN KEY (ingredient_id) 
                            REFERENCES ingredient (id) 
                                ON DELETE RESTRICT 
                                ON UPDATE NO ACTION
                    );
                    
                    CREATE TABLE IF NOT EXISTS ""PERSON"" (
                        id INTEGER PRIMARY KEY,
                        name TEXT NOT NULL,
                        coefficient REAL NOT NULL
                    );";
                connection.Execute(sql);
            }
        }

        public async Task<List<T>> LoadData<T, U>(string sql, U parameters)
        {

            using (IDbConnection connection = new SQLiteConnection(connectionString))
            {
                var data = await connection.QueryAsync<T>(sql, parameters);

                return data.ToList();
            }
        }

        public async Task<int> SaveData<T>(string sql, T parameters)
        {

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                SQLiteTransaction transaction = null;
                connection.Open();

                transaction = connection.BeginTransaction();
                    await connection.ExecuteAsync(sql, parameters);
                    var rowID = (int)connection.LastInsertRowId;
                transaction.Commit();

                return rowID;
            }
        }

    }
}
