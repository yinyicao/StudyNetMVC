using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Sql;
using System.Threading.Tasks;

namespace StudyNetMVC.DAL
{
    class Configuration
    {
        public static string DateFormat
        {
            get
            {
                string formatStr = System.Configuration.ConfigurationManager.AppSettings["DateFormat"];
                if (formatStr == null)
                {
                    return "yyyy/MM/dd HH:mm";
                }
                return "yyyy/MM/dd HH:mm";
            }
        }

        private static string mConnectionString = string.Empty;
        public static string ConnectionString
        {
            get
            {
                if (mConnectionString == string.Empty)
                {
                    mConnectionString = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"];
                }

                return mConnectionString;
            }
            set
            {
                mConnectionString = value;
            }
        }

        static EnumNetDbType mDbType = EnumNetDbType.Sql;
        public static EnumNetDbType DbType
        {
            get
            {
                return mDbType;
            }
            set
            {
                mDbType = value;
            }
        }

        //public static int GetOracleSequenceNextValue(string sequenceName)
        //{
        //    string sequel = "select  " + sequenceName + ".nextval from dual";
        //    return (int)(OracleHelper.ExecuteScalar(ConnectionString, CommandType.Text, sequel));
        //}

    }



    public enum EnumNetDbType
    {
        Sql = 0,
        Oracle = 1,
        OleDb = 2
    }
    public enum EnumPersistStatus
    {
        New = 0,
        Update = 1,
        Delete = 2
    }
}
