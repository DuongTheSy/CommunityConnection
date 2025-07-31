using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunityConnection.Common
{
    public class ChannelResponse
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
    }
    public class ListChannelResponse
    {
        public long User_Id { get; set; }
        public long Community_Id { get; set; }
        public List<ChannelResponse> Channels { get; set; }
    }

}
