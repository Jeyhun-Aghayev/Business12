namespace Business.Models.Base
{
    public abstract class BaseAudiTableEntity:BaseEntity
    {
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }
    }
}
