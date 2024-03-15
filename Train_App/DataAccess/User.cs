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
    public class User : IUser
    {
        private IConfiguration _config;
        protected SqlConnection _connection;

        public User(IConfiguration configuration)
        {
            _config = configuration;
        }

        public SqlConnection GetConnection()
        {
            _connection = new SqlConnection(_config.GetSection("Data").GetSection("ConnectionStrings").Value);
            return _connection;
        }

        //Register
        public bool RegisterUser(UserDetails user)
        {
            SqlConnection connection = GetConnection();
            connection.Open();

            SqlCommand cmd = new SqlCommand("REGISTER_USER", connection);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@NAME", user.Name);
            cmd.Parameters.AddWithValue("@PHONE", user.Number);
            cmd.Parameters.AddWithValue("@PASS", user.Password);
            cmd.Parameters.AddWithValue("@WALLET", user.Wallet);

            bool val = Convert.ToInt32(cmd.ExecuteScalar()) == 1;

            return val;
        }

        //Login
        public bool LoginUser(long phone, string pass)
        {
            SqlConnection connection = GetConnection();
            connection.Open();

            string query = "SELECT NAME FROM USER_DETAILS WHERE PHONE" +
                " = @PHONE AND PASSWORD = @PASS";
            SqlCommand cmd = new SqlCommand(query, connection);

            cmd.Parameters.AddWithValue("@PHONE", phone);
            cmd.Parameters.AddWithValue("@PASS", pass);

            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.HasRows)
            {
                UserDetails.UNumber = phone;
                connection.Close();
                return true;
            }
            else
            {
                connection.Close();
                return false;
            }
        }

        public UserDetails FetchMyDetails()
        {
            SqlConnection connection = GetConnection();
            connection.Open();

            string query = "SELECT * FROM USER_DETAILS WHERE PHONE = @PHONE";
            SqlCommand cmd = new SqlCommand(query, connection);

            cmd.Parameters.AddWithValue("@PHONE", UserDetails.UNumber);

            SqlDataReader reader = cmd.ExecuteReader();
            UserDetails user = new UserDetails();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    user.Name = (string)reader["NAME"];
                    user.Number = Convert.ToInt64(reader["PHONE"]);
                    user.Password = (string)reader["PASSWORD"];
                    user.Wallet = (decimal)reader["WALLET"];
                }
                reader.Close();
                connection.Close();
                return user;
            }
            else
            {
                return null;
            }

        }

        public bool UpdateWallet(decimal amt)
        {
            SqlConnection connection = GetConnection();
            connection.Open();

            string query = "UPDATE USER_DETAILS SET WALLET = WALLET + @AMT WHERE PHONE = @PHONE";
            SqlCommand cmd = new SqlCommand(query, connection);

            cmd.Parameters.AddWithValue("@AMT", amt);
            cmd.Parameters.AddWithValue("@PHONE", UserDetails.UNumber);

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

        public List<TrainDetails> FetchAvailableTrainDetails()
        {
            SqlConnection connection = GetConnection();
            connection.Open();

            string query = @"SELECT * FROM TRAIN_DETAILS WHERE STATUS = 'Available'";
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

        public bool BookTicket(int Tnum, string seatType, int count, DateTime date)
        {
            SqlConnection connection = GetConnection();
            connection.Open();

            SqlCommand cmd = new SqlCommand("BOOK_TICKET", connection);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@T_NUM", Tnum);
            cmd.Parameters.AddWithValue("@SEAT_TYPE", seatType);
            cmd.Parameters.AddWithValue("@SEAT_COUNT", count);
            cmd.Parameters.AddWithValue("@JOURNEY_DATE", date);
            cmd.Parameters.AddWithValue("@PHONE", UserDetails.UNumber);

            bool val = Convert.ToInt32(cmd.ExecuteScalar()) == 1;
            return val;
        }

        public List<TrainDetails> FetchBookedTrainDetails()
        {
            SqlConnection connection = GetConnection();
            connection.Open();

            string query = @"SELECT H.ID, T.NUMBER, T.[FROM], T.[TO], H.TOTAL_PRICE, H.BOOKING_DATE, H.JOURNEY_DATE, H.SEAT_TYPE, H.SEAT_COUNT
                            FROM TRAIN_HISTORY H 
                            JOIN TRAIN_DETAILS T
                            ON T.NUMBER = H.TRAIN_NUMBER
                            WHERE H.OWNER_NUMBER = @PHONE AND H.STATUS = 'Booked'";

            SqlCommand cmd = new SqlCommand(query, connection);

            cmd.Parameters.AddWithValue("@PHONE", UserDetails.UNumber);

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
                        TotalPrice = (decimal)reader["TOTAL_PRICE"],
                        BookingDate = reader.GetDateTime(reader.GetOrdinal("BOOKING_DATE")).ToString("yyyy-MM-dd"),
                        JourneyDate = reader.GetDateTime(reader.GetOrdinal("JOURNEY_DATE")).ToString("yyyy-MM-dd"),
                        SeatType = (string)reader["SEAT_TYPE"],
                        SeatCount = (int)reader["SEAT_COUNT"]
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

        public List<TrainDetails> FetchCancelledTrainDetails()
        {
            SqlConnection connection = GetConnection();
            connection.Open();

            string query = @"SELECT H.ID, T.NUMBER, T.[FROM], T.[TO], H.TOTAL_PRICE, H.BOOKING_DATE, H.JOURNEY_DATE, H.SEAT_TYPE, H.SEAT_COUNT, 
							H.CANCEL_DATE, H.RETURN_AMT
                            FROM TRAIN_HISTORY H 
                            JOIN TRAIN_DETAILS T
                            ON T.NUMBER = H.TRAIN_NUMBER
                            WHERE H.OWNER_NUMBER = @PHONE AND H.STATUS = 'Cancelled'";

            SqlCommand cmd = new SqlCommand(query, connection);

            cmd.Parameters.AddWithValue("@PHONE", UserDetails.UNumber);

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
                        TotalPrice = (decimal)reader["TOTAL_PRICE"],
                        BookingDate = reader.GetDateTime(reader.GetOrdinal("BOOKING_DATE")).ToString("yyyy-MM-dd"),
                        JourneyDate = reader.GetDateTime(reader.GetOrdinal("JOURNEY_DATE")).ToString("yyyy-MM-dd"),
                        SeatType = (string)reader["SEAT_TYPE"],
                        SeatCount = (int)reader["SEAT_COUNT"],
                        CancelDate = reader.GetDateTime(reader.GetOrdinal("CANCEL_DATE")).ToString("yyyy-MM-dd"),
                        RefundAmt = (decimal)reader["RETURN_AMT"]
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

        public bool CancelTicket(int id)
        {
            SqlConnection connection = GetConnection();
            connection.Open();

            SqlCommand cmd = new SqlCommand("CANCEL_TICKET", connection);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@ID", id);
            cmd.Parameters.AddWithValue("@PHONE", UserDetails.UNumber);

            return Convert.ToInt32(cmd.ExecuteScalar()) == 1;
        }
    }
}
