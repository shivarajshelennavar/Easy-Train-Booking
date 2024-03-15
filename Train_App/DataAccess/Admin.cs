using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Train_App.Models;

namespace Train_App.DataAccess
{
    public class Admin : IAdmin
    {
        private IConfiguration _config;
        protected SqlConnection _connection;

        public Admin(IConfiguration configuration)
        {
            _config = configuration;
        }

        public SqlConnection GetConnection()
        {
            _connection = new SqlConnection(_config.GetSection("Data").GetSection("ConnectionStrings").Value);
            return _connection;
        }

        public bool LoginAdmin(int id, string pass)
        {
            SqlConnection connection = GetConnection();
            connection.Open();

            string query = "SELECT ID FROM ADMIN_DETAILS WHERE ID = @ID AND BINARY_CHECKSUM(PASSWORD) = BINARY_CHECKSUM(@PASS)";
            SqlCommand cmd = new SqlCommand(query, connection);

            cmd.Parameters.AddWithValue("@ID", id);
            cmd.Parameters.AddWithValue("@PASS", pass);

            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.HasRows)
            {
                connection.Close();
                return true;
            }
            else
            {
                connection.Close();
                return false;
            }
        }

        public List<TrainDetails> FetchAllTrains()
        {
            SqlConnection connection = GetConnection();
            connection.Open();

            string query = @"SELECT * FROM TRAIN_DETAILS";
            SqlCommand cmd = new SqlCommand(query, connection);

            SqlDataReader reader = cmd.ExecuteReader();

            List<TrainDetails> trains = new List<TrainDetails>();

            if (reader.HasRows)
            { 
                while (reader.Read())
                {
                    TrainDetails train = new TrainDetails
                    {
                        Id = (int)reader["ID"],
                        TrainNumber = (int)reader["NUMBER"],
                        FromLoc = (string)reader["FROM"],
                        ToLoc = (string)reader["TO"],
                        AvailableWindowSeat = (int)reader["W_SEAT"],
                        AvailableNormalSeat = (int)reader["N_SEAT"],
                        WindowSeatPrice = (decimal)reader["W_PRICE"],
                        NormalSeatPrice = (decimal)reader["N_PRICE"],
                        AvailabiltyStatus = (string)reader["STATUS"]
                    };

                    trains.Add(train);
                }
                reader.Close();
                connection.Close();
                return trains;
            }
            else
            {
                connection.Close();
                return null;
            }
        }

        public List<UserDetails> FetchUserDetails()
        {
            SqlConnection connection = GetConnection();
            connection.Open();

            string query = "SELECT * FROM USER_DETAILS";
            SqlCommand cmd = new SqlCommand(query, connection);

            SqlDataReader reader = cmd.ExecuteReader();
            List<UserDetails> users = new List<UserDetails>();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    UserDetails user = new UserDetails();
                    user.Id = (int)reader["ID"];
                    user.Name = (string)reader["NAME"];
                    user.Number = Convert.ToInt64(reader["PHONE"]);
                    user.Password = (string)reader["PASSWORD"];
                    user.Wallet = (decimal)reader["WALLET"];

                    users.Add(user);
                }
                reader.Close();
                connection.Close();
                return users;
            }
            else
            {
                return null;
            }
        }

        public bool UpdateTrain(TrainDetails train)
        {
            SqlConnection connection = GetConnection();
            connection.Open();

            string query = @"UPDATE TRAIN_DETAILS SET [FROM] = @FROM, [TO] = @TO, 
                            W_SEAT = @WS, N_SEAT = @NS, W_PRICE = @WP, N_PRICE = @NP, [STATUS] = @STATUS
	                        WHERE NUMBER = @NUM";

            SqlCommand cmd = new SqlCommand(query, connection);

            cmd.Parameters.AddWithValue("@ID", train.Id);
            cmd.Parameters.AddWithValue("@NUM", train.TrainNumber);
            cmd.Parameters.AddWithValue("@FROM", train.FromLoc);
            cmd.Parameters.AddWithValue("@TO", train.ToLoc);
            cmd.Parameters.AddWithValue("@WS", train.AvailableWindowSeat);
            cmd.Parameters.AddWithValue("@NS", train.AvailableNormalSeat);
            cmd.Parameters.AddWithValue("@WP", train.WindowSeatPrice);
            cmd.Parameters.AddWithValue("@NP", train.NormalSeatPrice);
            cmd.Parameters.AddWithValue("@STATUS", train.AvailabiltyStatus);

            if (cmd.ExecuteNonQuery() > 0)
            {
                connection.Close();
                return true;
            }
            else
            {
                connection.Close();
                return false;
            }
        }

        public bool AddTrain(TrainDetails train)
        {
            SqlConnection connection = GetConnection();
            connection.Open();

            SqlCommand cmd = new SqlCommand("ADD_TRAIN", connection);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@NUM", train.TrainNumber);
            cmd.Parameters.AddWithValue("@FROM", train.FromLoc);
            cmd.Parameters.AddWithValue("@TO", train.ToLoc);
            cmd.Parameters.AddWithValue("@WS", train.AvailableWindowSeat);
            cmd.Parameters.AddWithValue("@NS", train.AvailableNormalSeat);
            cmd.Parameters.AddWithValue("@WP", train.WindowSeatPrice);
            cmd.Parameters.AddWithValue("@NP", train.NormalSeatPrice);
            cmd.Parameters.AddWithValue("@STATUS", train.AvailabiltyStatus);

            return Convert.ToInt32(cmd.ExecuteScalar()) == 1;
        }

        public bool DeleteTrain(int id)
        {
            SqlConnection connection = GetConnection();
            connection.Open();

            string query = @"DELETE FROM TRAIN_DETAILS WHERE ID = @ID";
            SqlCommand cmd = new SqlCommand(query, connection);

            cmd.Parameters.AddWithValue("@ID", id);

            if (cmd.ExecuteNonQuery() > 0)
            {
                connection.Close();
                return true;
            }
            else
            {
                connection.Close();
                return false;
            }
        }
    }
}
