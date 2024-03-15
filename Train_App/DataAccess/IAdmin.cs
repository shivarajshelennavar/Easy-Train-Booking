using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Train_App.Models;

namespace Train_App.DataAccess
{
    public interface IAdmin
    {
        bool LoginAdmin(int id, string pass);
        List<TrainDetails> FetchAllTrains();
        List<UserDetails> FetchUserDetails();
        bool UpdateTrain(TrainDetails train);
        bool AddTrain(TrainDetails train);
        bool DeleteTrain(int id);
    }
}
