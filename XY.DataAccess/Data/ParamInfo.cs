using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XY.DataAccess.Data
{
    public class ParamInfo
    {
        private string paramName;
        /// <summary>
        /// Parameter Name(not @)
        /// </summary>
        public string ParamName
        {
            get { return paramName; }
            set { paramName = value; }
        }

        private string condition;
        /// <summary>
        /// condition
        /// </summary>
        public string Condition
        {
            get { return condition; }
            set { condition = value; }
        }

        private LocationType location;
        /// <summary>
        /// location
        /// </summary>
        public LocationType Location
        {
            get { return location; }
            set { location = value; }
        }

        public ParamInfo() { }

        public ParamInfo(string paramName) : this(paramName, null) { }

        public ParamInfo(string paramName, string condition) : this(paramName, condition, LocationType.Null) { }

        public ParamInfo(string paramName, string condition, LocationType location)
        {
            this.paramName = paramName;
            this.condition = condition;
            this.location = location;
        }
    }
}
