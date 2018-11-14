﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Driver;

namespace PatientData.Models
{
    public static class PatientDb
    {
        public static IMongoCollection<Patient> Open()
        {
            var client=new MongoClient("mongodb://localhost");
            var db = client.GetDatabase("PatientDb");
            return db.GetCollection<Patient>("Patients");
        }
    }
}