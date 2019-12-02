using Nubalance.BuildingBlocks.Domain;

namespace IGRMgr.Modules.Administration.Domain.Staffs
{
    public class StaffGender : ValueObject
    {
        public static StaffGender Male => new StaffGender(nameof(Male));
        public static StaffGender Female => new StaffGender(nameof(Female));

        public StaffGender(string value)
        {
            Value = value;
        }

        public string Value { get; }
    }
}