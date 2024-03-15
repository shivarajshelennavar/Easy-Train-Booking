using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Train_App.Models;

namespace Train_App.DataAccess
{
    public interface IUser
    {
        bool RegisterUser(UserDetails user);
        bool LoginUser(long phone, string pass);
        UserDetails FetchMyDetails();
        bool UpdateWallet(decimal amt);
        List<TrainDetails> FetchAvailableTrainDetails();
        bool BookTicket(int Tnum, string seatType, int count, DateTime date);
        List<TrainDetails> FetchBookedTrainDetails();
        bool CancelTicket(int id);
        List<TrainDetails> FetchCancelledTrainDetails();
    }
}
