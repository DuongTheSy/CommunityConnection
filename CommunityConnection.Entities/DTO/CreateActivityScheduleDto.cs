using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunityConnection.Entities.DTO
{
    public class CreateActivityScheduleDto
    {
        public string ActivityName { get; set; } = null!;
        public string? Description { get; set; }

        public DateTime? Date { get; set; }

        public string? StartTime { get; set; }

        public string? EndTime { get; set; }

        public int? Status { get; set; }


    }

}
