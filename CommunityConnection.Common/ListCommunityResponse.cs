using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunityConnection.Common
{
    public class CommunityResponse
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class ListCommunityResponse
    {
        public long User_Id { get; set; }
        public List<CommunityResponse> Communities { get; set; }
    }
}
