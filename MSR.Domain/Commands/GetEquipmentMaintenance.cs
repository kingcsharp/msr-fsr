
using MSR.Domain.Commanding;
using System;

namespace MSR.Domain.Commands
{
    public class GetEquipmentMaintenance : Command
    {
        public int? Id { get; set; }
    }
}
