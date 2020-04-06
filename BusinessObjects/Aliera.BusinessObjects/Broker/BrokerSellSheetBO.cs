using System.Collections;
using System.Collections.Generic;

namespace Aliera.BusinessObjects.Broker
{
    public class PlanListBO: IEqualityComparer
    {
        public string Title;
        public string PlanName;
        public int PlanId;
        public int ProductId;
        public string ShellSheetName;
        public string ShellSheetUrl;

        bool IEqualityComparer.Equals(object x, object y)
        {
            var a = x as PlanListBO;
            var b = y as PlanListBO;

            if (a.PlanName.GetHashCode() == b.ShellSheetName.GetHashCode())
                return true;

            return false;
            
        }

        int IEqualityComparer.GetHashCode(object obj)
        {
            return obj.GetHashCode();
        }
    }

    public class PlanListComparer : IEqualityComparer<PlanListBO>
    {
        bool IEqualityComparer<PlanListBO>.Equals(PlanListBO x, PlanListBO y)
        {
            return x.ShellSheetName.GetHashCode() == y.ShellSheetName.GetHashCode() ? true : false;
        }

        int IEqualityComparer<PlanListBO>.GetHashCode(PlanListBO obj)
        {
            return obj.ShellSheetName.GetHashCode();
        }
    }

    public class BrokerSellSheetBO
    {
        public string Title;        
        public  List<PlanListBO> PlanList;
    }
}
