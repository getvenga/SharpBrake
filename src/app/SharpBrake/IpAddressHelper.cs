using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;

namespace SharpBrake
{
    public static class IpAddressHelper
    {
        public static string GetFirstAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            var address = host.AddressList.FirstOrDefault(i => i.AddressFamily == AddressFamily.InterNetwork);
            if (address == null)
            {
                address = host.AddressList.FirstOrDefault(i => i.AddressFamily == AddressFamily.InterNetworkV6);
            }
            return address?.ToString();
        }
    }
}
