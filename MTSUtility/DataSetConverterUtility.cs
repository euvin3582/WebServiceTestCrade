using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Diagnostics;
using System.Reflection;

namespace MTSUtility
{
    public class DataSetConverterUtility : IDisposable
    {
        private List<string> _methodDataList = new List<string>();

        public List<string> MethodDataList
        {
            get { return _methodDataList; }
            set { _methodDataList = value; }
        }

        /// <summary>
        /// Converts a DataSet object to a generic list object
        /// </summary>
        /// <param name="methDataSet">Required DataSet object to be converted.</param>
        public DataSetConverterUtility(DataSet methDataSet)
        {
            object[] rowArray = new object[1];
            rowArray[0] = null;
            List<DataRow> drList = methDataSet.Tables[0].AsEnumerable().ToList();

            foreach (DataRow dataValue in drList)
            {
                rowArray = dataValue.ItemArray;
                for (int arrayLength = 0; arrayLength < rowArray.Length; arrayLength++)
                {
                    _methodDataList.Add(rowArray[arrayLength].ToString());
                }
            }
        }

        void IDisposable.Dispose()
        {
            _methodDataList = null;
            GC.Collect();
        }
    }

    public class DateTimeConverter
    {
        //reference: http://stackoverflow.com/questions/7734315/how-can-a-net-date-be-converted-to-nstimeinterval

        /// <summary>
        /// Convert between System.DateTime and a POSIX timestamp (which is what NSTimeInterval is in this situation, plus subsecond precision).
        /// Converts dotnet datetime to apple datetime.
        /// </summary>
        /// <param name="timeStamp">Required DateTime object that represents the dotnet datetime.</param>
        /// <returns>Returns a double that represents the apple datetime.</returns>
        public double ConvertDateTimeToApple(DateTime timeStamp)
        {
            DateTime unixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            DateTime dt = timeStamp;
            double intervalSince1970 = (dt - unixEpoch).TotalSeconds;   
         
            return intervalSince1970;
        }

        /// <summary>
        /// Converts apple datetime to dotnet datetime.
        /// </summary>
        /// <param name="timeStamp">Required double value that represents the apple datetime.</param>
        /// <returns>Returns a DateTime object that represents the dotnet datetime.</returns>
        public DateTime ConvertDateTimeToDotNet(double timeStamp)
        {
            DateTime unixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            double intervalSince1970 = timeStamp;
            DateTime dt = unixEpoch + TimeSpan.FromSeconds(intervalSince1970);

            return dt;
        }
    }
}
