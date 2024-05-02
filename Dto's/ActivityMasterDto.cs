namespace GRCServices.Dto_s
{
    public class GetActivityMasterDto
    {
        public int Id { get; set; }
        public string? ActivityName { get; set; }
        public string? ActivityDescr { get; set; }
        public int? DoerRole { get; set; }
        public int? Frequency { get; set; }
        public int? Duration { get; set; }
        public int? RefDocument { get; set; }
        public int? OutputDocument { get; set; }
        public int? TriggeringActivity { get; set; }
        public int? ApproverRole { get; set; }
        public bool Auditable { get; set; }
        public int? PracticeId { get; set; }
        public int? HelpRef { get; set; }
        public char? Active { get; set; }
    }

    public class AddActivityMasterDto
    {
        public string? ActivityDescr { get; set; }
        public int DoerRole { get; set; }
        public int FrequencyId { get; set; }
        public int? Duration { get; set; }
        public int? RefDocumentId { get; set; }
        public int? OutputDocumentPath { get; set; }
        public int? TriggeringActivityNameId { get; set; }
        public int? ApproverRole { get; set; }
       // public int? PracticeId { get; set; }
        public int? HelpRef { get; set; }
        public bool IsActive { get; set; }
        public bool Auditable { get; set; }
        public int? ActivityNameId { get; set; }
        public int CustomerId { get; set; }
    }

    public class UpdateActivityMasterDto
    {
       // public int Id { get; set; }
        public int? ActivityNameid { get; set; }
        public string? ActivityDescr { get; set; }
        public int DoerRole { get; set; }
        public int Frequency { get; set; }
        public int? Duration { get; set; }
        public int RefDocument { get; set; }
        public int OutputDocument { get; set; }
        public int TriggeringActivity { get; set; }
        public int? ApproverRole { get; set; }
        public bool Auditable { get; set; }
        public int? PracticeId { get; set; }
        public int? HelpRef { get; set; }
        public char? Active { get; set; }
        public int? CustomerId { get; set; }
    }
}
