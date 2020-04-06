using System.Net.Sockets;

namespace Aliera.Utilities.IPAddress
{
    public class IPAddressRange
    {
        readonly AddressFamily addressFamily;
        readonly byte[] lowerBytes;
        readonly byte[] upperBytes;

        public IPAddressRange(System.Net.IPAddress lowerInclusive, System.Net.IPAddress upperInclusive)
        {
            // Assert that lower.AddressFamily == upper.AddressFamily

            this.addressFamily = lowerInclusive.AddressFamily;
            this.lowerBytes = lowerInclusive.GetAddressBytes();
            this.upperBytes = upperInclusive.GetAddressBytes();
        }

        public bool IsInRange(System.Net.IPAddress address)
        {
            if (address.AddressFamily != addressFamily)
            {
                return false;
            }

            var addressBytes = address.GetAddressBytes();
            var lowerBoundary = true;
            var upperBoundary = true;

            for (int i = 0; i < this.lowerBytes.Length &&
                (lowerBoundary || upperBoundary); i++)
            {
                if ((lowerBoundary && addressBytes[i] < lowerBytes[i]) ||
                    (upperBoundary && addressBytes[i] > upperBytes[i]))
                {
                    return false;
                }

                lowerBoundary &= (addressBytes[i] == lowerBytes[i]);
                upperBoundary &= (addressBytes[i] == upperBytes[i]);
            }

            return true;
        }
    }
}
