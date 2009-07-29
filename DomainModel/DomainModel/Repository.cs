﻿using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using NUnit.Framework;

namespace DomainModel
{
    public abstract class Repository
    {
       
         static Configuration Configuration;
       

        public static ISessionFactory InitalizeSessionFactory(params FileInfo[] hbmFiles)
        {
           // if (SessionFactory != null)
            //    return;

            var properties = new Dictionary<string, string>();
            properties.Add("connection.driver_class", "NHibernate.Driver.SQLite20Driver");
            properties.Add("dialect", "NHibernate.Dialect.SQLiteDialect");
            properties.Add("connection.provider", "NHibernate.Connection.DriverConnectionProvider");
            properties.Add("connection.connection_string", "Data Source=:memory:;Version=3;New=True;");
            properties.Add("connection.release_mode", "on_close");
            properties.Add("show_sql", "true");

            Configuration = new Configuration();
            Configuration.Properties = properties;

            foreach (FileInfo mappingFile in hbmFiles)
            {
                Configuration = Configuration.AddFile(mappingFile);
            }
            Configuration.BuildMapping();
            ISessionFactory sessionFactory = Configuration.BuildSessionFactory();

            return sessionFactory;
        }

        public static ISession CreateSession(ISessionFactory sessionFactory )
        {
            ISession openSession = sessionFactory.OpenSession();
            IDbConnection connection = openSession.Connection;
            new SchemaExport(Configuration).Execute(false, true, false, true, connection, null);
            return openSession;
        }


        
    }
}
