using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;

namespace CA2OOPS00213942
{
   public class Activity : IComparable
    {
        //Variables
        public string activityTitle { get; set; }
        public DateTime dateOfActivity { get; set; }
        public string typeOfActivity { get; set; }

        public int Cost { get; set; }


        //checks dates for acivities and sorts dates from soonest 
        public int CompareTo(object obj)
        {
            if (obj == null) return new int();
            Activity diffActivity = obj as Activity;
            if (diffActivity != null)
                return this.dateOfActivity.CompareTo(diffActivity.dateOfActivity);
            else
                throw new ArgumentException("Object is not an Activity");
        }

        public override string ToString()
        {
            return $"{activityTitle} - {dateOfActivity.ToShortDateString()}";
        }

      
    }
}
