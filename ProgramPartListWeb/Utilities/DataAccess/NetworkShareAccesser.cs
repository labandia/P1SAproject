using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;

namespace ProgramPartListWeb.Utilities.DataAccess
{
    public class NetworkShareAccesser : IDisposable
    {
        private readonly string _networkName;

        public NetworkShareAccesser(string networkName, string userName, string password)
        {
            _networkName = networkName;

            var netResource = new NetResource
            {
                Scope = ResourceScope.GlobalNetwork,
                ResourceType = ResourceType.Disk,
                DisplayType = ResourceDisplaytype.Share,
                RemoteName = networkName
            };

            var result = WNetAddConnection2(
                netResource,
                password,
                userName,
                0);

            if (result != 0)
                throw new Win32Exception(result, "Error connecting to network share");
        }

        ~NetworkShareAccesser()
        {
            Dispose(false);
        }

        public static NetworkShareAccesser Access(string networkName, string userName, string password)
            => new NetworkShareAccesser(networkName, userName, password);

        public void Dispose() => Dispose(true);

        protected virtual void Dispose(bool disposing)
        {
            WNetCancelConnection2(_networkName, 0, true);
            if (disposing)
                GC.SuppressFinalize(this);
        }

        [DllImport("mpr.dll")]
        private static extern int WNetAddConnection2(NetResource netResource,
            string password, string username, int flags);

        [DllImport("mpr.dll")]
        private static extern int WNetCancelConnection2(string name, int flags, bool force);

        [StructLayout(LayoutKind.Sequential)]
        public class NetResource
        {
            public ResourceScope Scope;
            public ResourceType ResourceType;
            public ResourceDisplaytype DisplayType;
            public int Usage;
            public string LocalName;
            public string RemoteName;
            public string Comment;
            public string Provider;
        }

        public enum ResourceScope : int
        {
            Connected = 1,
            GlobalNetwork,
            Remembered,
            Recent,
            Context
        };

        public enum ResourceType : int
        {
            Any = 0,
            Disk = 1,
            Print = 2,
            Reserved = 8,
        }

        public enum ResourceDisplaytype : int
        {
            Generic = 0x0,
            Domain = 0x01,
            Server = 0x02,
            Share = 0x03,
            File = 0x04,
            Group = 0x05,
            Network = 0x06,
            Root = 0x07,
            Shareadmin = 0x08,
            Directory = 0x09,
            Tree = 0x0a,
            Ndscontainer = 0x0b
        }
    }
}