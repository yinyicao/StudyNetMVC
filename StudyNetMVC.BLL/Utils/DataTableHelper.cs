using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace StudyNetMVC.BLL.Utils
{
    /// <summary>
    /// 将DataTable中的内容利用反射机制转换为实体
    /// 该类中有两个方法
    /// 1.转换为单个实体
    /// 2.转换为集合实体
    /// </summary>
    public static class DataTableHelper
    {

        #region 利用反射把DataTable的数据写到单个实体类

        public static T ToSingleEntity<T>(this System.Data.DataTable dtSource)

        {

            if (dtSource == null)

            {

                return default(T);

            }



            if (dtSource.Rows.Count != 0)

            {

                Type type = typeof(T);

                Object entity = Activator.CreateInstance(type);         //创建实例               

                foreach (PropertyInfo entityCols in type.GetProperties())

                {

                    if (!string.IsNullOrEmpty(dtSource.Rows[0][entityCols.Name].ToString()))

                    {

                        entityCols.SetValue(entity, Convert.ChangeType(dtSource.Rows[0][entityCols.Name], entityCols.PropertyType), null);

                    }

                }

                return (T)entity;

            }

            return default(T);

        }

        #endregion



        #region 利用反射把DataTable的数据写到集合实体类里

        public static List<T> ToListEntity<T>(this System.Data.DataTable dtSource)

        {

            if (dtSource == null)

            {

                return null;

            }



            List<T> list = new List<T>();

            Type type = typeof(T);

            foreach (DataRow dataRow in dtSource.Rows)

            {

                Object entity = Activator.CreateInstance(type);         //创建实例               

                foreach (PropertyInfo entityCols in type.GetProperties())

                {

                    if (!string.IsNullOrEmpty(dataRow[entityCols.Name].ToString()))

                    {

                        entityCols.SetValue(entity, Convert.ChangeType(dataRow[entityCols.Name], entityCols.PropertyType), null);

                    }

                }

                list.Add((T)entity);

            }

            return list;

        }

        #endregion

    }
}
