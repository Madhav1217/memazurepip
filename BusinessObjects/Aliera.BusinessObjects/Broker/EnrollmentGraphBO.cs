using System.Collections.Generic;
using Aliera.Utilities.Enumerations;

namespace Aliera.BusinessObjects.Broker
{
    public class EnrollmentGraphBO
    {
        public ViewBy ViewBy { get; set; }
        public long LoggedInBrokerId { get; set; }
        public string BrokerId { get; set; }
        public EnrollmentPeriod SelectedPeriod { get; set; }
        public List<LineChartBO> LineChartData { get; set; }

    }
}
